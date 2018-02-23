meta:
  id: acse_pdu
  file-extension: acse_pdu
doc: >
    The ACSE APDUs are part of DLMS/COSEM communication during opening and 
    closing phase. ACSE APDUs are standardized by IEC 8650-1 standard or X.227
    The APDUs are encoded by BER for transmission. The user-information field 
    contains a COSEMpdu encoded by A-XDR.
    
    References:
    
    * Distribution automation using distribution line carrier systems - Part 6: A-XDR encoding rule, IEC 61334-6:2000, 2006, Edition 1.
    * ITU-T Recommendation X.227: Data Networks and Open System Communications: Connection-oriented Protocol for the Association Control Service Element: Protocol Specification, ITU-T X.227, 1995.
seq:
  - id: pdu_type
    type: u1
    enum: pdu_type
  - id: pdu
    type:
      switch-on: pdu_type
      cases:
        'pdu_type::aarq': aarq_pdu
        'pdu_type::aare': aare_pdu
        'pdu_type::rlrq': rlrq_pdu
        'pdu_type::rlre': rlre_pdu
        'pdu_type::abrt': abrt_pdu
        'pdu_type::adt': adt_pdu
        
types:
  aarq_pdu: 
    {}
  aare_pdu:
    {}
  rlrq_pdu:
    {}
  rlre_pdu:
    {}
  abrt_pdu:
    {}
  adt_pdu:
    {}
  
enums:
  pdu_type:
    96: aarq
    97: aare
    98: rlrq
    99: rlre
    100: abrt
    101: adt