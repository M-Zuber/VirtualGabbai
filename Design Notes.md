# Purpose
The purpose of this system is to allow the Gabbai of a Shul (particularly one who is charge of the financial aspects) to manage data on the Shul members, such as when they have a Yartzhiet or if they are up to date on paying pledges.

# First draft of model design
## User
User of the system
## Privilege
Controls what a user can do in the system
## Privilege Group
A logical grouping of Privileges (for example super admin, support, ect)
## Person
A shul member
## PhoneNumber
## StreetAddress
## PhoneType
## Account
A persons account
## Donation
A dontation an account has made
## Yahrtzieht
Information on a Yartzieht that is relevant to a specific person

# Things to change
- Flatten out permissions (as well as the rename)
- Phone type can be an enum (if we integrate with a microservice to validate we can have two fields, one for what the user entered it as, and one for what it actually is)
- Expand payments to have more than just an double field on Donation
- Yartziehts should be connectable to more than one person
