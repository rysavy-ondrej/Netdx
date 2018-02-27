// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsActionResponse : KaitaiStruct
    {
        public static DlmsActionResponse FromFile(string fileName)
        {
            return new DlmsActionResponse(new KaitaiStream(fileName));
        }


        public enum ActionResponseType
        {
            ActionResponseNormal = 1,
            ActionResponseWithPblock = 2,
            ActionResponseWithList = 3,
            ActionResponseNextPblock = 4,
        }
        public DlmsActionResponse(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsActionResponse p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _actionResponseType = ((ActionResponseType) m_io.ReadU1());
            switch (ActionResponseType) {
            case ActionResponseType.ActionResponseNormal: {
                _response = new ActionResponseNormal(m_io, this, m_root);
                break;
            }
            case ActionResponseType.ActionResponseWithPblock: {
                _response = new ActionResponseWithPblock(m_io, this, m_root);
                break;
            }
            case ActionResponseType.ActionResponseWithList: {
                _response = new ActionResponseWithList(m_io, this, m_root);
                break;
            }
            case ActionResponseType.ActionResponseNextPblock: {
                _response = new ActionResponseNextPblock(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class ActionResponseNormal : KaitaiStruct
        {
            public static ActionResponseNormal FromFile(string fileName)
            {
                return new ActionResponseNormal(new KaitaiStream(fileName));
            }

            public ActionResponseNormal(KaitaiStream p__io, DlmsActionResponse p__parent = null, DlmsActionResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionResponse m_root;
            private DlmsActionResponse m_parent;
            public DlmsActionResponse M_Root { get { return m_root; } }
            public DlmsActionResponse M_Parent { get { return m_parent; } }
        }
        public partial class ActionResponseWithPblock : KaitaiStruct
        {
            public static ActionResponseWithPblock FromFile(string fileName)
            {
                return new ActionResponseWithPblock(new KaitaiStream(fileName));
            }

            public ActionResponseWithPblock(KaitaiStream p__io, DlmsActionResponse p__parent = null, DlmsActionResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionResponse m_root;
            private DlmsActionResponse m_parent;
            public DlmsActionResponse M_Root { get { return m_root; } }
            public DlmsActionResponse M_Parent { get { return m_parent; } }
        }
        public partial class ActionResponseWithList : KaitaiStruct
        {
            public static ActionResponseWithList FromFile(string fileName)
            {
                return new ActionResponseWithList(new KaitaiStream(fileName));
            }

            public ActionResponseWithList(KaitaiStream p__io, DlmsActionResponse p__parent = null, DlmsActionResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionResponse m_root;
            private DlmsActionResponse m_parent;
            public DlmsActionResponse M_Root { get { return m_root; } }
            public DlmsActionResponse M_Parent { get { return m_parent; } }
        }
        public partial class ActionResponseNextPblock : KaitaiStruct
        {
            public static ActionResponseNextPblock FromFile(string fileName)
            {
                return new ActionResponseNextPblock(new KaitaiStream(fileName));
            }

            public ActionResponseNextPblock(KaitaiStream p__io, DlmsActionResponse p__parent = null, DlmsActionResponse p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionResponse m_root;
            private DlmsActionResponse m_parent;
            public DlmsActionResponse M_Root { get { return m_root; } }
            public DlmsActionResponse M_Parent { get { return m_parent; } }
        }
        private ActionResponseType _actionResponseType;
        private KaitaiStruct _response;
        private DlmsActionResponse m_root;
        private KaitaiStruct m_parent;
        public ActionResponseType ActionResponseType { get { return _actionResponseType; } }
        public KaitaiStruct Response { get { return _response; } }
        public DlmsActionResponse M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
