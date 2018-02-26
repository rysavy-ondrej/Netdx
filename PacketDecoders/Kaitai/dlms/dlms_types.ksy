meta:
  id: dlms_types
  file-extension: dlms_types
  endian: be
  imports:
    - dlms_data
    - axdr_types
types:

  cosem_object_method_id:
    seq:
      - id: value
        type: u1  
  cosem_date_time:
    seq:
      - id: value
        size: 12
        

  action_result:  
    0: success
    1: hardware_fault
    2: temporary_failure
    3: read_write_denied
    4: object_undefined
    9: object_class_inconsistent
    11: object_unavailable 
    12: type_unmatched 
    13: scope_of_access_violated
    14: data_block_unavailable 
    15: long_action_aborted 
    16: no_long_action_in_progress
    250: other_reason  