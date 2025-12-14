# Token `verexpr`

Commonly used version expressions.
NOTE: `string` should be wrapped in parentheses for string subsitutions in larger expressions.
NOTE: BSVER 'Greater Than' Expressions only ever apply to BS i.e. BSVER GT 0.
WARNING: BSVER 'Less Than' Expressions also apply to NI i.e. BSVER EQ 0.

## Attributes
- **attrs**: `vercond`
- **name**: `verexpr`

## Entries
- `verexpr` `#NISTREAM#` (`string`=`(#BSVER# #EQ# 0)`) – NiStream that are not Bethesda.
- `verexpr` `#BSSTREAM#` (`string`=`(#BSVER# #GT# 0)`) – NiStream that are Bethesda.
- `verexpr` `#NI_BS_LTE_16#` (`string`=`(#BSVER# #LTE# 16)`) – All NI + BS until BSVER 16.
- `verexpr` `#NI_BS_LT_FO3#` (`string`=`(#BSVER# #LT# 34)`) – All NI + BS before Fallout 3.
- `verexpr` `#NI_BS_LTE_FO3#` (`string`=`(#BSVER# #LTE# 34)`) – All NI + BS until Fallout 3.
- `verexpr` `#NI_BS_LT_SSE#` (`string`=`(#BSVER# #LT# 100)`) – All NI + BS before SSE.
- `verexpr` `#NI_BS_LT_FO4#` (`string`=`(#BSVER# #LT# 130)`) – All NI + BS before Fallout 4.
- `verexpr` `#NI_BS_LTE_FO4#` (`string`=`(#BSVER# #LTE# 139)`) – All NI + BS until Fallout 4.
- `verexpr` `#BS_GT_FO3#` (`string`=`(#BSVER# #GT# 34)`) – Skyrim, SSE, and Fallout 4
- `verexpr` `#BS_GTE_FO3#` (`string`=`(#BSVER# #GTE# 34)`) – FO3 and later.
- `verexpr` `#BS_GTE_SKY#` (`string`=`(#BSVER# #GTE# 83)`) – Skyrim and later.
- `verexpr` `#BS_GTE_SSE#` (`string`=`(#BSVER# #GTE# 100)`) – SSE and later.
- `verexpr` `#BS_SSE#` (`string`=`(#BSVER# #EQ# 100)`) – SSE only.
- `verexpr` `#BS_FO4#` (`string`=`(#BSVER# #EQ# 130)`) – Fallout 4 strictly, excluding stream 132 and 139 in dev files.
- `verexpr` `#BS_FO4_2#` (`string`=`((#BSVER# #GTE# 130) #AND# (#BSVER# #LTE# 139))`) – Fallout 4/76 including dev files.
- `verexpr` `#BS_GT_130#` (`string`=`(#BSVER# #GT# 130)`) – Later than Bethesda 130.
- `verexpr` `#BS_GTE_130#` (`string`=`(#BSVER# #GTE# 130)`) – Bethesda 130 and later.
- `verexpr` `#BS_GTE_132#` (`string`=`(#BSVER# #GTE# 132)`) – Bethesda 132 and later.
- `verexpr` `#BS_GTE_152#` (`string`=`(#BSVER# #GTE# 152)`) – Bethesda 152 and later.
- `verexpr` `#BS_F76#` (`string`=`(#BSVER# #EQ# 155)`) – Fallout 76 stream 155 only.
- `verexpr` `#BS_GTE_152#` (`string`=`(#BSVER# #GTE# 152)`) – Fallout 76 stream 152 and higher
- `verexpr` `#BS202#` (`string`=`((#VER# #EQ# 20.2.0.7) #AND# (#BSVER# #GT# 0))`) – Bethesda 20.2 only.
- `verexpr` `#DIVINITY2#` (`string`=`((#USER# #EQ# 0x20000) #OR# (#USER# #EQ# 0x30000))`) – Divinity 2

