// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;
using System.Collections.Generic;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsHdlc : KaitaiStruct
    {
        public static DlmsHdlc FromFile(string fileName)
        {
            return new DlmsHdlc(new KaitaiStream(fileName));
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
        public DlmsHdlc(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsHdlc p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            f_size = false;
            _read();
        }
        private void _read()
        {
            _startFlag = m_io.EnsureFixedContents(new byte[] { 126 });
            _hdlcHeader = new HdlcHeaderFields(m_io, this, m_root);
            if ((HdlcHeader.Control.FrameType & 1) == 0) {
                _llcHeader = new LlcHeaderFields(m_io, this, m_root);
            }
            _dataPdu = m_io.ReadBytes((HdlcHeader.Format.FrameLength - ((((HdlcHeader.Control.FrameType & 1) == 0 ? HdlcHeader.Size : 0) + LlcHeader.Size) + 2)));
            _fsc = m_io.ReadBytes(2);
            _stopFlag = m_io.EnsureFixedContents(new byte[] { 126 });
        }
        public partial class UFrameControlByte : KaitaiStruct
        {
            public static UFrameControlByte FromFile(string fileName)
            {
                return new UFrameControlByte(new KaitaiStream(fileName));
            }

            public UFrameControlByte(KaitaiStream p__io, DlmsHdlc.ControlType p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _rType = m_io.ReadBitsInt(3);
                _pfBit = ((DlmsHdlc.HdlcPfBit) m_io.ReadBitsInt(1));
                _sType = m_io.ReadBitsInt(3);
            }
            private ulong _rType;
            private HdlcPfBit _pfBit;
            private ulong _sType;
            private DlmsHdlc m_root;
            private DlmsHdlc.ControlType m_parent;
            public ulong RType { get { return _rType; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public ulong SType { get { return _sType; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.ControlType M_Parent { get { return m_parent; } }
        }

        /// <summary>
        /// One byte group, clearly divided into 7-bit &quot;value&quot; and 1-bit &quot;has continuation
        /// in the next byte&quot; flag. contiune with next byte = 0, final byte = 1.
        /// </summary>
        public partial class HdlcAddressByte : KaitaiStruct
        {
            public static HdlcAddressByte FromFile(string fileName)
            {
                return new HdlcAddressByte(new KaitaiStream(fileName));
            }

            public HdlcAddressByte(KaitaiStream p__io, DlmsHdlc.HdlcAddress p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_hasNext = false;
                f_value = false;
                _read();
            }
            private void _read()
            {
                _b = m_io.ReadU1();
            }
            private bool f_hasNext;
            private bool _hasNext;

            /// <summary>
            /// If true, then we have more bytes to read
            /// </summary>
            public bool HasNext
            {
                get
                {
                    if (f_hasNext)
                        return _hasNext;
                    _hasNext = (bool) ((B & 1) == 0);
                    f_hasNext = true;
                    return _hasNext;
                }
            }
            private bool f_value;
            private int _value;

            /// <summary>
            /// The 7-bit (base128) numeric value of this group
            /// </summary>
            public int Value
            {
                get
                {
                    if (f_value)
                        return _value;
                    _value = (int) (((B & 254) >> 1));
                    f_value = true;
                    return _value;
                }
            }
            private byte _b;
            private DlmsHdlc m_root;
            private DlmsHdlc.HdlcAddress m_parent;
            public byte B { get { return _b; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.HdlcAddress M_Parent { get { return m_parent; } }
        }
        public partial class HdlcHeaderFields : KaitaiStruct
        {
            public static HdlcHeaderFields FromFile(string fileName)
            {
                return new HdlcHeaderFields(new KaitaiStream(fileName));
            }

            public HdlcHeaderFields(KaitaiStream p__io, DlmsHdlc p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
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
                    _size = (int) (((((2 + DstAddress.Size) + SrcAddress.Size) + 1) + ((Control.FrameType & 1) == 0 ? 2 : 0)));
                    f_size = true;
                    return _size;
                }
            }
            private FormatType _format;
            private HdlcAddress _dstAddress;
            private HdlcAddress _srcAddress;
            private ControlType _control;
            private ushort? _hcs;
            private DlmsHdlc m_root;
            private DlmsHdlc m_parent;
            private byte[] __raw_format;
            private byte[] __raw_control;
            public FormatType Format { get { return _format; } }
            public HdlcAddress DstAddress { get { return _dstAddress; } }
            public HdlcAddress SrcAddress { get { return _srcAddress; } }
            public ControlType Control { get { return _control; } }
            public ushort? Hcs { get { return _hcs; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc M_Parent { get { return m_parent; } }
            public byte[] M_RawFormat { get { return __raw_format; } }
            public byte[] M_RawControl { get { return __raw_control; } }
        }
        public partial class HdlcAddress : KaitaiStruct
        {
            public static HdlcAddress FromFile(string fileName)
            {
                return new HdlcAddress(new KaitaiStream(fileName));
            }

            public HdlcAddress(KaitaiStream p__io, DlmsHdlc.HdlcHeaderFields p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                f_last = false;
                f_value = false;
                _read();
            }
            private void _read()
            {
                _bytes = new List<HdlcAddressByte>();
                {
                    var i = 0;
                    HdlcAddressByte M_;
                    do {
                        M_ = new HdlcAddressByte(m_io, this, m_root);
                        _bytes.Add(M_);
                        i++;
                    } while (!(!(M_.HasNext)));
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
                    _size = (int) (Bytes.Count);
                    f_size = true;
                    return _size;
                }
            }
            private bool f_last;
            private int _last;
            public int Last
            {
                get
                {
                    if (f_last)
                        return _last;
                    _last = (int) ((Bytes.Count - 1));
                    f_last = true;
                    return _last;
                }
            }
            private bool f_value;
            private int _value;

            /// <summary>
            /// Resulting value as normal integer
            /// </summary>
            public int Value
            {
                get
                {
                    if (f_value)
                        return _value;
                    _value = (int) ((((((((Bytes[Last].Value + (Last >= 1 ? (Bytes[(Last - 1)].Value << 7) : 0)) + (Last >= 2 ? (Bytes[(Last - 2)].Value << 14) : 0)) + (Last >= 3 ? (Bytes[(Last - 3)].Value << 21) : 0)) + (Last >= 4 ? (Bytes[(Last - 4)].Value << 28) : 0)) + (Last >= 5 ? (Bytes[(Last - 5)].Value << 35) : 0)) + (Last >= 6 ? (Bytes[(Last - 6)].Value << 42) : 0)) + (Last >= 7 ? (Bytes[(Last - 7)].Value << 49) : 0)));
                    f_value = true;
                    return _value;
                }
            }
            private List<HdlcAddressByte> _bytes;
            private DlmsHdlc m_root;
            private DlmsHdlc.HdlcHeaderFields m_parent;
            public List<HdlcAddressByte> Bytes { get { return _bytes; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.HdlcHeaderFields M_Parent { get { return m_parent; } }
        }
        public partial class ControlType : KaitaiStruct
        {
            public static ControlType FromFile(string fileName)
            {
                return new ControlType(new KaitaiStream(fileName));
            }

            public ControlType(KaitaiStream p__io, DlmsHdlc.HdlcHeaderFields p__parent = null, DlmsHdlc p__root = null) : base(p__io)
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
            private DlmsHdlc m_root;
            private DlmsHdlc.HdlcHeaderFields m_parent;
            public IFrameControlByte IFrame { get { return _iFrame; } }
            public SFrameControlByte SFrame { get { return _sFrame; } }
            public UFrameControlByte UFrame { get { return _uFrame; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.HdlcHeaderFields M_Parent { get { return m_parent; } }
        }
        public partial class LlcHeaderFields : KaitaiStruct
        {
            public static LlcHeaderFields FromFile(string fileName)
            {
                return new LlcHeaderFields(new KaitaiStream(fileName));
            }

            public LlcHeaderFields(KaitaiStream p__io, DlmsHdlc p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_size = false;
                _read();
            }
            private void _read()
            {
                _remoteLsap = m_io.EnsureFixedContents(new byte[] { 230 });
                _localLsap = ((DlmsHdlc.LlcPacketType) m_io.ReadU1());
                _llcQuality = m_io.EnsureFixedContents(new byte[] { 0 });
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
            private byte[] _remoteLsap;
            private LlcPacketType _localLsap;
            private byte[] _llcQuality;
            private DlmsHdlc m_root;
            private DlmsHdlc m_parent;
            public byte[] RemoteLsap { get { return _remoteLsap; } }
            public LlcPacketType LocalLsap { get { return _localLsap; } }
            public byte[] LlcQuality { get { return _llcQuality; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc M_Parent { get { return m_parent; } }
        }
        public partial class SFrameControlByte : KaitaiStruct
        {
            public static SFrameControlByte FromFile(string fileName)
            {
                return new SFrameControlByte(new KaitaiStream(fileName));
            }

            public SFrameControlByte(KaitaiStream p__io, DlmsHdlc.ControlType p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _recvSequenceNumber = m_io.ReadBitsInt(3);
                _pfBit = ((DlmsHdlc.HdlcPfBit) m_io.ReadBitsInt(1));
                _sFrameType = ((DlmsHdlc.SFrameType) m_io.ReadBitsInt(3));
            }
            private ulong _recvSequenceNumber;
            private HdlcPfBit _pfBit;
            private SFrameType _sFrameType;
            private DlmsHdlc m_root;
            private DlmsHdlc.ControlType m_parent;
            public ulong RecvSequenceNumber { get { return _recvSequenceNumber; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public SFrameType SFrameType { get { return _sFrameType; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.ControlType M_Parent { get { return m_parent; } }
        }
        public partial class IFrameControlByte : KaitaiStruct
        {
            public static IFrameControlByte FromFile(string fileName)
            {
                return new IFrameControlByte(new KaitaiStream(fileName));
            }

            public IFrameControlByte(KaitaiStream p__io, DlmsHdlc.ControlType p__parent = null, DlmsHdlc p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _recvSequenceNumber = m_io.ReadBitsInt(3);
                _pfBit = ((DlmsHdlc.HdlcPfBit) m_io.ReadBitsInt(1));
                _sendSequenceNumber = m_io.ReadBitsInt(3);
            }
            private ulong _recvSequenceNumber;
            private HdlcPfBit _pfBit;
            private ulong _sendSequenceNumber;
            private DlmsHdlc m_root;
            private DlmsHdlc.ControlType m_parent;
            public ulong RecvSequenceNumber { get { return _recvSequenceNumber; } }
            public HdlcPfBit PfBit { get { return _pfBit; } }
            public ulong SendSequenceNumber { get { return _sendSequenceNumber; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.ControlType M_Parent { get { return m_parent; } }
        }
        public partial class FormatType : KaitaiStruct
        {
            public static FormatType FromFile(string fileName)
            {
                return new FormatType(new KaitaiStream(fileName));
            }

            public FormatType(KaitaiStream p__io, DlmsHdlc.HdlcHeaderFields p__parent = null, DlmsHdlc p__root = null) : base(p__io)
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
            private DlmsHdlc m_root;
            private DlmsHdlc.HdlcHeaderFields m_parent;
            public ulong FrameFormatType { get { return _frameFormatType; } }
            public bool SegmentationFlag { get { return _segmentationFlag; } }
            public ulong Length { get { return _length; } }
            public DlmsHdlc M_Root { get { return m_root; } }
            public DlmsHdlc.HdlcHeaderFields M_Parent { get { return m_parent; } }
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
        private byte[] _startFlag;
        private HdlcHeaderFields _hdlcHeader;
        private LlcHeaderFields _llcHeader;
        private byte[] _dataPdu;
        private byte[] _fsc;
        private byte[] _stopFlag;
        private DlmsHdlc m_root;
        private KaitaiStruct m_parent;
        public byte[] StartFlag { get { return _startFlag; } }
        public HdlcHeaderFields HdlcHeader { get { return _hdlcHeader; } }
        public LlcHeaderFields LlcHeader { get { return _llcHeader; } }

        /// <summary>
        /// frame_length in hdlc header gives the total lenght of the hdlc frame without start/stop flags
        /// to get the length of encapsulated message, we substract size of hdlc and llc headers 
        /// and hdlc trailer.
        /// </summary>
        public byte[] DataPdu { get { return _dataPdu; } }
        public byte[] Fsc { get { return _fsc; } }
        public byte[] StopFlag { get { return _stopFlag; } }
        public DlmsHdlc M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
