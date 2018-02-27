meta:
  id: dlms_get_response
  endian: be
  imports:
    - types/invoke_id_and_priority
    - types/get_data_result
    - types/datablock_g
    - types/sequence_of_get_data_result
seq:
  - id: get_response_type
    type: u1
    enum: get_response_type
  - id: response
    type:
      switch-on: get_response_type
      cases:
        'get_response_type::get_response_normal': get_response_normal
        'get_response_type::get_response_next': get_response_with_datablock
        'get_response_type::get_response_with_list': get_response_with_list
types:
  get_response_normal:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority    
      - id: result
        type: get_data_result
  get_response_with_datablock:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority  
      - id: result
        type: datablock_g
  get_response_with_list:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority  
      - id: result
        type: sequence_of_get_data_result
enums:
  get_response_type:
    1: get_response_normal
    2: get_response_next
    3: get_response_with_list