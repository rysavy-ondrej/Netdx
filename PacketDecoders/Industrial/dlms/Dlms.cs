// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class Dlms : KaitaiStruct
    {
        public static Dlms FromFile(string fileName)
        {
            return new Dlms(new KaitaiStream(fileName));
        }


        public enum LlcPacketType
        {
            LlcCommand = 230,
            LlcResponse = 231,
        }

        public enum HdlcPfBit
        {
            HdlcPoll = 0,
            HdlcFinal = 1,
        }

        public enum FrameTypeEnum
        {
            IFrame = 0,
            SFrame = 1,
            IFrame = 2,
            UFrame = 3,
        }

        public enum SFrameType
        {
            ReceiverReady = 0,
            ReceiverNotReady = 1,
            Reject = 2,
            SelectiveReject = 3,
        }

        public enum HdlcCr
        {
            HdlcCommand = 0,
            HdclResponse = 1,
        }
        public Dlms(KaitaiStream p__io, KaitaiStruct p__parent = null, Dlms p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            f_size = false;
            _read();
        }
        private void _read()
        {
            _hdlcHeader = new HdlcHeader(m_io, this, m_root);
            if ( ((HdlcHeader.Control.FrameType == 0) || (HdlcHeader.Control.FrameType == 2)) ) {
                _llcHeader = new LlcHeader(m_io, this, m_root);
            }
            _dlmsPdu = m_io.ReadBytes(((HdlcHeader.Format.FrameLength - HdlcHeader.Size) - 4));
            _hdlcTrailer = new HdlcTrailer(m_io, this, m_root);
        }
        public partial class LlcHeader : KaitaiStruct
        {
            public static LlcHeader FromFile(string fileName)
            {
                return new LlcHeader(new KaitaiStream(fileName));
            }

            public LlcHeader(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
                _sig = m_io.EnsureFixedContents(new byte[] { 230 });
                _packetType = ((Dlms.LlcPacketType) m_io.ReadU1());
                _zero = m_io.EnsureFixedContents(new byte[] { 0 });
            }
            private bool f_size;
            private sbyte _size;
            public sbyte Size
            {
                get
                {
                    if (f_size)
                        return _size;
                    _size = (sbyte) (3);
                    f_size = true;
                    return _size;
                }
            }
            private byte[] _sig;
            private LlcPacketType _packetType;
            private byte[] _zero;
            private Dlms m_root;
            private Dlms m_parent;
            public byte[] Sig { get { return _sig; } }
            public LlcPacketType PacketType { get { return _packetType; } }
            public byte[] Zero { get { return _zero; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms M_Parent { get { return m_parent; } }
        }
        public partial class UFrameControlByte : KaitaiStruct
        {
            public static UFrameControlByte FromFile(string fileName)
            {
                return new UFrameControlByte(new KaitaiStream(fileName));
            }

            public UFrameControlByte(KaitaiStream p__io, Dlms.ControlType p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _rType = m_io.ReadBitsInt(3);
                _pfBit = ((Dlms.HdlcPfBit) m_io.ReadBitsInt(1));
                _sType = m_io.ReadBitsInt(3);
            }
            private ulong _rType;
            private HdlcPfBit _pfBit;
            private ulong _sType;
            private Dlms m_root;
            private Dlms.ControlType m_parent;
            public ulong RType { get { return _rType; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public ulong SType { get { return _sType; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.ControlType M_Parent { get { return m_parent; } }
        }
        public partial class HdlcAddress : KaitaiStruct
        {
            public static HdlcAddress FromFile(string fileName)
            {
                return new HdlcAddress(new KaitaiStream(fileName));
            }

            public HdlcAddress(KaitaiStream p__io, Dlms.HdlcHeader p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
                _address = new VlqBase128Be(m_io);
            }
            private bool f_size;
            private int _size;
            public int Size
            {
                get
                {
                    if (f_size)
                        return _size;
                    _size = (int) (Address.Groups.Count);
                    f_size = true;
                    return _size;
                }
            }
            private VlqBase128Be _address;
            private Dlms m_root;
            private Dlms.HdlcHeader m_parent;
            public VlqBase128Be Address { get { return _address; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeader M_Parent { get { return m_parent; } }
        }
        public partial class HdlcHeader : KaitaiStruct
        {
            public static HdlcHeader FromFile(string fileName)
            {
                return new HdlcHeader(new KaitaiStream(fileName));
            }

            public HdlcHeader(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
                _openingFlag = m_io.ReadU1();
                __raw_format = m_io.ReadBytes(2);
                var io___raw_format = new KaitaiStream(__raw_format);
                _format = new FormatType(io___raw_format, this, m_root);
                _dstAddress = new HdlcAddress(m_io, this, m_root);
                _srcAddress = new HdlcAddress(m_io, this, m_root);
                __raw_control = m_io.ReadBytes(1);
                var io___raw_control = new KaitaiStream(__raw_control);
                _control = new ControlType(io___raw_control, this, m_root);
                if ( ((Control.FrameType == 0) || (Control.FrameType == 2)) ) {
                    _hcs = m_io.ReadU2be();
                }
            }
            private bool f_size;
            private int _size;
            public int Size
            {
                get
                {
                    if (f_size)
                        return _size;
                    _size = (int) ((((((1 + 2) + 1) + 2) + DstAddress.Size) + SrcAddress.Size));
                    f_size = true;
                    return _size;
                }
            }
            private byte _openingFlag;
            private FormatType _format;
            private HdlcAddress _dstAddress;
            private HdlcAddress _srcAddress;
            private ControlType _control;
            private ushort? _hcs;
            private Dlms m_root;
            private Dlms m_parent;
            private byte[] __raw_format;
            private byte[] __raw_control;
            public byte OpeningFlag { get { return _openingFlag; } }
            public FormatType Format { get { return _format; } }
            public HdlcAddress DstAddress { get { return _dstAddress; } }
            public HdlcAddress SrcAddress { get { return _srcAddress; } }
            public ControlType Control { get { return _control; } }
            public ushort? Hcs { get { return _hcs; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms M_Parent { get { return m_parent; } }
            public byte[] M_RawFormat { get { return __raw_format; } }
            public byte[] M_RawControl { get { return __raw_control; } }
        }
        public partial class ControlType : KaitaiStruct
        {
            public static ControlType FromFile(string fileName)
            {
                return new ControlType(new KaitaiStream(fileName));
            }

            public ControlType(KaitaiStream p__io, Dlms.HdlcHeader p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_controlByte = false;
                f_frameType = false;
                _read();
            }
            private void _read()
            {
                if ( ((FrameType == 0) || (FrameType == 2)) ) {
                    _iFrame = new IFrameControlByte(m_io, this, m_root);
                }
                if (FrameType == 1) {
                    _sFrame = new SFrameControlByte(m_io, this, m_root);
                }
                if (FrameType == 3) {
                    _uFrame = new UFrameControlByte(m_io, this, m_root);
                }
            }
            private bool f_controlByte;
            private byte _controlByte;
            public byte ControlByte
            {
                get
                {
                    if (f_controlByte)
                        return _controlByte;
                    long _pos = m_io.Pos;
                    m_io.Seek(0);
                    _controlByte = m_io.ReadU1();
                    m_io.Seek(_pos);
                    f_controlByte = true;
                    return _controlByte;
                }
            }
            private bool f_frameType;
            private FrameTypeEnum _frameType;
            public FrameTypeEnum FrameType
            {
                get
                {
                    if (f_frameType)
                        return _frameType;
                    _frameType = (FrameTypeEnum) (((Dlms.FrameTypeEnum) (ControlByte & 3)));
                    f_frameType = true;
                    return _frameType;
                }
            }
            private IFrameControlByte _iFrame;
            private SFrameControlByte _sFrame;
            private UFrameControlByte _uFrame;
            private Dlms m_root;
            private Dlms.HdlcHeader m_parent;
            public IFrameControlByte IFrame { get { return _iFrame; } }
            public SFrameControlByte SFrame { get { return _sFrame; } }
            public UFrameControlByte UFrame { get { return _uFrame; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeader M_Parent { get { return m_parent; } }
        }
        public partial class HdlcTrailer : KaitaiStruct
        {
            public static HdlcTrailer FromFile(string fileName)
            {
                return new HdlcTrailer(new KaitaiStream(fileName));
            }

            public HdlcTrailer(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
                _fsc = m_io.ReadBytes(2);
                _flag = m_io.ReadBytes(1);
            }
            private bool f_size;
            private sbyte _size;
            public sbyte Size
            {
                get
                {
                    if (f_size)
                        return _size;
                    _size = (sbyte) (3);
                    f_size = true;
                    return _size;
                }
            }
            private byte[] _fsc;
            private byte[] _flag;
            private Dlms m_root;
            private Dlms m_parent;
            public byte[] Fsc { get { return _fsc; } }
            public byte[] Flag { get { return _flag; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms M_Parent { get { return m_parent; } }
        }
        public partial class SFrameControlByte : KaitaiStruct
        {
            public static SFrameControlByte FromFile(string fileName)
            {
                return new SFrameControlByte(new KaitaiStream(fileName));
            }

            public SFrameControlByte(KaitaiStream p__io, Dlms.ControlType p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _recvSequenceNumber = m_io.ReadBitsInt(3);
                _pfBit = ((Dlms.HdlcPfBit) m_io.ReadBitsInt(1));
                _sFrameType = ((Dlms.SFrameType) m_io.ReadBitsInt(3));
            }
            private ulong _recvSequenceNumber;
            private HdlcPfBit _pfBit;
            private SFrameType _sFrameType;
            private Dlms m_root;
            private Dlms.ControlType m_parent;
            public ulong RecvSequenceNumber { get { return _recvSequenceNumber; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public SFrameType SFrameType { get { return _sFrameType; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.ControlType M_Parent { get { return m_parent; } }
        }
        public partial class IFrameControlByte : KaitaiStruct
        {
            public static IFrameControlByte FromFile(string fileName)
            {
                return new IFrameControlByte(new KaitaiStream(fileName));
            }

            public IFrameControlByte(KaitaiStream p__io, Dlms.ControlType p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _recvSequenceNumber = m_io.ReadBitsInt(3);
                _pfBit = ((Dlms.HdlcPfBit) m_io.ReadBitsInt(1));
                _sendSequenceNumber = m_io.ReadBitsInt(3);
            }
            private ulong _recvSequenceNumber;
            private HdlcPfBit _pfBit;
            private ulong _sendSequenceNumber;
            private Dlms m_root;
            private Dlms.ControlType m_parent;
            public ulong RecvSequenceNumber { get { return _recvSequenceNumber; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public ulong SendSequenceNumber { get { return _sendSequenceNumber; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.ControlType M_Parent { get { return m_parent; } }
        }
        public partial class FormatType : KaitaiStruct
        {
            public static FormatType FromFile(string fileName)
            {
                return new FormatType(new KaitaiStream(fileName));
            }

            public FormatType(KaitaiStream p__io, Dlms.HdlcHeader p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _frameFormatType = m_io.ReadBitsInt(4);
                _segmentationFlag = m_io.ReadBitsInt(1) != 0;
                _frameLength = m_io.ReadBitsInt(11);
            }
            private ulong _frameFormatType;
            private bool _segmentationFlag;
            private ulong _frameLength;
            private Dlms m_root;
            private Dlms.HdlcHeader m_parent;
            public ulong FrameFormatType { get { return _frameFormatType; } }
            public bool SegmentationFlag { get { return _segmentationFlag; } }
            public ulong FrameLength { get { return _frameLength; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeader M_Parent { get { return m_parent; } }
        }
        private bool f_size;
        private int _size;
        public int Size
        {
            get
            {
                if (f_size)
                    return _size;
                _size = (int) (M_Io.Size);
                f_size = true;
                return _size;
            }
        }
        private HdlcHeader _hdlcHeader;
        private LlcHeader _llcHeader;
        private byte[] _dlmsPdu;
        private HdlcTrailer _hdlcTrailer;
        private Dlms m_root;
        private KaitaiStruct m_parent;
        public HdlcHeader HdlcHeader { get { return _hdlcHeader; } }
        public LlcHeader LlcHeader { get { return _llcHeader; } }
        public byte[] DlmsPdu { get { return _dlmsPdu; } }
        public HdlcTrailer HdlcTrailer { get { return _hdlcTrailer; } }
        public Dlms M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
