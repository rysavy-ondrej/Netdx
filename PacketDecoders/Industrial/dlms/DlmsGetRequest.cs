// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsGetRequest : KaitaiStruct
    {
        public static DlmsGetRequest FromFile(string fileName)
        {
            return new DlmsGetRequest(new KaitaiStream(fileName));
        }


        public enum GetRequestType
        {
            GetRequestNormal = 1,
            GetRequestNext = 2,
            GetRequestWithList = 3,
        }
        public DlmsGetRequest(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsGetRequest p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _getRequestType = ((GetRequestType) m_io.ReadU1());
            switch (GetRequestType) {
            case GetRequestType.GetRequestNormal: {
                _request = new GetRequestNormal(m_io, this, m_root);
                break;
            }
            case GetRequestType.GetRequestNext: {
                _request = new GetRequestNext(m_io, this, m_root);
                break;
            }
            case GetRequestType.GetRequestWithList: {
                _request = new GetRequestWithList(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class GetRequestNormal : KaitaiStruct
        {
            public static GetRequestNormal FromFile(string fileName)
            {
                return new GetRequestNormal(new KaitaiStream(fileName));
            }

            public GetRequestNormal(KaitaiStream p__io, DlmsGetRequest p__parent = null, DlmsGetRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _invokeIdAndPriority = new DlmsStruct.InvokeIdAndPriority(m_io);
                _cosemAttributeDescriptor = new DlmsStruct.CosemAttributeDescriptor(m_io);
                _accessSelection = new DlmsStruct.SelectiveAccessDescriptorOptional(m_io);
            }
            private DlmsStruct.InvokeIdAndPriority _invokeIdAndPriority;
            private DlmsStruct.CosemAttributeDescriptor _cosemAttributeDescriptor;
            private DlmsStruct.SelectiveAccessDescriptorOptional _accessSelection;
            private DlmsGetRequest m_root;
            private DlmsGetRequest m_parent;
            public DlmsStruct.InvokeIdAndPriority InvokeIdAndPriority { get { return _invokeIdAndPriority; } }
            public DlmsStruct.CosemAttributeDescriptor CosemAttributeDescriptor { get { return _cosemAttributeDescriptor; } }
            public DlmsStruct.SelectiveAccessDescriptorOptional AccessSelection { get { return _accessSelection; } }
            public DlmsGetRequest M_Root { get { return m_root; } }
            public DlmsGetRequest M_Parent { get { return m_parent; } }
        }
        public partial class GetRequestNext : KaitaiStruct
        {
            public static GetRequestNext FromFile(string fileName)
            {
                return new GetRequestNext(new KaitaiStream(fileName));
            }

            public GetRequestNext(KaitaiStream p__io, DlmsGetRequest p__parent = null, DlmsGetRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _invokeIdAndPriority = new DlmsStruct.InvokeIdAndPriority(m_io);
                _blockNumber = m_io.ReadU4be();
            }
            private DlmsStruct.InvokeIdAndPriority _invokeIdAndPriority;
            private uint _blockNumber;
            private DlmsGetRequest m_root;
            private DlmsGetRequest m_parent;
            public DlmsStruct.InvokeIdAndPriority InvokeIdAndPriority { get { return _invokeIdAndPriority; } }
            public uint BlockNumber { get { return _blockNumber; } }
            public DlmsGetRequest M_Root { get { return m_root; } }
            public DlmsGetRequest M_Parent { get { return m_parent; } }
        }
        public partial class GetRequestWithList : KaitaiStruct
        {
            public static GetRequestWithList FromFile(string fileName)
            {
                return new GetRequestWithList(new KaitaiStream(fileName));
            }

            public GetRequestWithList(KaitaiStream p__io, DlmsGetRequest p__parent = null, DlmsGetRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _invokeIdAndPriority = new DlmsStruct.InvokeIdAndPriority(m_io);
                _attributeDescriptorList = new DlmsStruct.CosemAttributeDescriptorWithSelection(m_io);
            }
            private DlmsStruct.InvokeIdAndPriority _invokeIdAndPriority;
            private DlmsStruct.CosemAttributeDescriptorWithSelection _attributeDescriptorList;
            private DlmsGetRequest m_root;
            private DlmsGetRequest m_parent;
            public DlmsStruct.InvokeIdAndPriority InvokeIdAndPriority { get { return _invokeIdAndPriority; } }
            public DlmsStruct.CosemAttributeDescriptorWithSelection AttributeDescriptorList { get { return _attributeDescriptorList; } }
            public DlmsGetRequest M_Root { get { return m_root; } }
            public DlmsGetRequest M_Parent { get { return m_parent; } }
        }
        private GetRequestType _getRequestType;
        private KaitaiStruct _request;
        private DlmsGetRequest m_root;
        private KaitaiStruct m_parent;
        public GetRequestType GetRequestType { get { return _getRequestType; } }
        public KaitaiStruct Request { get { return _request; } }
        public DlmsGetRequest M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
