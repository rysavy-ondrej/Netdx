// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsTypes : KaitaiStruct
    {
        public static DlmsTypes FromFile(string fileName)
        {
            return new DlmsTypes(new KaitaiStream(fileName));
        }


        public enum Boolean
        {
            False = 0,
            True = 1,
        }
        public DlmsTypes(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
        }
        public partial class OctetString : KaitaiStruct
        {
            public static OctetString FromFile(string fileName)
            {
                return new OctetString(new KaitaiStream(fileName));
            }

            public OctetString(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _length = new LengthEncoded(m_io, this, m_root);
                _value = System.Text.Encoding.GetEncoding("ascii").GetString(m_io.ReadBytes(Length.Value));
            }
            private LengthEncoded _length;
            private string _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public LengthEncoded Length { get { return _length; } }
            public string Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class OctetStringOptional : KaitaiStruct
        {
            public static OctetStringOptional FromFile(string fileName)
            {
                return new OctetStringOptional(new KaitaiStream(fileName));
            }

            public OctetStringOptional(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _present = m_io.ReadU1();
                if (Present != 0) {
                    _value = new OctetString(m_io, this, m_root);
                }
            }
            private byte _present;
            private OctetString _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte Present { get { return _present; } }
            public OctetString Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class LengthEncoded : KaitaiStruct
        {
            public static LengthEncoded FromFile(string fileName)
            {
                return new LengthEncoded(new KaitaiStream(fileName));
            }

            public LengthEncoded(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_value = false;
                _read();
            }
            private void _read()
            {
                _b1 = m_io.ReadU1();
                if (B1 == 130) {
                    _int2 = m_io.ReadU2be();
                }
            }
            private bool f_value;
            private ushort _value;
            public ushort Value
            {
                get
                {
                    if (f_value)
                        return _value;
                    _value = (ushort) (((B1 & 128) == 0 ? B1 : Int2));
                    f_value = true;
                    return _value;
                }
            }
            private byte _b1;
            private ushort? _int2;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte B1 { get { return _b1; } }
            public ushort? Int2 { get { return _int2; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Boolean : KaitaiStruct
        {
            public static Boolean FromFile(string fileName)
            {
                return new Boolean(new KaitaiStream(fileName));
            }

            public Boolean(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = (() m_io.ReadU1());
            }
            private Boolean _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public Boolean Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class BooleanOptional : KaitaiStruct
        {
            public static BooleanOptional FromFile(string fileName)
            {
                return new BooleanOptional(new KaitaiStream(fileName));
            }

            public BooleanOptional(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _present = m_io.ReadU1();
                if (Present != 0) {
                    _value = new Boolean(m_io, this, m_root);
                }
            }
            private byte _present;
            private Boolean _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte Present { get { return _present; } }
            public Boolean Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        private DlmsTypes m_root;
        private KaitaiStruct m_parent;
        public DlmsTypes M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
