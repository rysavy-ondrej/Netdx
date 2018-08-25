using Kaitai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netdx.Packets.Core
{
    public enum HttpPacketType { Request, Response, Data }
    /// <summary>
    /// Implements Http packet parser. Limitation: it does not parse body of HTTP packet. 
    /// </summary>
    public partial class HttpPacket : KaitaiStruct
    {
        private HttpPacket m_root;
        private KaitaiStruct m_parent;
        private HttpRequest m_request;
        private HttpResponse m_response;
        private HttpHeader m_header;
        private HttpBody m_body;

        public HttpPacket(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
        {
            m_parent = parent;
            m_root = root ?? this;
            _parse();
        }

        public HttpPacketType PacketType
        {
            get
            {
                if (m_request != null) return HttpPacketType.Request;
                if (m_response != null) return HttpPacketType.Response;
                return HttpPacketType.Data;
            }
        }

        public HttpRequest Request { get => m_request; }
        public HttpResponse Response { get => m_response;  }
        public HttpHeader Header { get => m_header;  }
        public HttpBody Body { get => m_body; }
        private void _parse()
        {
            var keyword = m_io.PeekAsciiStringTerm(' ',false);
            switch(keyword.ToUpperInvariant())
            {
                case "GET":
                case "PUT":
                case "HEAD":
                case "POST":
                case "TRACE":
                case "PATCH":
                case "DELETE":
                case "UNLINK":
                case "CONNECT":
                case "OPTIONS":
                case "CCM_POST":
                case "RPC_CONNECT":
                case "RPC_IN_DATA":
                case "RPC_OUT_DATA":
                case "SSTP_DUPLEX_POST":
                case "MERGE":
                case "NOTIFY":
                case "M-SEARCH":
                case "COPY":
			    case "LOCK":
			    case "MOVE":
			    case "MKCOL":
			    case "SEARCH":
			    case "UNLOCK":
			    case "PROPFIND":
			    case "PROPPATCH":
			    case "GETLIB":
			    case "SUBSCRIBE":
                    m_request = new HttpRequest(m_io, this, m_root);
                    m_header = new HttpHeader(m_io, this, m_root);
                    m_body = new HttpBody(m_io, this, m_root);
                    break;
                case "HTTP/1.0":
                case "HTTP/1.1":
                    m_response = new HttpResponse(m_io, this, m_root);
                    m_header = new HttpHeader(m_io, this, m_root);
                    m_body = new HttpBody(m_io, this, m_root);
                    break;
                default:
                    m_body = new HttpBody(m_io, this, m_root);
                    break;
            }
        }

        public partial class HttpBody : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;
            private byte[] m_bytes;
            public HttpBody(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }
            private void _parse()
            {
                m_bytes = m_io.ReadBytesFull();
            }
            public byte[] Bytes { get => m_bytes; }
        }
        public partial class HttpHeader : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;
            private readonly List<(string Name, string Value)> m_headerFields;
            public HttpHeader(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                m_headerFields = new List<(string Name, string Value)>();
                _parse();
            }

            public long ContentLength {
                get
                {
                    var value = GetLine("ContentLength", "Content-Length");
                    return long.TryParse(value, out var result) ? result : 0;
                }
            }


            public string GetLine(params string[] names)
            {
                var (_, value) = m_headerFields.FirstOrDefault(x => names.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                return value;
            }

            public string Host => GetLine("Host");

            public IList<(string Name, string Value)> Lines => m_headerFields;

            private void _parse()
            {
                while(!m_io.PeekAsciiString(2).Equals("\r\n") && !m_io.IsEof)
                {
                    var name = m_io.ReadAsciiStringTerm(':', false).Trim();
                    var value = m_io.ReadAsciiStringTerm("\r\n", false).Trim();
                    m_headerFields.Add((name, value));
                }
                if (!m_io.IsEof)
                {
                    m_io.ReadAsciiString(2);
                }
            }
        }
        public partial class HttpRequest : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;
            private string m_command;
            private string m_uri;
            private string m_version;

            public HttpRequest(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }

            public string Command { get => m_command;  }
            public string Uri { get => m_uri;  }
            public string Version { get => m_version;  }

            private void _parse()
            {
                m_command = m_io.ReadAsciiStringTerm(' ', false);
                m_uri = m_io.ReadAsciiStringTerm(' ', false);
                m_version = m_io.ReadAsciiStringTerm("\r\n", false);
            }
        }
        public partial class HttpResponse : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;
            private string m_version;
            private string m_statusCode;
            private string m_reason;

            public HttpResponse(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }

            public string Version { get => m_version;  }
            public string StatusCode { get => m_statusCode;  }
            public string Reason { get => m_reason;  }

            private void _parse()
            {
                m_version = m_io.ReadAsciiStringTerm(' ', false);
                m_statusCode = m_io.ReadAsciiStringTerm(' ', false);
                m_reason = m_io.ReadAsciiStringTerm("\r\n", false);
            }
        }
    }
}
