meta:
  id: selective_access_descriptor_optional
  imports: 
    - dlms_data
seq:
  - id: present
    type: u1
  - id: value
    type: selective_access_descriptor
    if: present != 0
types:
  selective_access_descriptor:
    seq:
      - id: access_selector
        type: u1
      - id: access_parameters
        type: dlms_data