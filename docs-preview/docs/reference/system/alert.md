<style>
  .md-nav--primary .md-nav__link[for=__toc] ~ .md-nav {
    display: none;
  }
</style>
# **`Alert`**
### Description
**Displays an pre specified alert to the screen.**
### Usage
**First define the string, then define the type of alert.**
### Examples
```
new Alert("Message", 1)
# shows alert with header 'Note'.

new Alert("Message", 2)
# shows alert with header 'Message'.

new Alert("Message", 3)
# shows alert with header 'Warning'.

new Alert("Message", 4)
# shows alert with header 'Error'.

new Alert("Message", 5)
# shows alert with header 'Help'.

new Alert("Message", 6)
# shows alert with header 'Update'.
```