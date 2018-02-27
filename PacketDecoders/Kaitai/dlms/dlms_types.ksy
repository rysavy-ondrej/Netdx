meta:
  id: dlms_types
types:
  boolean_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: boolean
        if: present != 0
  boolean:
    seq:
      - id: value
        type: u1
        enum: boolean
  octet_string_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: octet_string
        if: present != 0
  octet_string:
    seq:
      - id: length
        type: length_encoded
      - id: value
        type: str
        encoding: ascii
        size: length.value
  length_encoded:
    seq:
      - id: b1
        type: u1
      - id: int2
        type: u2be
        if: b1 == 0x82
    instances:
      value:
        value: '(b1 & 0x80 == 0) ? b1 : int2'
enums:
  boolean:
    0: false
    1: true