meta:
  id: dlms_types
  file-extension: dlms_types
  endian: be
  imports:
    - dlms_data
types:
  invoke_id_and_priority:
    seq:
      - id: priority
        type: b1
      - id: service_class
        type: b1
      - id: invoke_id
        type: b6
        
  cosem_attribute_descriptor:
    seq:
      - id: class_id
        type: cosem_class_id
      - id: instance_id
        type: cosem_object_instance_id
      - id: attribute_id
        type: cosem_object_attribute_id
  cosem_class_id:
    seq:
      - id: value
        type: u2
  cosem_object_instance_id:
    seq:
      - id: value
        size: 6
  cosem_object_attribute_id:
    seq:
      - id: value
        type: u1
  cosem_object_method_id:
    seq:
      - id: value
        type: u1  
  cosem_date_time:
    seq:
      - id: value
        size: 12
  cosem_attribute_descriptor_with_selection:
    seq:
      - id: cosem_attribute_descriptor
        type: cosem_attribute_descriptor
      - id: access_selection
        type: selective_access_description_optional
  selective_access_description_optional:
    seq:
      - id: present
        type: u1
      - id: value
        type: selective_access_description
        if: present != 0
  selective_access_description:
    seq:
      - id: access_selector
        type: u1
      - id: access_parameters
        type: dlms_data

  data_access_result:
    seq:
      - id: value
        type: u1
        enum: data_access_result
enums:
  data_access_result:
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
    15: long_get_aborted 
    16: no_long_get_in_progress
    17: long_set_aborted
    18: no_long_set_in_progress
    250: other_reason
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