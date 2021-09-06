<style>
  .md-nav--primary .md-nav__link[for=__toc] ~ .md-nav {
    display: none;
  }
</style>
# **`print`**
### Description
**Prints the specified message to the screen, commonly a string.**
### Usage
**By default prints data as a single line, use @ to create multiple lines.**
### Examples
```
print "value" 
#Outputs 'value'.

string example1 = "Example Value"
print example1
# Prints the data from the string called example1.

print @"value" 
#Outputs the data printed before with 'value'.

string example2 = "Example Value"
print @example2
# Prints the data from the string called example2.
```

----

# `p;`
### Description
**Prints the specified message to the screen, commonly a string. (With Formatting)**
### Usage
**By default prints data as a single line, use @ to create multiple lines.**
### Examples
```
p:bold; "hello"

p:italic; "hello"

p:underline; "hello"

p:invert; "hello"

p:slowblink; "hello"

p:rapidblink; "hello"

p:strikethrough; "hello"

p:bold:rapidblink:underline; "hello"
# Note: you can also stack multiple styles at once.
```