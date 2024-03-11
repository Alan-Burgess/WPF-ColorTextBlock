# WPF-ColorTextBlock

## Introduction

![alt text](Screenshot1.png?raw=true)

This is a text block control that allows different colors, font sizes, font weights, font styles and font families. It also allows a user to select text and copy it to the clipboard.

![alt text](Screenshot2.png?raw=true)

The demo project implementation demonstrates the use of it using a MVVM architecture.

Though the project is here to be used, I am still in the process of making a useful demo application and documentation.

## How the formatting is achieved

Below is the string required to display the formatted text in the image above:

```
A demo of the %C=Red>%T=Italic>ColorTextBlock%>%> Control %C=Green>%S=16>(and its many %F=Lucida Handwriting>features%>)%>%>.
```

The '=' is optional and the command character is not case sensitive:

```
A demo of the %cRed>%tItalic>ColorTextBlock%>%> Control %cGreen>%s16>(and its many %fLucida Handwriting>features%>)%>%>.
```

## Commands

The following commands are available:

- **`%C=[Color]>`** - Set the color of the text.
  Example: **`%C=Red>`**
- **`%F=[Font Family]>`** - Set the font family.
  Example: **`%F=Console>`**
- **`%S=[Size]>`** - Set the font size.
  Example: **`%S=16>`**
- **`%T=[Font Style]>`** - Set the font style.
  Example: **`%T=Italic>`**
- **`%W=[Font Weight]>`** - Set the font weight.
  Example: **`%W=Bold>`**
- **`%>`** - Return to previous setting.

### Use of commands

When a command is embedded in the string, the string interpreter saves the previous setting, and it is restored by the `%>` command. If you fail to restore the setting, it will automatically be restored for you at the end of the string.


