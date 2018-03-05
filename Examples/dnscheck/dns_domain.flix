/* 
 * PACKET
 */ 
enum Packet {
    case Undefined,
    case DnsPacket(Float64, Str, Str, Int, DnsPacketData)   // (frame.time, ip.src, ip.dst, dns.id)
}


def frame_time(f:Packet) : Float64 = 
    match (f) with {
        case DnsPacket(ts,_,_,_,_) => ts    
        case _ => 0.0
    }

// tests if the timestamp of q is within (p.ts + delta) interval
def within(p:Packet, q:Packet, delta:Float64) : Bool =
    let p_ts = frame_time(p);
    let q_ts = frame_time(q);
    q_ts > p_ts && q_ts <= p_ts + delta 

/*
 * DNS
 */
enum ResponseCode {
    case NoError, 
    case NameDoesNotExist
}

enum DnsPacketData {
    case Query(List[DnsRecord]),
    case Response(ResponseCode, List[DnsRecord], List[DnsRecord])
}

enum DnsRecord {
    case NULL(Str),           // domainname -> ()
    case A(Str, Str),         // domainname -> (address)
    case AAAA(Str, Str),      // domainname -> (address)
    case MX(Str, Int, Str),   // domainname -> (priority, hostname)
    case NS(Str, Str),        // domainname -> (hostname)
    case PTR(Str, Str),        // domainname -> (hostname)
    case CNAME(Str,Str)       // domainname -> (hostname)
}

def dns_id(p:Packet) : Int = 
    match (p) with {
        case DnsPacket(_,_,_,id,_) => id    
        case _ => 0
    }

def dns_data(p:Packet) : Option[DnsPacketData] = 
    match (p) with {
        case DnsPacket(_,_,_,_,data) => Some(data)    
        case _ => None
    }

def dns_id_eq(p: Packet, q: Packet): Bool = 
    match (p,q) with {
        case (DnsPacket(_,_,_,pId,_), DnsPacket(_,_,_,qId,_)) => pId == qId
        case _ => false 
    }

def dns_query(p:Packet) : Bool =
    match (dns_data(p)) with {
        case Some(Query(_)) => true
        case _ => false
    }

def dns_response(p:Packet) : Bool =
    match (dns_data(p)) with {
        case Some(Response(_,_,_)) => true
        case _ => false
    }
  
def dns_nameDoesNotExistError(p:Packet) : Bool =
    match (dns_data(p)) with {
        case Some(Response(NameDoesNotExist,_,_)) => true
        case _ => false
    }    

/*
 * DOMAIN RULES
 *
 */
rel Frame(f: Packet)


rel DnsQueryResponsePair(q: Packet, r: Packet)
DnsQueryResponsePair(q,r) :- 
    Frame(q), Frame(r), 
    dns_query(q),
    dns_response(r),
    dns_id_eq(q,r),
    within(q,r,2.0).

rel DnsErrorResponse(r: Packet)
DnsErrorResponse(r) :-
    Frame(r),
    dns_nameDoesNotExistError(r).

