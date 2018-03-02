meta:
  id: dlms
  endian: be
  imports: 
    - dlms_pdu
    - vlq_base128_be
seq:
  - id: hdlc_header
    type: hdlc_header_fields
  - id: llc_header
    type: llc_header_fields
    if: hdlc_header.control.frame_type & 0x1 == 0 

  - id: dlms_pdu
    size: (hdlc_header.format.frame_length - hdlc_header.size) - 4
    type: dlms_pdu

  - id: hdlc_trailer
    type: hdlc_trailer_fields
instances:
  size:
    value: _io.size
types:
  hdlc_header_fields:
    seq:
      - id: opening_flag 
        contents: [ 0x7e ]
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
        if: control.frame_type & 0x1 == 0 
    instances:
      size:
        value: 1 + 2 + 1 + 2 + dst_address.size + src_address.size
  hdlc_trailer_fields:
    seq:
      - id: fsc
        size: 2
      - id: flag
        size: 1
    instances:
      size:
        value: 3        
  llc_header_fields:
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
      - id: length
        type: b11
    instances:
      frame_length:
        value: length + 0 # this should convert length from ulong to int
  hdlc_address:
    seq:
      - id: address
        type: vlq_base128_be
    instances:
      size:
        value: address.groups.size
  control_type:
    seq:
      - id: i_frame
        type: i_frame_control_byte
        if: frame_type & 0x1 == 0 
      - id: s_frame
        type: s_frame_control_byte
        if: frame_type == 1
      - id: u_frame
        type: u_frame_control_byte
        if: frame_type == 3
    instances:
      control_byte:
        pos: 0
        type: u1
      frame_type:
        value: control_byte & 0x3
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

    
