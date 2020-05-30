## The Grammar-Grammar

The following is a grammar which can be used to describe PEGs, according to Python master Guido Van Rossum...

start: rules ENDMARKER
rules: rule rules | rule
rule: NAME ":" alts NEWLINE
alts: alt "|" alts | alt
alt: items
items: item items | item
item: NAME | STRING

The following constructions are to be added in order to make the metagrammar complete (as far as I can tell...):

- Support for multiline alternatives
- The ? and ! operators
