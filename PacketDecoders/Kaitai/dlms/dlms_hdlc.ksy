meta:
  id: dlms_hdlc
  endian: be
  imports: 
    - vlq_base128_be
seq:
  - id: start_flag 
    contents: [ 0x7e ]

  - id: hdlc_header
    type: hdlc_header_fields
    
  - id: llc_header
    type: llc_header_fields
    if: hdlc_header.control.frame_type & 0x1 == 0 

  - id: data_pdu
    size: |
      hdlc_header.format.frame_length - ((hdlc_header.control.frame_type & 0x1 == 0 ? hdlc_header.size : 0) + llc_header.size + 2)
    doc: |
      frame_length in hdlc header gives the total lenght of the hdlc frame without start/stop flags
      to get the length of encapsulated message, we substract size of hdlc and llc headers 
      and hdlc trailer.
  - id: fsc
    size: 2
  - id: stop_flag
    contents: [ 0x7e ]
instances:
  size:
    value: _io.size
types:
  hdlc_header_fields:
    seq:
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
        if: 'control.frame_type & 0x1 == 0' 
    instances:
      size:
        value: '2 + dst_address.size + src_address.size + 1 + (control.frame_type & 0x1 == 0 ? 2 : 0)'
  llc_header_fields:
    seq:
      - id: remote_lsap
        contents: [ 0xe6 ]
      - id: local_lsap
        type: u1
        enum: llc_packet_type
      - id: llc_quality
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
  control_type:
    seq:
      - id: i_frame
        type: i_frame_control_byte
        if: 'frame_type & 0x1 == 0' 
      - id: s_frame
        type: s_frame_control_byte
        if: 'frame_type == 1'
      - id: u_frame
        type: u_frame_control_byte
        if: 'frame_type == 3'
    instances:
      control_byte:
        pos: 0
        type: u1
      frame_type:
        value: 'control_byte & 0x3'
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
  hdlc_address:       
    seq:
      - id: bytes
        type: hdlc_address_byte
        repeat: until
        repeat-until: not _.has_next
    instances:
          size:
            value: bytes.size
          last:
            value: bytes.size - 1
          value:
            value: >-
              bytes[last].value
              + (last >= 1 ? (bytes[last - 1].value << 7) : 0)
              + (last >= 2 ? (bytes[last - 2].value << 14) : 0)
              + (last >= 3 ? (bytes[last - 3].value << 21) : 0)
              + (last >= 4 ? (bytes[last - 4].value << 28) : 0)
              + (last >= 5 ? (bytes[last - 5].value << 35) : 0)
              + (last >= 6 ? (bytes[last - 6].value << 42) : 0)
              + (last >= 7 ? (bytes[last - 7].value << 49) : 0)
            doc: Resulting value as normal integer
  hdlc_address_byte:
    doc: |
      One byte group, clearly divided into 7-bit "value" and 1-bit "has continuation
      in the next byte" flag. contiune with next byte = 0, final byte = 1.
    seq:
      - id: b
        type: u1
    instances:
      has_next:
        value: (b & 0b0000_0001) == 0
        doc: If true, then we have more bytes to read
      value:
        value: (b & 0b111_11110) >> 1
        doc: The 7-bit (base128) numeric value of this group
    
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
