meta:
  id: dlms_pdu
  file-extension: dlms_pdu
  endian: be
  imports: 
    - dlms_data
    - dlms_set_request
    - dlms_get_request
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
  ## GET RESPONSE:
  ##    
  dlms_get_response:
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
  dlms_get_response_normal:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority    
      - id: result
        type: get_data_result
  dlms_get_response_with_datablock:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority  
      - id: result
        type: datablock_g
  dlms_get_response_with_list:
    seq:
      - id: invoke_id_and_priority
        type: invoke_id_and_priority  
      - id: result
        type: sequence_of_get_data_result
        
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
  ##
  ## COMMON FOR ALL PDUs
  get_data_result:
    seq: 
      - id: data
        type: dlms_data
      - id: data_access_result
        type: data_access_result
  
  sequence_of_get_data_result:
    seq:
      - id: length
        type: length_encoded
      - id: data
        type: get_data_result_array  
        size: length.value
        
  get_data_result_array:
    seq:
      - id: data
        type: get_data_result
        repeat: eos
        
  datablock_g:
    seq:
      - id: last_block
        type: u1
      - id: block_number
        type: u4
      - id: result_choice
        type: u1
      - id: result
        type:
          switch-on: result_choice
          cases:
            0: axdr_octet_string
            1: data_access_result
        
  #
  # COSEM Types
  #
  

  # ----
  # AXDR stuff
  #
  length_encoded:
    seq:
      - id: b1
        type: u1
      - id: int2
        type: u2be
        if: b1 == 0x82
    instances:
      value:
        value: '(b1 & 0x80 == 0) ? b1 : int2'
  #
  # DLMS Data Types
  #
  axdr_octet_string_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: axdr_octet_string
        if: present != 0
  axdr_octet_string:
    seq:
      - id: length
        type: u1
      - id: value
        type: str
        encoding: ascii
        size: length
  axdr_boolean_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: axdr_boolean
        if: present != 0
  axdr_boolean:
    seq:
      - id: value
        type: u1
        enum: boolean

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

  get_response_type:
    1: get_response_normal
    2: get_response_next
    3: get_response_with_list
  
  boolean:
    0: false
    1: true