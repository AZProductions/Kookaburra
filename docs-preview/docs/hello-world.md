# Hello World!
!!! info inline end "Output"
        ```
        welcome to my first program! 
        You pressed enter!
        ```


Lets start by creating a simple program.
```
app.debug-off
# setting the foreground color to blue.
app.color = blue
# changing the title.
app.title = "My first program"
# printing welcome message.
print "welcome to my first program!"
# freezing the program until the user presses enter.
app.read()
print "You pressed enter!"
wait 2000
```
We can use Figlets to display the message.
```
figlet "Hello World!"
app.read()
```
!!! info "Output"
        Hello World but it takes the entire window.

