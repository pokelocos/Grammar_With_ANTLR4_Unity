grammar Simple_Grammar;

start :  ( s EOF ) ;
s : t;
t : a t  | t b  | b;
a : 'a' ;
b : 'b' ;
