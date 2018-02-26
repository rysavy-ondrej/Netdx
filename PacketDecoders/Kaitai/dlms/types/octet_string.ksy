meta:
  id: octet_string
  imports:
    - length_encoded
seq:
  - id: length
    type: length_encoded
  - id: value
    type: str
    encoding: ascii
    size: length.value