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
* Possibility to reference other "Dangling" executables.

### Data types

Simple data types (integers and booleans) can be simply used as shown in the following code:

```
x = 5^2
b = true || false
y = (x/5)!

print(x + y)
print(b)
```

While struct types must be properly declared before usage and they can be nested to allow more complex types:

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
