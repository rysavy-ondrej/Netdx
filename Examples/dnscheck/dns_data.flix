/*
 * MODEL
 * flix --print DnsQueryResponsePair dns_domain.flix dns_data.flix
 */
Frame(DnsPacket(0.1, 1234, Query)).
Frame(DnsPacket(0.6, 1234, Response(NoError))).

Frame(DnsPacket(0.7, 1325, Query)).
Frame(DnsPacket(0.9, 1325, Response(NoError))).