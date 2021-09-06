<style>
  .md-nav--primary .md-nav__link[for=__toc] ~ .md-nav {
    display: none;
  }
</style>
# **`dialog`**
### Description
**Custom pre-made input dialogs, made to retrieve user input.**
### Usage
**Display a dialog when creating a new string.**
### Examples
```
Added dialog.yesno("Do you want to update?")
# Input either yes or no.

Added dialog.numerical("How many apples do you want?") 
# Numbers only.

Added dialog.color("What is your favorite color?") 
# Colors only.

Added dialog.secret("What is the password?"). 
# Secret password input. (Hiding the input with '*' characters.)
```