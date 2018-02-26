meta:
  id: length_encoded
seq:
  - id: b1
    type: u1
  - id: int2
    type: u2be
    if: b1 == 0x82
instances:
  value:
    value: '(b1 & 0x80 == 0) ? b1 : int2'