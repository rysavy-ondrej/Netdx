// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsActionRequest : KaitaiStruct
    {
        public static DlmsActionRequest FromFile(string fileName)
        {
            return new DlmsActionRequest(new KaitaiStream(fileName));
        }


        public enum ActionRequestType
        {
            ActionRequestNormal = 1,
            ActionRequestNextPblock = 2,
            ActionRequestWithList = 3,
            ActionRequestWithFirstPblock = 4,
            ActionRequestWithListAndFirstPblock = 5,
            ActionRequestWithPblock = 6,
        }
        public DlmsActionRequest(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _actionRequestType = ((ActionRequestType) m_io.ReadU1());
            switch (ActionRequestType) {
            case ActionRequestType.ActionRequestWithList: {
                _request = new ActionRequestWithList(m_io, this, m_root);
                break;
            }
            case ActionRequestType.ActionRequestNormal: {
                _request = new ActionRequestNormal(m_io, this, m_root);
                break;
            }
            case ActionRequestType.ActionRequestWithListAndFirstPblock: {
                _request = new ActionRequestWithListAndFirstPblock(m_io, this, m_root);
                break;
            }
            case ActionRequestType.ActionRequestWithFirstPblock: {
                _request = new ActionRequestWithFirstPblock(m_io, this, m_root);
                break;
            }
            case ActionRequestType.ActionRequestNextPblock: {
                _request = new ActionRequestNextPblock(m_io, this, m_root);
                break;
            }
            case ActionRequestType.ActionRequestWithPblock: {
                _request = new ActionRequestWithPblock(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class ActionRequestWithFirstPblock : KaitaiStruct
        {
            public static ActionRequestWithFirstPblock FromFile(string fileName)
            {
                return new ActionRequestWithFirstPblock(new KaitaiStream(fileName));
            }

            public ActionRequestWithFirstPblock(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        public partial class ActionRequestWithListAndFirstPblock : KaitaiStruct
        {
            public static ActionRequestWithListAndFirstPblock FromFile(string fileName)
            {
                return new ActionRequestWithListAndFirstPblock(new KaitaiStream(fileName));
            }

            public ActionRequestWithListAndFirstPblock(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        public partial class ActionRequestNormal : KaitaiStruct
        {
            public static ActionRequestNormal FromFile(string fileName)
            {
                return new ActionRequestNormal(new KaitaiStream(fileName));
            }

            public ActionRequestNormal(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        public partial class ActionRequestNextPblock : KaitaiStruct
        {
            public static ActionRequestNextPblock FromFile(string fileName)
            {
                return new ActionRequestNextPblock(new KaitaiStream(fileName));
            }

            public ActionRequestNextPblock(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        public partial class ActionRequestWithPblock : KaitaiStruct
        {
            public static ActionRequestWithPblock FromFile(string fileName)
            {
                return new ActionRequestWithPblock(new KaitaiStream(fileName));
            }

            public ActionRequestWithPblock(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        public partial class ActionRequestWithList : KaitaiStruct
        {
            public static ActionRequestWithList FromFile(string fileName)
            {
                return new ActionRequestWithList(new KaitaiStream(fileName));
            }

            public ActionRequestWithList(KaitaiStream p__io, DlmsActionRequest p__parent = null, DlmsActionRequest p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsActionRequest m_root;
            private DlmsActionRequest m_parent;
            public DlmsActionRequest M_Root { get { return m_root; } }
            public DlmsActionRequest M_Parent { get { return m_parent; } }
        }
        private ActionRequestType _actionRequestType;
        private KaitaiStruct _request;
        private DlmsActionRequest m_root;
        private KaitaiStruct m_parent;
        public ActionRequestType ActionRequestType { get { return _actionRequestType; } }
        public KaitaiStruct Request { get { return _request; } }
        public DlmsActionRequest M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
