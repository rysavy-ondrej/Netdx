meta:
  id: dlms_action_request
  endian: be
  imports:
    - dlms_data
    - dlms_struct
seq:
    - id: action_request_type 
      type: u1
      enum: action_request_type
    - id: request
      type:
        switch-on: action_request_type
        cases: 
            'action_request_type::action_request_normal': action_request_normal
            'action_request_type::action_request_next_pblock': action_request_next_pblock
            'action_request_type::action_request_with_list': action_request_with_list
            'action_request_type::action_request_with_first_pblock': action_request_with_first_pblock
            'action_request_type::action_request_with_list_and_first_pblock': action_request_with_list_and_first_pblock
            'action_request_type::action_request_with_pblock': action_request_with_pblock
types:
    action_request_normal:
        {}
    action_request_next_pblock:
        {}
    action_request_with_list:
        {}
    action_request_with_first_pblock:
        {}
    action_request_with_list_and_first_pblock:
        {}
    action_request_with_pblock:
        {}

enums:
    action_request_type:
        1: action_request_normal
        2: action_request_next_pblock
        3: action_request_with_list
        4: action_request_with_first_pblock 
        5: action_request_with_list_and_first_pblock 
        6: action_request_with_pblock