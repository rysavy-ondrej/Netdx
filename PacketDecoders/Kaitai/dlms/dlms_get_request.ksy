meta:
  id: dlms_get_request
  file-extension: dlms_get_request
  endian: be
  imports:
    - dlms_types
seq:
  - id: get_request_type # this identifies the structure, 1 = sequence
    type: u1
    enum: get_request_type
  - id: request
    type:
      switch-on: get_request_type
      cases: 
        'get_request_type::get_request_normal': dlms_get_request_normal
        'get_request_type::get_request_next': dlms_get_request_next
        'get_request_type::get_request_with_list': dlms_get_request_with_list
types:
  dlms_get_request_normal:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority
      - id: cosem_attribute_descriptor
        type: dlms_types::cosem_attribute_descriptor
      - id: access_selection
        type: dlms_types::selective_access_description_optional
  dlms_get_request_next:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority
      - id: block_number
        type: u4
  dlms_get_request_with_list:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority
      - id: attribute_descriptor_list
        type: dlms_types::cosem_attribute_descriptor_with_selection
enums:
  get_request_type:
    1: get_request_normal
    2: get_request_next
    3: get_request_with_list  