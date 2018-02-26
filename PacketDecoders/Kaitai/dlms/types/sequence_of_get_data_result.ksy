meta:
  id: sequence_of_get_data_result
  imports: 
    - length_encoded
    - get_data_result
seq:
  - id: item_count
    type: length_encoded
  - id: items
    type: get_data_result
    repeat: expr
    repeat-expr: item_count.value