meta:
  id: octet_string_optional
  imports:
    - octet_string
seq:
  - id: present
    type: u1
  - id: value
    type: octet_string
    if: present != 0