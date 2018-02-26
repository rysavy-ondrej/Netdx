meta:
  id: dlms_get_response
  file-extension: dlms_get_response
  endian: be
  imports:
    - dlms_types
seq:

  - id: get_response_type
    type: u1
    enum: get_response_type
  - id: response
    type:
      switch-on: get_response_type
      cases:
        'get_response_type::get_response_normal': dlms_get_response_normal
        'get_response_type::get_response_next': dlms_get_response_with_datablock
        'get_response_type::get_response_with_list': dlms_get_response_with_list
types:
  dlms_get_response_normal:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority    
      - id: result
        type: dlms_types::get_data_result
  dlms_get_response_with_datablock:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority  
      - id: result
        type: dlms_types::datablock_g
  dlms_get_response_with_list:
    seq:
      - id: invoke_id_and_priority
        type: dlms_types::invoke_id_and_priority  
      - id: result
        type: dlms_types::sequence_of_get_data_result
        
enums:
  get_response_type:
    1: get_response_normal
    2: get_response_next
    3: get_response_with_list