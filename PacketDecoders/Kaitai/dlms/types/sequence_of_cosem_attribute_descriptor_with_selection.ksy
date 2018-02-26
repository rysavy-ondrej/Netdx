meta:
  id: sequence_of_cosem_attribute_descriptor_with_selection
  imports:
    - cosem_attribute_descriptor
    - selective_access_description_optional
    - length_encoded
seq:
  - id: item_count
    type: length_encoded
  - id: items
    type: cosem_attribute_descriptor_with_selection
    repeat: expr
    repeat-expr: item_count.value
types:
  cosem_attribute_descriptor_with_selection:
    seq:
      - id: cosem_attribute_descriptor
        type: cosem_attribute_descriptor
      - id: access_selection
        type: selective_access_description_optional