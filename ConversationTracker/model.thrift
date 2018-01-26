//namespace netcore ConversationTracker
namespace csharp Netdx.ConversationTracker


/// <summary>
///	Specifies the flow orientation if known.
/// </summary>
enum FlowOrientation { Undefined = 0; Upflow = 1; Downflow = 2;  }

enum ProtocolType {
        /// <summary> IPv6 Hop-by-Hop options. </summary>
        IP = 0;
        /// <summary> Internet Control Message Protocol. </summary>
        ICMP = 1;
        /// <summary> Internet Group Management Protocol.</summary>
        IGMP = 2;
        /// <summary> IPIP tunnels (older KA9Q tunnels use 94). </summary>
        IPIP = 4;
        /// <summary> Transmission Control Protocol. </summary>
        TCP = 6;
        /// <summary> Exterior Gateway Protocol. </summary>
        EGP = 8;
        /// <summary> PUP protocol. </summary>
        PUP = 12;
        /// <summary> User Datagram Protocol. </summary>
        UDP = 17;
        /// <summary> XNS IDP protocol. </summary>
        IDP = 22;
        /// <summary> SO Transport Protocol Class 4. </summary>
        TP = 29;
        /// <summary> IPv6 header. </summary>
        IPv6 = 41;
        /// <summary> IPv6 routing header. </summary>
        ROUTING = 43;
        /// <summary> IPv6 fragmentation header. </summary>
        FRAGMENT = 44;
        /// <summary> Reservation Protocol. </summary>
        RSVP = 46;
        /// <summary> General Routing Encapsulation. </summary>
        GRE = 47;
        /// <summary> Encapsulating security payload. </summary>
        ESP = 50;
        /// <summary> Authentication header. </summary>
        AH = 51;
        /// <summary> ICMPv6. </summary>
        ICMPV6 = 58;
        /// <summary> IPv6 no next header. </summary>
        NONE = 59;
        /// <summary> IPv6 destination options. </summary>
        DSTOPTS = 60;
        /// <summary> Multicast Transport Protocol. </summary>
        MTP = 92;
        /// <summary> Encapsulation Header. </summary>
        ENCAP = 98;
        /// <summary> Protocol Independent Multicast. </summary>
        PIM = 103;
        /// <summary> Compression Header Protocol. </summary>
        COMP = 108;
        
        RAW = 255;
}	

struct FlowKey {
    1: ProtocolType Protocol;
    2: binary SourcePoint;  
    3: binary DestinationPoint;
}

struct FlowRecord {    
    1: i64 FirstSeen;
    2: i64 LastSeen;
    3: i64 Octets;
    4: i32 Packets;
}

// Conversation object. 
struct Conversation {
    1: FlowKey ConversationKey;
    2: i32 ConversationId;
    3: i32 ParentId;
    4: i64 FirstSeen;
    5: i64 LastSeen;
} 

struct PacketMetrics {
    1: i64 Timeval;
    2: i32 SegmentSize;
}

// The structure stores features as proposed in http://www.cl.cam.ac.uk/~awm22/publications/li2007machine.pdf
struct TcpFlowFeaturesRecord : FlowRecord {
    // Count of all packets with push bit set in TCP header
    1: i32 PushTcp;  
    // The total number of bytes sent in initial window
    2: i32 InitWindowsBytes;    
    // Total numbers of RTT samples found.
    3: i32 RttSamples;  
    // Collection of packet metrics to be used for compute other values.
    4: list<PacketMetrics> Packets;
}