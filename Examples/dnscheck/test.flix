/* 
 * PACKET
 */ 
enum Packet {
    case Undefined,
    case DnsPacket(Float64, Int, DnsPacketQR)
}

def getPacketTs(f:Packet) : Float64 = 
    match (f) with {
        case DnsPacket(ts,_,_) => ts    
        case _ => 0.0
    }
// tests if the timestamp of q is within (p.ts + delta) interval
def within(p:Packet, q:Packet, delta:Float64) : Bool =
    let p_ts = getPacketTs(p);
    let q_ts = getPacketTs(q);
    q_ts > p_ts && q_ts <= p_ts + delta 

/*
 * DNS
 */
enum ResponseCode {
    case NoError, 
    case NameDoesNotExist
}

enum DnsPacketQR {
    case Query,
    case Response(ResponseCode)
}

def getDnsPacketId(p:Packet) : Int = 
    match (p) with {
        case DnsPacket(_,id,_) => id    
        case _ => 0
    }

def getDnsPacketQr(p:Packet) : Option[DnsPacketQR] = 
    match (p) with {
        case DnsPacket(_,_,qr) => Some(qr)    
        case _ => None
    }

def dnsIdEq(p: Packet, q: Packet): Bool = 
    match (p,q) with {
        case (DnsPacket(_,pId,_), DnsPacket(_,qId,_)) => pId == qId
        case _ => false 
    }

def isDnsQuery(p:Packet) : Bool =
    match (getDnsPacketQr(p)) with {
        case Some(Query) => true
        case _ => false
    }

def isDnsResponse(p:Packet) : Bool =
    match (getDnsPacketQr(p)) with {
        case Some(Response(_)) => true
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
    isDnsQuery(q),
    isDnsResponse(r),
    dnsIdEq(q,r),
    within(q,r,2.0).

/*
 * MODEL
 */
Frame(DnsPacket(0.1, 1234, Query)).
Frame(DnsPacket(0.6, 1234, Response(NoError))).

