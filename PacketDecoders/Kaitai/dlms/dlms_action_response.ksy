meta:
  id: dlms_action_response
  endian: be
  imports:
    - dlms_data
    - dlms_struct
seq:
    - id: action_response_type 
      type: u1
      enum: action_response_type
    - id: response
      type:
        switch-on: action_response_type
        cases: 
            'action_response_type::action_response_normal': action_response_normal
            'action_response_type::action_response_with_pblock': action_response_with_pblock
            'action_response_type::action_response_with_list': action_response_with_list
            'action_response_type::action_response_next_pblock': action_response_next_pblock
types:
    action_response_normal:
        {}
    action_response_with_pblock:
        {}
    action_response_with_list:
        {}
    action_response_next_pblock:
        {}
enums:
  action_response_type:
    1: action_response_normal
    2: action_response_with_pblock
    3: action_response_with_list
    4: action_response_next_pblock


