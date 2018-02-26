meta:
  id: dlms_pdu
  file-extension: dlms_pdu
  endian: be
  imports: 
    - types/dlms_data
    - dlms_set_request
    - dlms_get_request
    - dlms_get_response
seq:
  - id: pdu_type
    type: u1
    enum: dlms_pdu_type
  - id: pdu 
    size-eos: true
    type:
      switch-on: pdu_type
      cases:
        'dlms_pdu_type::get_request': dlms_get_request
        'dlms_pdu_type::get_response': dlms_get_response
        'dlms_pdu_type::set_request': dlms_set_request
        'dlms_pdu_type::set_response': dlms_set_response   
        'dlms_pdu_type::event_notification_request': dlms_event_notification_request
        'dlms_pdu_type::action_reauest': dlms_action_reauest
        'dlms_pdu_type::action_response': dlms_action_response
        
types:
  ##
  ## SET RESPONSE
  ##
  dlms_set_response: 
    {}
  
  dlms_event_notification_request:
    {}
  
  dlms_action_reauest:
    {}
    
  dlms_action_response:
    {}

enums:
  dlms_pdu_type:
    1: initiate_request
    5: read_request 
    6: write_request
    8: initiate_response
    12: read_response
    13: write_response
    14: confirmed_service_error
    22: unconfirmed_write_request
    24: information_report_request
    192: get_request 
    193: set_request
    194: even_notification_request
    195: action_request
    196: get_response
    197: set_response
    199: action_response
  
  boolean:
    0: false
    1: true