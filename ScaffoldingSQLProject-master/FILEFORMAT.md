1. [SQL Scaffolding File Format](#sql-file-format)
	1. [Version 1](#version1)
	2. [Encrypted Secret Word Format](#encrypted-secret-format)
	3. [Testcase Format](#testcase-format)
	4. [Parsons Format](#parsons-format)
2. [Tenative Story File Format](#story-file-format)
	1. [Tentative Story Format](#story-format)
	2. [Tenative Tree Unlock Format](#story-tree-unlock-format)
	3. [Example File](#story-file-format-example-1)

# SQL Scaffolding File Format <a name=sql-file-format></a>

Filename: {unit_number}.{question number}.txt

###### Version 1 <a name=version1></a>

| Field Name                   | Field Value                              | Type           | Is Single Line         | Is Optional | Starting String     | Ending String     | Notes                                                                                                                    |
|------------------------------|------------------------------------------|----------------|------------------------|-------------|---------------------|-------------------|--------------------------------------------------------------------------------------------------------------------------|
| Question                     | string                                   | string         | Yes                    | No          |                     |                   |                                                                                                                          |
| Testcases Enabled            | (true\|false)                            | bool           | Yes                    | No          |                     |                   |                                                                                                                          |
| Parsons Enabled              | (true\|false)                            | bool           | Yes                    | No          |                     |                   |                                                                                                                          |
| Secrets (Code words)         | list of Base64 encoded strings           | List\<string\> | Yes and No (See Notes) | No          | StartSecrets        | EndSecrets        | See Encrypted Secret Word Format                                                                                         |
| Parsons Secrets (Code Words) | list of Base64 encoded strings           | List\<string\> | Yes and No (See Notes) | No          | StartParsonsSecrets | EndParsonsSecrets | See Encrypted Secret Word Format                                                                                         |
| Test Cases                   | list of strings in the Test Case Format  | List\<string\> | No                     | No          | *Any string*        | Parsons           | Parsons denotes the next field.                                                                                          |
| Parsons                      | list of strings writen in Parsons Format | List\<string\> | No                     | Yes\*       | Parsons             | EndParsons        | \*If there should be no parsons, it must be formated as Parsons\<CR\>EndParsons                                          |
| Database                     | A list of strings                        | List\<string\> | No                     | Yes         | StartDatabase       | EndDatabase       | We do not forsee multilple databases bieng loaded at the same time. But if the need arrises, the ground work is laid.    |
| Version Number               | A version number                         | int            | Yes                    | Yes\*       |                     |                   | \*If the string is not present, assume format version 0                                                                  |


###### Encrypted Secret Word Format <a name=encrypted-secret-format></a>
Secret words may be encrypted. To this end, a simple proprietary format is used.
First, the values are encrypted in the javascript layer with the help of CryptoJS.
Second, these values are passed to the C# layer, where they are encrypted once more.
*NOTE: On decryption, this process happens in reverse!* This is the format for the encryption format: version 1.
CryptoJS.AES Encryption => C# RijndaelManaged Encryption

| Bytes 0-3             | Bytes 4-27            | Bytes 28-51 | Bytes 52 - last   |
|-----------------------|-----------------------|-------------|-------------------|
| Version Number (1-99) | Initialization Vector | Key         | Encrypted message |

Each component of the encrypted secret word format is encoded with [Base64](https://en.wikipedia.org/wiki/Base64).

###### Testcase Format <a name=testcase-format></a>

A test case is written on a single line.

| First Character | Second Character Set    | Thrid Character Set          | Fourth Character Set | Notes                                                                 |
|-----------------|-------------------------|------------------------------|----------------------|-----------------------------------------------------------------------|
| L               | R \| C                  | numbers                      |                      | IF second set is R: test number of rows; ELSE test number of columns. |
| V               | \[numbers\],\[numbers\] | Combination of =, \>, \<, !  | a string             |                                                                       |

###### Parsons Format <a name=parsons-format></a>

The first set of words must represent an SQL command, such as Select, Delete From, On, Order By, etc.
This is then optionally followed by a set of strings resembling the following:
```
$$toggle::value1::value2::value3::value4::..::valuen$$
```
The values encoded in this string are handled in the JavaScript layer via the Parsons.js libray.

# Tenative Story File Format <a name=story-file-format></a>
Tenative Story File name: {unit_number}.story.{unique_name}.txt

| Field Name        | Field Value                             | Type           | Is Single Line | Is Optional | Starting String | Ending String | Notes                                                       |
|-------------------|-----------------------------------------|----------------|----------------|-------------|-----------------|---------------|-------------------------------------------------------------|
| Version Number    | Number                                  | int            | Yes            | No          |                 |               |                                                             |
| Related Questions | List of related questions               | List\<string\> | No             | No          | StartRelatedQ   | EndRelatedQ   | These may or may not end with .txt                          |
| Unlock Tree       | Question Completed -> List of Questions | List\<string\> | No             | Yes         | StartUnlockTree | EndUnlockTree | If a number is not listed, or is the root, assume unlocked. |
| Story             | String                                  | List\<string\> | No             | No          | StartStory      | EndStory      | See Tenative Story Format                                   |


###### Tentative Story Format <a name=story-format></a>

The story format will be a multi-line string (for ease of editing manually, should the need arise. ). The string will contain story markers, which will look like:
```
$$story::<integer>.
```
Each story marker will be used to denote which quesion is used, ie, if `$$story::1`, then the first question listed in the 'Related Questions' will be used.

###### Tenative Tree Unlock Format <a name=story-tree-unlock-format></a>

| Symbols                    | Operation                                                             |
|----------------------------|-----------------------------------------------------------------------|
| \<number\>                 | If number is complete, unlock.                                        |
| \<number1\> & \<number2\>  | If number one *AND* number two are complete, unlock.                  |
| \<number1\> \| \<number2\> | If number one *OR* number two are complete, unlock.                   |
| -\>                        | If condition on the left is true, unlock the questions on the right   |

Example:
```
1 -> 2, 3
```
```
2 & 3 -> 5
```
```
3 | 5 -> 6
```

###### Example File <a name=story-file-format-example-1></a>
```
1
StartRelatedQ
1.1
1.2
1.4
1.3
EndRelatedQ
StartUnlockTree
1 -> 2, 3
2 & 3 -> 4
4 -> 5
EndUnlockTree
StartStory
And so, Bilbo Worf looked to the $$story::1, where he saw a beast most $$story::2. He was most
enamored with this beautiful sight, he almost forgot about the $$story::3 of $$story::4 chasing him
across the $$story::5.
EndStory
```
