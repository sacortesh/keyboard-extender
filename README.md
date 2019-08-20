# keyboard-extender
A CSharp shortcut app extensible by plugins to perform particular tasks, only on windows.

Heavy inspiration for this project came from similar app developed for linux (http://ssokolow.com/quicktile). In the workplace you can't always avoid Windows so I made this.

This app uses ini-parser.

If you found this app useful, consider buying me a beer via paypal (https://paypal.me/sacortesh) or telling me how it has improved your workflow via twitter (https://twitter.com/sacortesh).

## Configuration

On first execution, a file "KeyboardExtender.cfg" will be created which will allow the user to configure the shortcuts. It will have a [General] subsection to select the selected modifiers, Control and Alt by default, and a [Keys] sub section where each plugin can be attached to a trigger key.

The convention used for the keys is can be found on:

https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netframework-4.8

An example of a configuration file is:

```
[General]
ModMask = <LControlKey><Alt>
[Keys]
NumPad1 = bottom-left
NumPad2 = bottom
NumPad3 = bottom-right
NumPad4 = left
NumPad5 = middle
NumPad6 = right
NumPad7 = top-left
NumPad8 = top
NumPad9 = top-right
NumPad0 = maximize
D = show-desktop
```

## Usage

After configuration, execute the application and a splash screen and a system icon will be displayed. No form is deployed with the application so exiting can be done through right click on the tray icon.

As long as everything is correct on the configuration, the user should be able to use the shortcuts configured in his file.

## Features

On current version, there are the following behaviors implemented:

- `top-left` 

  Tile the active window to span the top-left quarter of the  screen. Press the hotkey multiple times to cycle through different width  presets.

- `top` 

  Tile the active window to span the top half of the screen. Press  the hotkey multiple times to cycle through different width presets.

- `top-right` 

  Tile the active window to span the top-right quarter of the  screen. Press the hotkey multiple times to cycle through different width  presets.

- `left`

  Tile the active window to span the left half of the screen.  Press the hotkey multiple times to cycle through different width  presets.

  

- `middle` 

  Tile the active window to fill the screen. Press the hotkey multiple times to cycle through different width presets.

  

- `right` 

  Tile the active window to span the right half of the screen.  Press the hotkey multiple times to cycle through different width  presets.

  

- `bottom-left` 

  Tile the active window to span the bottom-left quarter of the  screen. Press the hotkey multiple times to cycle through different width  presets.

  

- `bottom`

  Tile the active window to span the bottom half of the screen.  Press the hotkey multiple times to cycle through different width  presets.

  

- `bottom-right`

  Tile the active window to span the bottom-right quarter of the  screen. Press the hotkey multiple times to cycle through different width  presets.

  

- `maximize` 

Maximizes the window.

- `show-desktop`

Minimizes all screens

## Conclusion

Hope that the app is of use to you. Downloads are on the releases part of the repo.