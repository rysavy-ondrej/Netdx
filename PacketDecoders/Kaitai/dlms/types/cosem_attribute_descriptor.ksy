meta:
  id: cosem_attribute_descriptor
  endian: be
seq:
  - id: class_id
    type: cosem_class_id
  - id: instance_id
    type: cosem_object_instance_id
  - id: attribute_id
    type: cosem_object_attribute_id
types:
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