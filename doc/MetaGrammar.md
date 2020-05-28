## The Grammar-Grammar

The following is a grammar which can be used to describe PEGs, according to Python master Guido Van Rossum...

grammar : rule+ ENDMARKER
rule : NAME ':' alternative ('|' alternative)\* NEWLINE
alternative : item+
item : NAME | STRING

The following constructions are to be added in order to make the metagrammar complete (as far as I can tell...):

- Support for multiline alternatives
- The ? operator
