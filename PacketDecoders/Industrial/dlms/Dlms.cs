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
            _hdlcHeader = new HdlcHeaderFields(m_io, this, m_root);
            if ((HdlcHeader.Control.FrameType & 1) == 0) {
                _llcHeader = new LlcHeaderFields(m_io, this, m_root);
            }
            __raw_dlmsPdu = m_io.ReadBytes(((HdlcHeader.Format.FrameLength - HdlcHeader.Size) - 4));
            var io___raw_dlmsPdu = new KaitaiStream(__raw_dlmsPdu);
            _dlmsPdu = new DlmsPdu(io___raw_dlmsPdu);
            _hdlcTrailer = new HdlcTrailerFields(m_io, this, m_root);
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
        public partial class HdlcHeaderFields : KaitaiStruct
        {
            public static HdlcHeaderFields FromFile(string fileName)
            {
                return new HdlcHeaderFields(new KaitaiStream(fileName));
            }

            public HdlcHeaderFields(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
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
                if ((Control.FrameType & 1) == 0) {
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
        public partial class HdlcAddress : KaitaiStruct
        {
            public static HdlcAddress FromFile(string fileName)
            {
                return new HdlcAddress(new KaitaiStream(fileName));
            }

            public HdlcAddress(KaitaiStream p__io, Dlms.HdlcHeaderFields p__parent = null, Dlms p__root = null) : base(p__io)
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
            private Dlms.HdlcHeaderFields m_parent;
            public VlqBase128Be Address { get { return _address; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeaderFields M_Parent { get { return m_parent; } }
        }
        public partial class ControlType : KaitaiStruct
        {
            public static ControlType FromFile(string fileName)
            {
                return new ControlType(new KaitaiStream(fileName));
            }

            public ControlType(KaitaiStream p__io, Dlms.HdlcHeaderFields p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_controlByte = false;
                f_frameType = false;
                _read();
            }
            private void _read()
            {
                if ((FrameType & 1) == 0) {
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
            private int _frameType;
            public int FrameType
            {
                get
                {
                    if (f_frameType)
                        return _frameType;
                    _frameType = (int) ((ControlByte & 3));
                    f_frameType = true;
                    return _frameType;
                }
            }
            private IFrameControlByte _iFrame;
            private SFrameControlByte _sFrame;
            private UFrameControlByte _uFrame;
            private Dlms m_root;
            private Dlms.HdlcHeaderFields m_parent;
            public IFrameControlByte IFrame { get { return _iFrame; } }
            public SFrameControlByte SFrame { get { return _sFrame; } }
            public UFrameControlByte UFrame { get { return _uFrame; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeaderFields M_Parent { get { return m_parent; } }
        }
        public partial class LlcHeaderFields : KaitaiStruct
        {
            public static LlcHeaderFields FromFile(string fileName)
            {
                return new LlcHeaderFields(new KaitaiStream(fileName));
            }

            public LlcHeaderFields(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
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
        public partial class HdlcTrailerFields : KaitaiStruct
        {
            public static HdlcTrailerFields FromFile(string fileName)
            {
                return new HdlcTrailerFields(new KaitaiStream(fileName));
            }

            public HdlcTrailerFields(KaitaiStream p__io, Dlms p__parent = null, Dlms p__root = null) : base(p__io)
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

            public FormatType(KaitaiStream p__io, Dlms.HdlcHeaderFields p__parent = null, Dlms p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_frameLength = false;
                _read();
            }
            private void _read()
            {
                _frameFormatType = m_io.ReadBitsInt(4);
                _segmentationFlag = m_io.ReadBitsInt(1) != 0;
                _length = m_io.ReadBitsInt(11);
            }
            private bool f_frameLength;
            private int _frameLength;
            public int FrameLength
            {
                get
                {
                    if (f_frameLength)
                        return _frameLength;
                    _frameLength = (int) ((Length + 0));
                    f_frameLength = true;
                    return _frameLength;
                }
            }
            private ulong _frameFormatType;
            private bool _segmentationFlag;
            private ulong _length;
            private Dlms m_root;
            private Dlms.HdlcHeaderFields m_parent;
            public ulong FrameFormatType { get { return _frameFormatType; } }
            public bool SegmentationFlag { get { return _segmentationFlag; } }
            public ulong Length { get { return _length; } }
            public Dlms M_Root { get { return m_root; } }
            public Dlms.HdlcHeaderFields M_Parent { get { return m_parent; } }
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
        private HdlcHeaderFields _hdlcHeader;
        private LlcHeaderFields _llcHeader;
        private DlmsPdu _dlmsPdu;
        private HdlcTrailerFields _hdlcTrailer;
        private Dlms m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_dlmsPdu;
        public HdlcHeaderFields HdlcHeader { get { return _hdlcHeader; } }
        public LlcHeaderFields LlcHeader { get { return _llcHeader; } }
        public DlmsPdu DlmsPdu { get { return _dlmsPdu; } }
        public HdlcTrailerFields HdlcTrailer { get { return _hdlcTrailer; } }
        public Dlms M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawDlmsPdu { get { return __raw_dlmsPdu; } }
    }
}
