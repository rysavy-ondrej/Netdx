// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{

    /// <summary>
    /// The ACSE APDUs are part of DLMS/COSEM communication during opening and  closing phase. ACSE APDUs are standardized by IEC 8650-1 standard or X.227 The APDUs are encoded by BER for transmission. The user-information field  contains a COSEMpdu encoded by A-XDR.
    /// References:
    /// * Distribution automation using distribution line carrier systems - Part 6: A-XDR encoding rule, IEC 61334-6:2000, 2006, Edition 1. * ITU-T Recommendation X.227: Data Networks and Open System Communications: Connection-oriented Protocol for the Association Control Service Element: Protocol Specification, ITU-T X.227, 1995.
    /// </summary>
    public partial class AcsePdu : KaitaiStruct
    {
        public static AcsePdu FromFile(string fileName)
        {
            return new AcsePdu(new KaitaiStream(fileName));
        }


        public enum PduType
        {
            Aarq = 96,
            Aare = 97,
            Rlrq = 98,
            Rlre = 99,
            Abrt = 100,
            Adt = 101,
        }
        public AcsePdu(KaitaiStream p__io, KaitaiStruct p__parent = null, AcsePdu p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _pduType = ((PduType) m_io.ReadU1());
            switch (PduType) {
            case PduType.Adt: {
                _pdu = new AdtPdu(m_io, this, m_root);
                break;
            }
            case PduType.Rlrq: {
                _pdu = new RlrqPdu(m_io, this, m_root);
                break;
            }
            case PduType.Aarq: {
                _pdu = new AarqPdu(m_io, this, m_root);
                break;
            }
            case PduType.Abrt: {
                _pdu = new AbrtPdu(m_io, this, m_root);
                break;
            }
            case PduType.Rlre: {
                _pdu = new RlrePdu(m_io, this, m_root);
                break;
            }
            case PduType.Aare: {
                _pdu = new AarePdu(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class RlrqPdu : KaitaiStruct
        {
            public static RlrqPdu FromFile(string fileName)
            {
                return new RlrqPdu(new KaitaiStream(fileName));
            }

            public RlrqPdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        public partial class AbrtPdu : KaitaiStruct
        {
            public static AbrtPdu FromFile(string fileName)
            {
                return new AbrtPdu(new KaitaiStream(fileName));
            }

            public AbrtPdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        public partial class AarqPdu : KaitaiStruct
        {
            public static AarqPdu FromFile(string fileName)
            {
                return new AarqPdu(new KaitaiStream(fileName));
            }

            public AarqPdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        public partial class AarePdu : KaitaiStruct
        {
            public static AarePdu FromFile(string fileName)
            {
                return new AarePdu(new KaitaiStream(fileName));
            }

            public AarePdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        public partial class RlrePdu : KaitaiStruct
        {
            public static RlrePdu FromFile(string fileName)
            {
                return new RlrePdu(new KaitaiStream(fileName));
            }

            public RlrePdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        public partial class AdtPdu : KaitaiStruct
        {
            public static AdtPdu FromFile(string fileName)
            {
                return new AdtPdu(new KaitaiStream(fileName));
            }

            public AdtPdu(KaitaiStream p__io, AcsePdu p__parent = null, AcsePdu p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private AcsePdu m_root;
            private AcsePdu m_parent;
            public AcsePdu M_Root { get { return m_root; } }
            public AcsePdu M_Parent { get { return m_parent; } }
        }
        private PduType _pduType;
        private KaitaiStruct _pdu;
        private AcsePdu m_root;
        private KaitaiStruct m_parent;
        public PduType PduType { get { return _pduType; } }
        public KaitaiStruct Pdu { get { return _pdu; } }
        public AcsePdu M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
