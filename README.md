# MousePlayback

![](https://imgur.com/6cVc8s2.png)

MousePlayback is an Windows Forms application that records your mouse- and keyboard clicks/movements after you press record, and plays them back when you press play.

You can use the buttons for this, or hotkeys. Default:

* F1: Start and stop recording
* F2: Start and stop playing back the recording

You can install MousePlayback by downloading the installer and running it. ([Setup.msi](https://github.com/Stefangansevles/MousePlayback/raw/master/Setup.msi))

# Features

* Function-keys (Ctrl / Alt / Shift) combinations with the keyboard (For example: ctrl + t)
* Function-keys (Ctrl / Alt / Shift) combinations with the mouse (For example: Shift + click/scroll)
* Infinite repeating of the recording, or customizable repeating (10 times, 20 times, etc.)
* (optional) randomized input. Tiny x/y differences between movements and random sleeps between playbacks
* Shortcut keys for recording / playing (Default: F1 / F2 )
* Export functionality
* Import functionality
* Full playback of all mouse and keyboard actions done while recording

----

Written in: C# - Windows Forms

Uses:

SQLite

Entity Framework

[Materialskin (Design of the application)](https://github.com/donaldsteele/MaterialSkin)

[Bunifu Framework (3 Buttons)](https://bunifuframework.com/)
