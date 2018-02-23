meta:
  id: snmp
  file-extension: pcap
  endian: be

seq:
  - id: message_type_tag
    contents: [ 0x30 ] 
    
  - id: message_length
    type: len_encoded
    
  - id: version
    type: asn1_ber
  
  - id: community
    type: asn1_ber
    
  - id: data
    type: asn1_ber
    
types:
  asn1_ber:
    seq:
    - id: type_tag
      type: u1
      enum: type_tag
    - id: len
      type: len_encoded
    - id: body
      size: len.result
      type:
        switch-on: type_tag
        cases:
          'type_tag::integer': body_integer
          'type_tag::sequence_10': body_sequence
          'type_tag::sequence_30': body_sequence
          'type_tag::set': body_sequence
          'type_tag::utf8string': body_utf8string
          'type_tag::printable_string': body_printable_string
          'type_tag::snmp_request': snmp_request
          'type_tag::snmp_response': snmp_response
          

  snmp_request:
    seq:
      - id: request_id
        type: asn1_ber
      - id: error_status
        type: asn1_ber
      - id: error_index
        type: asn1_ber
      - id: variable_bindings
        type: asn1_ber
    instances:
      request_id_value:
        value: request_id.body
  snmp_response:
    seq:
      - id: request_id
        type: asn1_ber
      - id: error_status
        type: asn1_ber
      - id: error_index
        type: asn1_ber
      - id: variable_bindings
        type: asn1_ber
    
  len_encoded:
    seq:
      - id: b1
        type: u1
      - id: int2
        type: u2be
        if: b1 == 0x82
    instances:
      result:
        value: '(b1 & 0x80 == 0) ? b1 : int2'
  body_sequence:
    seq:
      - id: entries
        type: asn1_ber
        repeat: eos
  body_utf8string:
    seq:
      - id: str
        type: str
        size-eos: true
        encoding: UTF-8
  body_printable_string:
    seq:
      - id: str
        type: str
        size-eos: true
        encoding: ASCII # actually a subset of ASCII
        
enums:
  type_tag:
    0: end_of_content
    0x1: boolean
    0x2: integer
    0x3: bit_string
    0x4: octet_string
    0x5: null_value
    0x6: object_id
    0x7: object_descriptor
    0x8: external
    0x9: real
    0xa: enumerated
    0xb: embedded_pdv
    0xc: utf8string
    0xd: relative_oid
    0x10: sequence_10
    0x13: printable_string
    0x16: ia5string
    0x30: sequence_30
    0x31: set
    0xa0: snmp_request
    0xa2: snmp_response
