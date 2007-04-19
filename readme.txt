
 Refal5 compiler for .NET platform 

 This is my spare-time project created for fun. I made fully-functional 
 Refal5 compiler in a few days about a year ago, in June 2006. 
 Once I discovered it recently among my old projects, I decided to publish 
 its source on CodePlex. Although it is not yet complete, it can compile 
 and run most sample programs from standard Refal5 distribution. 
 The main disadvantage of the current version is that the compiler 
 don't handle recursion as the original Refal5 complier does, so large 
 programs would crash with stack overflow.

 Features

 * Supports base Refal syntax (functions, pattern matching, etc)
 * Extended Refal5 syntax (where- and with-clauses)
 * Compiles Refal to C# (could be easily extended to produce binary code)
 * A few standard functions supported (Prout, Card, Open, etc) 

 Future plans

 * Intelligent recursion handling
 * IL-code and binary assembly generation
 * Pattern matching optimizations 

 Implementation details
 Scanner and parser for Refal5.NET are generated with the use of Coco/R 
 Compiler Generator. Parser uses CodeBuilder helper class to build the AST, 
 which is then processed with the code generator. Code generator uses Visitor 
 design pattern to generate plain C# code from AST nodes, and it could be 
 easily extended to generate IL-code or whatever. Compilation results 
 are linked to a few utility classes (pattern matching, standard Refal5 RTL 
 functions, etc) to produce an executable program. There are currently 
 no utilities to handle build process automatically, so I had to write 
 bat-files for this purpose.

 References

 * Refal language homepage: http://refal.com/
 * Refal5 book (see Syntax Summary): http://shura.botik.ru/refal/book/html/
 * Coco/R Compiler Generator for C#: http://www.scifac.ru.ac.za/coco/
 * My homepage: http://yallie.narod.ru/

 Sincerely yours, Alexey Yakovlev <yallie@yandex.ru>

 Y [18-04-07]
