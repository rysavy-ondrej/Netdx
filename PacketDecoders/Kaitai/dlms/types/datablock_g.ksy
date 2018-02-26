meta:
  id: datablock_g
  endian: be
  imports:
   - octet_string
   - data_access_result
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
        0: octet_string
        1: data_access_result