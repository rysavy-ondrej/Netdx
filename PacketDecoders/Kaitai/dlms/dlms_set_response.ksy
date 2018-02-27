meta:
  id: dlms_set_response
  endian: be
  imports:
    - dlms_data
    - dlms_struct
seq:
    - id: set_response_type 
      type: u1
      enum: set_response_type
    - id: response
      type:
        switch-on: set_response_type
        cases: 
            'set_response_type::set_response_normal': set_response_normal
            'set_response_type::set_response_datablock': set_response_datablock
            'set_response_type::set_response_last_datablock': set_response_last_datablock
            'set_response_type::set_response_last_datablock_with_list': set_response_last_datablock_with_list
            'set_response_type::set_response_with_list': set_response_with_list
types:
    set_response_normal:
        seq: 
            - id: invoke_id_and_priority
              type: dlms_struct::invoke_id_and_priority
            - id: result
              type: dlms_struct::data_access_result
    set_response_datablock:
        seq:
            - id: foo
              type: dlms_types::boolean

    set_response_last_datablock:
        {}
    set_response_last_datablock_with_list:
        {}
    set_response_with_list:
        {}
enums:
  set_response_type:
    1: set_response_normal
    2: set_response_datablock
    3: set_response_last_datablock
    4: set_response_last_datablock_with_list
    5: set_response_with_list
