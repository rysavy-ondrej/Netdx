// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsSetResponse : KaitaiStruct
    {
        public static DlmsSetResponse FromFile(string fileName)
        {
            return new DlmsSetResponse(new KaitaiStream(fileName));
        }


        public enum SetResponseType
        {
            SetResponseNormal = 1,
            SetResponseDatablock = 2,
            SetResponseLastDatablock = 3,
            SetResponseLastDatablockWithList = 4,
            SetResponseWithList = 5,
        }
        public DlmsSetResponse(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _setResponseType = ((SetResponseType) m_io.ReadU1());
            switch (SetResponseType) {
            case SetResponseType.SetResponseWithList: {
                _response = new SetResponseWithList(m_io, this, m_root);
                break;
            }
            case SetResponseType.SetResponseNormal: {
                _response = new SetResponseNormal(m_io, this, m_root);
                break;
            }
            case SetResponseType.SetResponseLastDatablock: {
                _response = new SetResponseLastDatablock(m_io, this, m_root);
                break;
            }
            case SetResponseType.SetResponseDatablock: {
                _response = new SetResponseDatablock(m_io, this, m_root);
                break;
            }
            case SetResponseType.SetResponseLastDatablockWithList: {
                _response = new SetResponseLastDatablockWithList(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class SetResponseLastDatablock : KaitaiStruct
        {
            public static SetResponseLastDatablock FromFile(string fileName)
            {
                return new SetResponseLastDatablock(new KaitaiStream(fileName));
            }

            public SetResponseLastDatablock(KaitaiStream p__io, DlmsSetResponse p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsSetResponse m_root;
            private DlmsSetResponse m_parent;
            public DlmsSetResponse M_Root { get { return m_root; } }
            public DlmsSetResponse M_Parent { get { return m_parent; } }
        }
        public partial class SetResponseDatablock : KaitaiStruct
        {
            public static SetResponseDatablock FromFile(string fileName)
            {
                return new SetResponseDatablock(new KaitaiStream(fileName));
            }

            public SetResponseDatablock(KaitaiStream p__io, DlmsSetResponse p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _foo = new DlmsTypes.Boolean(m_io);
            }
            private DlmsTypes.Boolean _foo;
            private DlmsSetResponse m_root;
            private DlmsSetResponse m_parent;
            public DlmsTypes.Boolean Foo { get { return _foo; } }
            public DlmsSetResponse M_Root { get { return m_root; } }
            public DlmsSetResponse M_Parent { get { return m_parent; } }
        }
        public partial class SetResponseLastDatablockWithList : KaitaiStruct
        {
            public static SetResponseLastDatablockWithList FromFile(string fileName)
            {
                return new SetResponseLastDatablockWithList(new KaitaiStream(fileName));
            }

            public SetResponseLastDatablockWithList(KaitaiStream p__io, DlmsSetResponse p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsSetResponse m_root;
            private DlmsSetResponse m_parent;
            public DlmsSetResponse M_Root { get { return m_root; } }
            public DlmsSetResponse M_Parent { get { return m_parent; } }
        }
        public partial class SetResponseWithList : KaitaiStruct
        {
            public static SetResponseWithList FromFile(string fileName)
            {
                return new SetResponseWithList(new KaitaiStream(fileName));
            }

            public SetResponseWithList(KaitaiStream p__io, DlmsSetResponse p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsSetResponse m_root;
            private DlmsSetResponse m_parent;
            public DlmsSetResponse M_Root { get { return m_root; } }
            public DlmsSetResponse M_Parent { get { return m_parent; } }
        }
        public partial class SetResponseNormal : KaitaiStruct
        {
            public static SetResponseNormal FromFile(string fileName)
            {
                return new SetResponseNormal(new KaitaiStream(fileName));
            }

            public SetResponseNormal(KaitaiStream p__io, DlmsSetResponse p__parent = null, DlmsSetResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _invokeIdAndPriority = new DlmsStruct.InvokeIdAndPriority(m_io);
                _result = new DlmsStruct.DataAccessResult(m_io);
            }
            private DlmsStruct.InvokeIdAndPriority _invokeIdAndPriority;
            private DlmsStruct.DataAccessResult _result;
            private DlmsSetResponse m_root;
            private DlmsSetResponse m_parent;
            public DlmsStruct.InvokeIdAndPriority InvokeIdAndPriority { get { return _invokeIdAndPriority; } }
            public DlmsStruct.DataAccessResult Result { get { return _result; } }
            public DlmsSetResponse M_Root { get { return m_root; } }
            public DlmsSetResponse M_Parent { get { return m_parent; } }
        }
        private SetResponseType _setResponseType;
        private KaitaiStruct _response;
        private DlmsSetResponse m_root;
        private KaitaiStruct m_parent;
        public SetResponseType SetResponseType { get { return _setResponseType; } }
        public KaitaiStruct Response { get { return _response; } }
        public DlmsSetResponse M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
