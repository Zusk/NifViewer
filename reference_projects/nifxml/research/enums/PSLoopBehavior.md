# Enum `PSLoopBehavior`

## Attributes
- **name**: `PSLoopBehavior`
- **since**: `V20_5_0_0`
- **storage**: `uint`

## Values
- `option` `PS_LOOP_CLAMP_BIRTH` (`value`=`0`) – Key times map such that the first key occurs at the birth of the particle, and times later than the last key get the last key value.
- `option` `PS_LOOP_CLAMP_DEATH` (`value`=`1`) – Key times map such that the last key occurs at the death of the particle, and times before the initial key time get the value of the initial key.
- `option` `PS_LOOP_AGESCALE` (`value`=`2`) – Scale the animation to fit the particle lifetime, so that the first key is age zero, and the last key comes at the particle death.
- `option` `PS_LOOP_LOOP` (`value`=`3`) – The time is converted to one within the time range represented by the keys, as if the key sequence loops forever in the past and future.
- `option` `PS_LOOP_REFLECT` (`value`=`4`) – The time is reflection looped, as if the keys played forward then backward the forward then backward etc for all time.

