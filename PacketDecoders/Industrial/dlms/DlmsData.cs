// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsData : KaitaiStruct
    {
        public static DlmsData FromFile(string fileName)
        {
            return new DlmsData(new KaitaiStream(fileName));
        }


        public enum DataType
        {
            NullData = 0,
            Array = 1,
            Structure = 2,
            Boolean = 3,
            BitString = 4,
            DoubleLong = 5,
            DoubleLongUnsigned = 6,
            OctetString = 9,
            VisibleString = 10,
            Bcd = 13,
            Integer = 15,
            Long = 16,
            Unsigned = 17,
            LongUnsigned = 18,
            CompactArray = 19,
            Long64 = 20,
            Long64Unsigned = 21,
            Enum = 22,
            Float32 = 23,
            Float64 = 24,
            DateTime = 25,
            Date = 26,
            Time = 27,
            DoNotCare = 255,
        }

        public enum Boolean
        {
            False = 0,
            True = 1,
        }
        public DlmsData(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsData p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _dataType = ((DataType) m_io.ReadU1());
            switch (DataType) {
            case DataType.NullData: {
                _dataValue = new NullData(m_io, this, m_root);
                break;
            }
            }
        }
        public partial class NullData : KaitaiStruct
        {
            public static NullData FromFile(string fileName)
            {
                return new NullData(new KaitaiStream(fileName));
            }

            public NullData(KaitaiStream p__io, DlmsData p__parent = null, DlmsData p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _nothing = m_io.ReadBytes(0);
            }
            private byte[] _nothing;
            private DlmsData m_root;
            private DlmsData m_parent;
            public byte[] Nothing { get { return _nothing; } }
            public DlmsData M_Root { get { return m_root; } }
            public DlmsData M_Parent { get { return m_parent; } }
        }
        private DataType _dataType;
        private NullData _dataValue;
        private DlmsData m_root;
        private KaitaiStruct m_parent;
        public DataType DataType { get { return _dataType; } }
        public NullData DataValue { get { return _dataValue; } }
        public DlmsData M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
