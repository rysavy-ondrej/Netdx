// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild

using Kaitai;

namespace Netdx.Packets.Industrial
{
    public partial class DlmsPdu : KaitaiStruct
    {
        public static DlmsPdu FromFile(string fileName)
        {
            return new DlmsPdu(new KaitaiStream(fileName));
        }


        public enum DlmsPduType
        {
            InitiateRequest = 1,
            ReadRequest = 5,
            WriteRequest = 6,
            InitiateResponse = 8,
            ReadResponse = 12,
            WriteResponse = 13,
            ConfirmedServiceError = 14,
            UnconfirmedWriteRequest = 22,
            InformationReportRequest = 24,
            GetRequest = 192,
            SetRequest = 193,
            EvenNotificationRequest = 194,
            ActionRequest = 195,
            GetResponse = 196,
            SetResponse = 197,
            ActionResponse = 199,
        }
        public DlmsPdu(KaitaiStream p__io, KaitaiStruct p__parent = null, DlmsPdu p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _pduType = ((DlmsPduType) m_io.ReadU1());
            switch (PduType) {
            case DlmsPduType.GetResponse: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsGetResponse(io___raw_pdu);
                break;
            }
            case DlmsPduType.EvenNotificationRequest: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsEventNotificationRequest(io___raw_pdu);
                break;
            }
            case DlmsPduType.GetRequest: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsGetRequest(io___raw_pdu);
                break;
            }
            case DlmsPduType.SetResponse: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsSetResponse(io___raw_pdu);
                break;
            }
            case DlmsPduType.ActionResponse: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsActionResponse(io___raw_pdu);
                break;
            }
            case DlmsPduType.SetRequest: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsSetRequest(io___raw_pdu);
                break;
            }
            case DlmsPduType.ActionRequest: {
                __raw_pdu = m_io.ReadBytesFull();
                var io___raw_pdu = new KaitaiStream(__raw_pdu);
                _pdu = new DlmsActionRequest(io___raw_pdu);
                break;
            }
            default: {
                _pdu = m_io.ReadBytesFull();
                break;
            }
            }
        }
        private DlmsPduType _pduType;
        private object _pdu;
        private DlmsPdu m_root;
        private KaitaiStruct m_parent;
        private byte[] __raw_pdu;
        public DlmsPduType PduType { get { return _pduType; } }
        public object Pdu { get { return _pdu; } }
        public DlmsPdu M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
        public byte[] M_RawPdu { get { return __raw_pdu; } }
    }
}
