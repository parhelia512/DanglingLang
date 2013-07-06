![Mr. Dangling](http://pomma89.altervista.org/danglinglang/rocco.png "Rocco Siffredi") DanglingLang
===================================================================================================

Introduction
------------

This is a small project realized for the academic course "Implementation of Programming Languages", taught by [Giovanni Lagorio](http://www.disi.unige.it/person/LagorioG/) and [Davide Ancona](http://www.disi.unige.it/person/AnconaD/) at the [University of Genoa](http://www.dibris.unige.it/index.php). All kind of material stored on this repository is public domain.

"Dangling" is a simple language, whose goal was to let us understand how one can write a compiler and which are the difficulties involved. If you are wondering why the language is called "Dangling" and why the "logo" of the tool chain is [Rocco Siffredi](http://it.wikipedia.org/wiki/Rocco_Siffredi)... Well, we will let you wonder :)

The tools that have been used to realize this project were:
* Visual Studio 2012: http://www.microsoft.com/visualstudio
* Garden Points Parser Generator: http://gppg.codeplex.com/
* Mono.Cecil: http://www.mono-project.com/Cecil
* NUnit: http://en.wikipedia.org/wiki/NUnit

Language features
-----------------

The features that have been implemented during this project are:
* Three data types: integers, booleans, records;
* Implicit variable typing;
* Main arithmetic instructions, including power and factorial;
* Main logic operators (and, or, not);
* Functions, with simple recursion (that is, not mutual);
* Possibility to reference other "Dangling" executables;
* A small GUI to quickly work with the compiler.

### Data types

Simple data types (integers and booleans) can be simply used as shown in the following code:

```
x = 5^2
b = true || false
y = (x/5)!

print(x + y)
print(b)
```

While, on the other hand, struct types must be properly declared before usage; however, they can be nested to allow more complex types:

```
struct point {int x; int y;}
p = struct point {3^2, 5!}
print(p.x)
print(p.y)

struct datum {struct point p; bool rel;}
d1 = struct datum {p, true}
print(d1.p.x)
print(d1.p.y)
print(d1.rel)
d2 = struct datum {struct point {4!, 3*2}, true && true}
print(d2.p.x)
print(d2.p.y)
print(d2.rel)
```

### Functions

Function declaration (and usage) closely follows the C model. As said before, recursion is allowed, but mutual recursion is not possible. Let's see how functions work with a classical example, that is, Fibonacci numbers computation:

```
int recfib(int n) {
    if (n <= 0) return 0
    if (n == 1) return 1
    return recfib(n-1) + recfib(n-2)
}

print(recfib(-1))
print(recfib(0))
print(recfib(1))
print(recfib(3))
print(recfib(5))

int iterfib(int n) {
    f0 = 0 
    f1 = 1
    i = 0
    while (i < n) {  
        tmp = f1 
        f1 = f1 + f0 
        f0 = tmp
        i = i + 1 
    }
    return f0
}

print(iterfib(-1))
print(iterfib(0))
print(iterfib(1))
print(iterfib(3))
print(iterfib(5))
```

Functions can also work with struct types, as expected:

```
struct point {int x; int y;}

struct point enlargeMyPoint(struct point p, int factor) {
    return struct point {p.x*factor, p.y*factor}
}

int recfib(int n) {
    if (n <= 0) return 0
    if (n == 1) return 1
    return recfib(n-1) + recfib(n-2)
}

p = struct point {recfib(5), recfib(6)}
p = enlargeMyPoint(p, recfib(3))
print(p.x)
print(p.y)
```

### Loading external executables

As an extension to what was originally planned for the project, a "load" statement as been added to the language. With that statement it is possible to link an external executable, only if it was obtained by the "Dangling" compiler. Moreover, the name of the executable must follow IDs grammar and the executable itself must be placed in the same compiler directory.

Supposing the following script was compiled into "TestLoad.exe":

```
struct time {int h; int m; int s;}
struct datum {struct time t; bool relevant;}

void printTime(struct time t) {
  print(t.h)
  print(t.m)
  print(t.s)
}

bool isRelevant(struct datum d) {
  return d.relevant
}

struct time createTime(int h, int m, int s) {
  return struct time {h, m, s}
}

t = struct time {12, 28, 34}
printTime(t)
```

Then it can be loaded into new scripts, as in the following example:

```
load(TestLoad)

t = createTime(12, 34, 27)
printTime(t)

d = struct datum {t, true}
print(isRelevant(d))
```

Compilation process
-------------------

The compiler is based on the structure proposed by Giovanni Lagorio for the laboratory about LLVM code generation. That structure has been properly modified, so that code is now generated for the Common Language Infrastructure.

The following operations are sequentially run by the compiler in order to obtain working bytecode:

1. Input is broken into tokens, using Gppg and specification contained in [DanglingLang.l](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Tokenizer/DanglingLang.l);
2. Tokens are checked against the grammar (produced by Gplex following specification in [DanglingLang.y](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Tokenizer/DanglingLang.y);
3. At the same time, the abstract syntax tree is built, using the object model contained in [AST.cs](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/AST.cs);
4. 4. [Type checker](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Visitors/TypecheckVisitor.cs) and [function return statement checker](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Visitors/ReturnCheckVisitor.cs) are run on the AST;
5. A visitors which transforms the AST into string ([ToStringVisitor.cs](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Visitors/ToStringVisitor.cs)) is run for debugging purposes;
6. As a last step, the visitor which generates code ([CecilVisitor.cs](https://github.com/pomma89/DanglingLang/blob/master/DanglingLang/Visitors/CecilVisitor.cs)) is run on the AST.

### How to run the compiler

The compiler, `DanglingLang.exe`, can be run in two ways. The first one is the following:

```
DanglingLang.exe mySourceFile.txt
```

Command above will compile given file into `mySourceFile.exe`, an executable for the .NET framework. If you want the compiler to launch the executable for you, you can just modify the command in this way:

```
DanglingLang.exe -e mySourceFile.txt
```

If you launch the compiler with the "-e" flag, in case of successful compilation the compiler itself will launch the executable.

### Error handling

If the code contains some mistakes, then the compiler should print some kind of error (more or less detailed...) and then exit with 1 as error code. That value is used by the GUI to understand whether something has gone wrong.

Error messages could have been improved, indeed this is a weak point of the project.

### Return statement checker

That kind of visitor has been added in order to deal with wrongly declared functions. In particular, it is run to discover:
* Procedures which do not have a return statement, so that a default one can be placed right at the bottom of the body;
* Procedures and functions which have code after a return statement;
* Functions which do not have a return statement in some execution path.

Code generation
---------------

To generate executable code from a checked AST using Mono.Cecil, we start from a "skeleton" assembly, whose code is contained in [DanglingLang.Runner](https://github.com/pomma89/DanglingLang/tree/master/DanglingLang.Runner). That assembly contains the following static classes:
* Program: where the Main function is declared;
* SystemFunctions: contains functions like Min, Max and Fact, which are used to provide default functionalities;
* UserFunctions: where all functions declared in the script will be stored.

The code generation process takes that assembly and shapes it according to what follows:
* Program.Main is emptied and filled with the instructions contained in the script;
* All struct types declared in the script become top level types in that assembly; this choice was made to simplify things, but it can create problems when a script declares a struct named, for example, "UserFunctions". That issue could be solved by storing structs as inner classes inside a special static class ("UserTypes", for example);
* All functions become static methods of the "UserFunctions" static class;
* All namespaces are fixed, so that they match the script file name.

After having modified the assembly, it is written back to disk; if the script was called "PINO.txt", then the executable will be stored as "PINO.exe". As said before, all namespace of that file will be renamed to "PINO". Therefore, if the user declares a struct as "Gino", then the struct full name will be "PINO.Gino".

### Mono.Cecil examples

Conclusions
-----------

This is a "toy" compiler, whose code could be used as a starting point to build something more serious. As said at the beginning of the page, code is public domain, in order to let people do whatever they like with that.

There is not much documentation on Mono.Cecil; in any case, the best resource to understand what CIL instructions op-codes mean is, as always, Wikipedia:

http://en.wikipedia.org/wiki/List_of_CIL_instructions

Hope you will find the contents of this repository useful :)
