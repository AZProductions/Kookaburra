<style>
  .md-nav--primary .md-nav__link[for=__toc] ~ .md-nav {
    display: none;
  }
</style>

***This syntax is supported in the versions 0.5.0 and newer.*** 

## **Print**
### **Description**
### Prints the specified message to the screen, commonly a string.
### **Usage**
### By default prints data as a single line, use ```@``` to create multiple lines. 
### **Examples**
```print "value"``` - ***Output 'value'.***

```print 'string'```

```print @"value"``` - ***Output the data printed before with 'value'.***

```print @'string'``` 

----
## **wait**
### **Description**
### Pauses the program for a specific time.
### **Usage**
### After ```wait```, specify the time of milliseconds to pause the program. 
### **Examples**
```wait 4000``` - ***Waits 4 seconds.***

```wait 10000``` - ***Waits 10 seconds.***

----
## **app.title**
### **Description**
### Changes the title of the application.
### **Usage**
### ```app.title = string``` 
### **Examples**
```app.title = "hello"``` - ***Sets the window title to the text 'hello'.***

```app.title = example``` - ***Sets the window title to the present value of the string called example.***

----
## **app.color**
### **Description**
### Changes the foreground color of the console.
### **Usage**
### ```app.color = color```  
### **Examples**
```
app.color = blue
print "Blue"
app.color = red
print "Red"
app.color = green
print "Green"
app.color = yellow
print "Yellow"
app.color = white
print "white"
```

----
## **app.read()**
### **Description**
### Pauses the program until the user presses the 'enter' key.
### **Usage**
### ```app.read()``` ***or*** ```string example = app.read()```

----
## **filewriter**
##### ***```import FileIO```***
### **Description**
### Writes data to files.
### **Usage**
```
import FileIO
new filewriter(location, value)
```
### **Examples**
```new filewriter("C:/users/AZ/Desktop/example.txt", "Hello World.")``` - ***Writes the data 'Hello World.' to the file 'example.txt'.***

----
## **app.debug-off**
### **Description**
### Removes debug text.
### **Usage**
### ```app.debug-off``` at the first line of the **.kookabura** file.
----
## **start**
### **Description**
### Starts executable or file.
### **Usage**
### After ```start```, type the location of the file. 
#### *This is the only function that disobeys the rules about formatting a string.*
### **Examples**
```start c:/file.exe```

----