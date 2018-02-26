meta:
  id: boolean_optional
  imports:
    - boolean
seq:
  - id: present
    type: u1
  - id: value
    type: boolean
    if: present != 0