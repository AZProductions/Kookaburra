<style>
  .md-nav--primary .md-nav__link[for=__toc] ~ .md-nav {
    display: none;
  }
</style>
# **`image`**
### Description
**Displays an image in the window using the [Spectre.Console.Imagesharp](https://github.com/spectreconsole/spectre.console/tree/main/src/Spectre.Console.ImageSharp) API.**
### Usage
**First argument is the file location and the second is the max width.**
### Examples
```
new image("example.png", "12")
# Displays an image called example with the max width of 12 pixels.
```