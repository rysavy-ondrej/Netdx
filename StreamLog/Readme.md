# Streamlog - Temporal Datalog Engine with Semi-Naive Evaluation and Stratified Negation

Streamlog is a C# implementation of Datalog engine that performs semi-naive, bottom-up evaluation.
It has a fluent API through which it can be embedded in .NET applications to run queries.
It is based on Jatalog (https://github.com/wernsey/Jatalog)





We would like to use C# functions in Rules:

`
rel DnsQueryResponsePair(time: Float64, q: Packet, r: Packet) :-
        Frame(q.Time, _, q), Frame(r.Time, _, r), 
        packet_dns_query(q),
        packet_dns_response(r),
        packet_dns_id_eq(q,r),
        within(q_ts,r_ts,2.0).
`
Also, it should be possible to use built-in relations: `==, !=`.

Thus the predicate is referencing another rule or an expression that evaluates to True/False.

Rule
  Head: Predicate
  Body: [Predicate]