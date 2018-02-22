meta:
  id: dlms
  file-extension: dlms
  endian: be
seq:
  - id: hdlc_header
    type: hdlc_header
  - id: llc_header
    type: llc_header
    if: hdlc_header.control.frame_type.to_i == 0 or hdlc_header.control.frame_type.to_i == 2
  - id: dlms_pdu
    size: (hdlc_header.format.frame_length - hdlc_header.size) - 4
    type: dlms_type
  - id: hdlc_trailer
    type: hdlc_trailer
instances:
  size:
    value: _io.size
types:
  hdlc_header:
    seq:
      - id: opening_flag 
        type: u1
      - id: format
        type: format_type
        size: 2
      - id: dst_address
        type: hdlc_address
      - id: src_address
        type: hdlc_address
      - id: control
        type: control_type
        size: 1
      - id: hcs
        type: u2
        if: control.frame_type.to_i == 0 or control.frame_type.to_i == 2
    instances:
      size:
        value: 1 + 2 + dst_address.size + src_address.size + 1 + 2
  hdlc_trailer:
    seq:
      - id: fsc
        size: 2
      - id: flag
        size: 1
    instances:
      size:
        value: 3        
  dlms_type:
    seq:
      - id: pdu_type
        type: u1
        enum: dlms_pdu_type
      - id: pdu 
        size-eos: true
        type:
          switch-on: pdu_type
          cases:
            'dlms_pdu_type::get_request': dlms_get_request
  # http://www.dlms.com/faqanswers/questionsonthedlmscosemspecification/areexamplesforinformationexchangeavailable.php
  dlms_get_request:
    seq:
      - id: get_request_type # this identifies the structure, 1 = sequence
        type: u1
        enum: get_request_type
      - id: invoke
        type: invoke_priority
      - id: class_id
        size: 2
      - id: instance_id  # OBIS code ?
        size: 6
      - id: attribute_id
        size: 2
  invoke_priority:
    seq:
      - id: priority
        type: b1
      - id: service_class
        type: b1
      - id: invoke_id
        type: b6

  axdr_octet_string_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: axdr_octet_string
        if: present != 0
  axdr_octet_string:
    seq:
      - id: length
        type: u1
      - id: value
        type: str
        encoding: ascii
        size: length
  axdr_boolean_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: axdr_boolean
        if: present != 0
  axdr_boolean:
    seq:
      - id: value
        type: u1
        enum: boolean_type
        
  llc_header:
    seq:
      - id: sig
        contents: [ 0xe6 ]
      - id: packet_type
        type: u1
        enum: llc_packet_type
      - id: zero
        contents: [ 0 ]
    instances:
      size:
        value: 3
  format_type:
    seq:
      - id: frame_format_type
        type: b4
      - id: segmentation_flag
        type: b1
      - id: frame_length
        type: b11
  hdlc_address:
    seq:
      - id: addr_byte
        type: addr_byte_type
        repeat: until
        repeat-until: _.stop_bit == true 
    instances:
      size:
        value: 1
      # TODO: ensure that the length is at most 4 bytes
      # value - hodnota adresy 
  addr_byte_type:
    seq:
      - id: sap
        type: b6
      - id: cr
        type: b1
        enum: hdlc_cr
      - id: stop_bit
        type: b1
  control_type:
    seq:
      - id: i_frame
        type: i_frame_control_byte
        if: frame_type.to_i == 0 or frame_type.to_i == 2
      - id: s_frame
        type: s_frame_control_byte
        if: frame_type.to_i == 1
      - id: u_frame
        type: u_frame_control_byte
        if: frame_type.to_i == 3
    instances:
      control_byte:
        pos: 0
        type: u1
      frame_type:
        value: control_byte & 0x3
        enum: frame_type_enum
  i_frame_control_byte:
    seq:
      - id: recv_sequence_number
        type: b3
      - id: pf_bit
        type: b1
        enum: hdlc_pf_bit
      - id: send_sequence_number
        type: b3
  s_frame_control_byte:
    seq:
      - id: recv_sequence_number
        type: b3
      - id: pf_bit
        type: b1
        enum: hdlc_pf_bit
      - id: s_frame_type
        type: b3        
        enum: s_frame_type
  u_frame_control_byte:
    seq:
      - id: r_type
        type: b3
      - id: pf_bit
        type: b1
        enum: hdlc_pf_bit
      - id: s_type
        type: b3
enums:
  frame_type_enum:  
    0: i_frame
    1: s_frame
    2: i_frame
    3: u_frame
  s_frame_type:
    0: receiver_ready
    1: receiver_not_ready
    2: reject
    3: selective_reject
  hdlc_cr:
    0: hdlc_command
    1: hdcl_response
  llc_packet_type:
    0xe6: llc_command
    0xe7: llc_response
  hdlc_pf_bit:
    0: hdlc_poll
    1: hdlc_final
  dlms_pdu_type:
    1: initiate_request
    5: read_request 
    6: write_request
    8: initiate_response
    12: read_response
    13: write_response
    14: confirmed_service_error
    22: unconfirmed_write_request
    24: information_report_request
    192: get_request 
    193: set_request
    194: even_notification_request
    195: action_request
    196: get_response
    197: set_response
    199: action_response
  get_request_type:
    1: get_request_normal
    2: get_request_next
    3: get_request_with_list
  boolean_type:
    0: false
    1: true
    