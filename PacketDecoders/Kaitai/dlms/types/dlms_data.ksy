meta:
  id: dlms_data
  file-extension: dlms_data
seq: 
  - id: data_type
    type: u1
    enum: data_type
  - id: data_value
    type:
      switch-on: data_type
      cases: 
        'data_type::null_data': null_data
        
      # TODO: Add other data definitions  
        
types:
  null_data:
    seq:
      - id: nothing
        size: 0

enums:
  data_type:
    0: null_data
    1: array
    2: structure
    3: boolean
    4: bit_string
    5: double_long
    6: double_long_unsigned
    9: octet_string
    10: visible_string
    13: bcd
    15: integer
    16: long
    17: unsigned
    18: long_unsigned
    19: compact_array
    20: long64
    21: long64_unsigned
    22: enum
    23: float32
    24: float64
    25: date_time
    26: date
    27: time
    255: do_not_care
  boolean:
    0: false
    1: true
