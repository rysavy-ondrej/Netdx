/* 
 * PACKET
 */ 
enum Packet {
    case Undefined,
    case DNS(DnsPacket),
    case ICMP(IcmpPacket)
}

def dns(p:Packet) : Option[DnsPacket] = 
    match (p) with {
        case DNS(p) => Some(p)    
        case _ => None
    }
def icmp(p:Packet) : Option[IcmpPacket] = 
    match (p) with {
        case ICMP(p) => Some(p)
        case _ => None
    }

enum Flow {
    // (ip.src, ip.dst, udp.srcport, udp.dstport)
    case UDP(Str, Str, Int, Int),
    // (ip.src, ip.dst, tcp.srcport, tcp.dstport)
    case TCP(Str, Str, Int, Int)
}

// tests if the timestamp of q is within (p.time + delta) interval
def within(p_time:Float64, q_time:Float64, delta:Float64) : Bool =
    q_time > p_time && q_time <= p_time + delta 

/*
 * Dns
 */
enum ResponseCode {
    case NoError, 
    case NameDoesNotExist
}

enum DnsPacket {
    case Query(Int32, List[DnsRecord]),
    case Response(Int32, ResponseCode, List[DnsRecord], List[DnsRecord])
}

enum DnsRecord {
    case NULL(Str),           // domainname -> ()
    case A(Str, Str),         // domainname -> (address)
    case AAAA(Str, Str),      // domainname -> (address)
    case MX(Str, Int, Str),   // domainname -> (priority, hostname)
    case NS(Str, Str),        // domainname -> (hostname)
    case PTR(Str, Str),       // domainname -> (hostname)
    case CNAME(Str,Str)       // domainname -> (hostname)
}

def dns_id(p:Option[DnsPacket]) : Int = 
    match (p) with {
        case Some(Query(id,_)) => id    
        case Some(Response(id,_,_,_)) => id
        case _ => 0
    }



def dns_id_eq(p: Option[DnsPacket], q: Option[DnsPacket]): Bool = 
    match (p,q) with {
        case (Some(pDns),Some(qDns)) => dns_id(p) == dns_id(q)
        case _ => false
    }
    
    

def dns_query(p:Option[DnsPacket]) : Bool =
    match (p) with {
        case Some(Query(_,_)) => true
        case _ => false
    }

def dns_response(p:Option[DnsPacket]) : Bool =
    match (p) with {
        case Some(Response(_,_,_,_)) => true
        case _ => false
    }
  


def dns_nameDoesNotExistError(p:Option[DnsPacket]) : Bool =
    match (p) with {
        case Some(Response(_, NameDoesNotExist,_,_)) => true
        case _ => false
    }    

/*
 * Icmp
 */
enum IcmpPacket {
    case EchoRequest,
    case EchoReplay,
    case Unreachable(IcmpUnreachable),
    case TimeExceeded
}

enum IcmpUnreachable {
    case Net, 
    case Host, 
    case Protocol, 
    case Port
}

def packet_dns_query(q:Packet) : Bool =
    dns_query(dns(q))
def packet_dns_response(p:Packet) : Bool =
    dns_response(dns(p))
def packet_dns_id_eq(q:Packet, r:Packet) : Bool = 
    dns_id_eq(dns(q),dns(r))

/*
 * DOMAIN RULES
 */
rel Frame(ts: Float64, f: Flow, p: Packet)

rel DnsQueryResponsePair(t: Float64, q: Packet, r: Packet)
    DnsQueryResponsePair(q_ts, q,r) :- 
        Frame(q_ts, _, q), Frame(r_ts, _, r), 
        packet_dns_query(q),
        packet_dns_response(r),
        packet_dns_id_eq(q,r),
        within(q_ts,r_ts,2.0).

// Examples:
Frame(1520333364.529, UDP("192.168.111.100","147.229.9.43",56726,53), DNS(Query(10086,A("www.google.com.","")::Nil))).
Frame(1520333364.529, UDP("147.229.9.43","192.168.111.100",53,56726), DNS(Response(10086,NoError,A("www.google.com.","")::Nil,A("www.google.com.","216.58.201.68")::Nil))).
Frame(1520333370.148, UDP("192.168.111.100","147.229.9.43",64702,53), DNS(Query(8951,A("play.google.com.","")::Nil))).
Frame(1520333370.148, UDP("192.168.111.100","147.229.9.43",53197,53), DNS(Query(25329,A("fonts.gstatic.com.","")::Nil))).
Frame(1520333370.149, UDP("147.229.9.43","192.168.111.100",53,64702), DNS(Response(8951,NoError,A("play.google.com.","")::Nil,CNAME("play.google.com.","play.l.google.com.")::A("play.l.google.com.","172.217.23.206")::Nil))).
Frame(1520333370.149, UDP("147.229.9.43","192.168.111.100",53,53197), DNS(Response(25329,NoError,A("fonts.gstatic.com.","")::Nil,CNAME("fonts.gstatic.com.","gstaticadssl.l.google.com.")::A("gstaticadssl.l.google.com.","172.217.23.195")::Nil))).
