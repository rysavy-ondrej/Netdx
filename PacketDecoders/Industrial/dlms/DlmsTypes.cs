// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;
using System.Collections.Generic;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsTypes : KaitaiStruct
    {
        public static DlmsTypes FromFile(string fileName)
        {
            return new DlmsTypes(new KaitaiStream(fileName));
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
        public partial class DoubleLong : KaitaiStruct
        {
            public static DoubleLong FromFile(string fileName)
            {
                return new DoubleLong(new KaitaiStream(fileName));
            }

            public DoubleLong(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadS4be();
            }
            private int _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public int Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
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
        public partial class NullData : KaitaiStruct
        {
            public static NullData FromFile(string fileName)
            {
                return new NullData(new KaitaiStream(fileName));
            }

            public NullData(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
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
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte[] Nothing { get { return _nothing; } }
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
        public partial class Float64 : KaitaiStruct
        {
            public static Float64 FromFile(string fileName)
            {
                return new Float64(new KaitaiStream(fileName));
            }

            public Float64(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadF8be();
            }
            private double _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public double Value { get { return _value; } }
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
        public partial class CompactArray : KaitaiStruct
        {
            public static CompactArray FromFile(string fileName)
            {
                return new CompactArray(new KaitaiStream(fileName));
            }

            public CompactArray(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class DoNotCare : KaitaiStruct
        {
            public static DoNotCare FromFile(string fileName)
            {
                return new DoNotCare(new KaitaiStream(fileName));
            }

            public DoNotCare(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Array : KaitaiStruct
        {
            public static Array FromFile(string fileName)
            {
                return new Array(new KaitaiStream(fileName));
            }

            public Array(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Float32 : KaitaiStruct
        {
            public static Float32 FromFile(string fileName)
            {
                return new Float32(new KaitaiStream(fileName));
            }

            public Float32(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadF4be();
            }
            private float _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public float Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Long : KaitaiStruct
        {
            public static Long FromFile(string fileName)
            {
                return new Long(new KaitaiStream(fileName));
            }

            public Long(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadS2be();
            }
            private short _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public short Value { get { return _value; } }
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
                _value = m_io.ReadU1();
            }
            private byte _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Structure : KaitaiStruct
        {
            public static Structure FromFile(string fileName)
            {
                return new Structure(new KaitaiStream(fileName));
            }

            public Structure(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Date : KaitaiStruct
        {
            public static Date FromFile(string fileName)
            {
                return new Date(new KaitaiStream(fileName));
            }

            public Date(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadBytes(5);
            }
            private byte[] _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte[] Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class DoubleLongUnsigned : KaitaiStruct
        {
            public static DoubleLongUnsigned FromFile(string fileName)
            {
                return new DoubleLongUnsigned(new KaitaiStream(fileName));
            }

            public DoubleLongUnsigned(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadU4be();
            }
            private uint _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public uint Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class VisibleString : KaitaiStruct
        {
            public static VisibleString FromFile(string fileName)
            {
                return new VisibleString(new KaitaiStream(fileName));
            }

            public VisibleString(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Enum : KaitaiStruct
        {
            public static Enum FromFile(string fileName)
            {
                return new Enum(new KaitaiStream(fileName));
            }

            public Enum(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadU1();
            }
            private byte _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Unsigned : KaitaiStruct
        {
            public static Unsigned FromFile(string fileName)
            {
                return new Unsigned(new KaitaiStream(fileName));
            }

            public Unsigned(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadU1();
            }
            private byte _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class DateTime : KaitaiStruct
        {
            public static DateTime FromFile(string fileName)
            {
                return new DateTime(new KaitaiStream(fileName));
            }

            public DateTime(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadBytes(12);
            }
            private byte[] _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte[] Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Long64Unsigned : KaitaiStruct
        {
            public static Long64Unsigned FromFile(string fileName)
            {
                return new Long64Unsigned(new KaitaiStream(fileName));
            }

            public Long64Unsigned(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadU8be();
            }
            private ulong _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public ulong Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Integer : KaitaiStruct
        {
            public static Integer FromFile(string fileName)
            {
                return new Integer(new KaitaiStream(fileName));
            }

            public Integer(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadS1();
            }
            private sbyte _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public sbyte Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Time : KaitaiStruct
        {
            public static Time FromFile(string fileName)
            {
                return new Time(new KaitaiStream(fileName));
            }

            public Time(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadBytes(4);
            }
            private byte[] _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public byte[] Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Bcd : KaitaiStruct
        {
            public static Bcd FromFile(string fileName)
            {
                return new Bcd(new KaitaiStream(fileName));
            }

            public Bcd(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class Long64 : KaitaiStruct
        {
            public static Long64 FromFile(string fileName)
            {
                return new Long64(new KaitaiStream(fileName));
            }

            public Long64(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadS8be();
            }
            private long _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public long Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class BitString : KaitaiStruct
        {
            public static BitString FromFile(string fileName)
            {
                return new BitString(new KaitaiStream(fileName));
            }

            public BitString(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _length = new LengthEncoded(m_io, this, m_root);
                _value = new List<byte>((int) (Length.Value));
                for (var i = 0; i < Length.Value; i++)
                {
                    _value.Add(m_io.ReadU1());
                }
            }
            private LengthEncoded _length;
            private List<byte> _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public LengthEncoded Length { get { return _length; } }
            public List<byte> Value { get { return _value; } }
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
        public partial class LongUnsigned : KaitaiStruct
        {
            public static LongUnsigned FromFile(string fileName)
            {
                return new LongUnsigned(new KaitaiStream(fileName));
            }

            public LongUnsigned(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsTypes p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _value = m_io.ReadU2be();
            }
            private ushort _value;
            private DlmsTypes m_root;
            private KaitaiStruct m_parent;
            public ushort Value { get { return _value; } }
            public DlmsTypes M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        private DlmsTypes m_root;
        private KaitaiStruct m_parent;
        public DlmsTypes M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
