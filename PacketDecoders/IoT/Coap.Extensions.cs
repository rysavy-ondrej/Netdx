using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netdx.Packets.IoT
{
    public enum RequestMethod
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Delete = 4
    }
    public enum ResponseCode
    {
        Success = 2,
        Reserved = 3,
        ClientError = 4,
        ServerError = 5
    }
    
    public partial class Coap
    {

        /// <summary>
        /// Tests if the given array of bytes represents a valid CoAP message. 
        /// </summary>
        /// <remarks>
        /// This is simple pattern-based matching procedure. The pattern 
        /// is developed based on the header format as specififed 
        /// in https://tools.ietf.org/html/rfc7252#section-3. </remarks>
        /// <param name="array"></param>
        /// <returns>true if the byte array represents CoAP packet and false otherwise.</returns>
        public static bool IsCoap(Span<byte> array)
        {
            if (array.Length < 4) return false;
            var first = array[0];
            var second = array[1];
            // Version must be 01
            if ((first & 0b11000000) != 0b01000000) return false;
            // Token length must be 0-8:
            if ((first & 0b00001111) > 8) return false;
            // Code is 
            var code = (second >> 5);
            var details = (second & 0b00011111);
            switch(code)
            {
                case 0:
                    return details < 5;
                case 2:
                case 4:
                case 5:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsRequest => (int)Code < 0x40 && Code != CoapCode.Empty;


        public RequestMethod RequestMethod => (RequestMethod)((int)Code & 0b00011111);

        public ResponseCode ResponseCode => (ResponseCode)(((int)Code) >> 5);

        public string Info
        {
            get
            {
                var token = BitConverter.ToString(Token).Replace("-", "");
                var str = $"{this.Type.ToString().ToUpperInvariant()}, MID:{this.MessageId}, TOKEN: {token}, {this.Code.ToString().ToUpperInvariant()}";
                return str;
            }
        }

        /// <summary>
        /// Gets the URI from options. 
        /// </summary>
        /// <returns></returns>
        IEnumerable<byte[]> GetOptionValues(CoapOptions option)
        {
            if (Options == null) yield break;
            var currentOption = 0;
            foreach(var opt in Options)
            {
                if (opt.IsPayloadMarker) break;
                currentOption += opt.Delta;
                if (currentOption == (int)option)
                {
                    yield return opt.Value;
                }
            }        
        }
        static byte[] emptyByteArray = new byte[] { };
        /// <summary>
        /// Gets CoAP URI. This string has the following format: coap-URI = "coap:" "//" host [ ":" port ] path-abempty [ "?" query ]
        /// </summary>
        public Uri GetUri(string defaultHost, int defaultPort = 5683)
        {
            var host = GetOptionValues(CoapOptions.UriHost).FirstOrDefault();
            var port = GetOptionValues(CoapOptions.UriPort).FirstOrDefault();
            var path = GetOptionValues(CoapOptions.UriPath);
            var query = GetOptionValues(CoapOptions.UriQuery);

            var urb = new UriBuilder
            {
                Scheme = "coap",
                Host = host != null ? Encoding.ASCII.GetString(host) : defaultHost,
                Port = port != null ? BitConverter.ToUInt16(port, 0) : defaultPort,
                Path = String.Join("/", (path.ToArray().Select(Encoding.ASCII.GetString))),
                Query = String.Join("/", (query.ToArray().Select(Encoding.ASCII.GetString)))
            };
            return urb.Uri;
        }
    }
}
