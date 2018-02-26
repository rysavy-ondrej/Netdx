meta:
  id: dlms_set_request
  file-extension: dlms_set_request
  imports:
    - dlms_data
    - dlms_types
seq:
  - id: set_request_type # this identifies the structure, 1 = sequence
    type: u1
    enum: set_request_type
  - id: request
    type:
      switch-on: set_request_type
      cases: 
        'set_request_type::set_request_normal': set_request_normal
        'set_request_type::set_request_with_first_datablock': set_request_with_first_datablock
        'set_request_type::set_request_with_datablock': set_request_with_datablock
        'set_request_type::set_request_with_list': set_request_with_list
        'set_request_type::set_request_with_list_and_first_datablock': set_request_with_list_and_first_datablock
types:
  set_request_normal:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority
      - id: cosem_attribute_descriptor 
        type: dlms_types::cosem_attribute_descriptor
      - id: access_selection
        type: dlms_types::selective_access_description_optional
      - id: value
        type: dlms_data
  set_request_with_first_datablock: 
    {}
  set_request_with_datablock:
    {}
  set_request_with_list: 
    {}
  set_request_with_list_and_first_datablock:
    {}
enums:
  set_request_type:
    1: set_request_normal
    2: set_request_with_first_datablock 
    3: set_request_with_datablock 
    4: set_request_with_list 
    5: set_request_with_list_and_first_datablock