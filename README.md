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
