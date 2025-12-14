# Token `operator`

All Operators except for unary not (!), parentheses, and member of (\)
NOTE: These can be ignored entirely by string substitution and dealt with directly.
NOTE: These must be listed after the above tokens so that they replace last. For example, `verexpr` uses these tokens.

## Attributes
- **attrs**: `cond vercond length width arg calc`
- **name**: `operator`

## Entries
- `operator` `#ADD#` (`string`=`+`)
- `operator` `#SUB#` (`string`=`-`)
- `operator` `#MUL#` (`string`=`*`)
- `operator` `#DIV#` (`string`=`/`)
- `operator` `#AND#` (`string`=`&&`)
- `operator` `#OR#` (`string`=`||`)
- `operator` `#LT#` (`string`=`<`)
- `operator` `#GT#` (`string`=`>`)
- `operator` `#LTE#` (`string`=`<=`)
- `operator` `#GTE#` (`string`=`>=`)
- `operator` `#EQ#` (`string`=`==`)
- `operator` `#NEQ#` (`string`=`!=`)
- `operator` `#RSH#` (`string`=`>>`)
- `operator` `#LSH#` (`string`=`<<`)
- `operator` `#BITAND#` (`string`=`&`)
- `operator` `#BITOR#` (`string`=`|`)

