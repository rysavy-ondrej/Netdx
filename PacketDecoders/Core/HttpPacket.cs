using Kaitai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netdx.PacketDecoders.Core
{
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
                    m_body = new HttpBody(m_io, m_header.ContentLength, this, m_root);
                    break;
                case "HTTP/1.0":
                case "HTTP/1.1":
                    m_response = new HttpResponse(m_io, this, m_root);
                    m_header = new HttpHeader(m_io, this, m_root);
                    m_body = new HttpBody(m_io, m_header.ContentLength, this, m_root);
                    break;
            }
        }

        public partial class HttpBody : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;
            private readonly long m_length;
            public HttpBody(KaitaiStream io, long length, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                m_length = length;
                _parse();
            }
            private void _parse()
            {
                throw new NotImplementedException();
            }
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
                    var (_, Value) = m_headerFields.FirstOrDefault(x => x.Name.Equals("ContentLength", StringComparison.InvariantCultureIgnoreCase) || x.Name.Equals("Content-Length", StringComparison.InvariantCultureIgnoreCase));
                    return long.TryParse(Value, out var result) ? result : 0;
                }
            }

            private void _parse()
            {
                throw new NotImplementedException();
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
            private void _parse()
            {
                m_command = m_io.ReadAsciiStringTerm(' ', false);
                m_uri = m_io.ReadAsciiStringTerm(' ', false);
                m_version = m_io.ReadAsciiStringTerm("\r\n", false);
                throw new NotImplementedException();
            }
        }
        public partial class HttpResponse : KaitaiStruct
        {
            private readonly KaitaiStruct m_parent;
            private readonly HttpPacket m_root;

            public HttpResponse(KaitaiStream io, KaitaiStruct parent = null, HttpPacket root = null) : base(io)
            {
                m_parent = parent;
                m_root = root;
                _parse();
            }
            private void _parse()
            {
                throw new NotImplementedException();   
            }
        }
    }
}
