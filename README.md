# Stardew Valley To-Dew List

An in-game to-do list mod for Stardew Valley.  Horrible pun included at no extra charge.

You can find this mod on [Nexus Mods](https://www.nexusmods.com/stardewvalley/mods/7409)
if you prefer.


## Installation

Follow the usual installation proceedure for SMAPI mods:
1. Install [SMAPI](https://smapi.io)
2. Download the latest release of this mod and unzip it into the `Mods` directory
3. Run the game using SMAPI


## How To Use

Bring up the to-do list by pressing the configurable hotkey (default `L`
for "**l**ist").  If you have the [Mobile Phone﻿](https://www.nexusmods.com/stardewvalley/mods/6523)
mod installed then you can also bring up the list with the mobile phone app
(which does exactly the same thing as the hotkey).


Edit your list.  See "List Editing" below for more information on editing.

Press escape to close the list.

List data is saved in the game save file, so any changes since the last
save will be lost when you exit.

The list is also displayed in an overlay in the top left corner during game
play.  This can be disabled in the configuration.  When the overlay is enabled,
its visibility can be toggled with a hotkey, if you configure the hotkey.
(By default, the hotkey is not configured.  Or to be pedantic, configured
to `None`, which matches no buttons or keys.)

### List Editing

Enter text in the textbox.  Press enter to add it to the list.

Use the scroll gesture on your mouse or trackpad to scroll the list if it
has more items than fit on one screen.  (Arrows will appear to indicate
that there are more items.)

Click a list item to remove it from the list.

Use the small up and down arrows to the right of items to reorder them.

Right-clicking on an item will copy its text to the textbox (replacing
whatever is there).  The textbox implementation is from the base game,
and as far as I can tell you can only add or remove characters from the
end.  If you see any open-source mods that have more advanced text editing
capabilities then let me know and I'll take a look to see how they're done.

## Configuration

When SMAPI runs the mod for the first time it will create a `config.json`
in the mod directory.  You can edit this file to configure the hotkey for
opening the to-do list and for various properties of the overlay (including
whether it is enabled at all).

If [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098)
is installed then there will be an entry for To-Dew in the in-game config
menu.


## Compatibility

Works with Stardew Valley 1.5 / SMAPI 3.8, single and multiplayer (but see
further notes on Multiplayer below).  No known incompatibilities with
other mods.

I have no idea what things do or do not work with split-screen.  As
far as I can tell you have to have controllers to even start split-screen,
and I don't.


## Multiplayer Support

There is a single list for the farm, and everyone (with the mod
installed) can edit it.

List data is saved in the game save file, which is only accessible to the
host.  So, in order to use the to-do list, the host must have To-Dew
installed.  Farmhands that have To-Dew installed will also be able to use
the list.  If a farmhand has To-Dew installed but the host does not then
the farmhand will not be able to use the to-do list.  (I.e., there is no
combination of places where the mod is installed or not that will result
in the game being unplayable.)

If the host and a farmhand have different versions of To-Dew installed,
the mod attempts to make intelligent decisions about what to do.  Look for
messages in the SMAPI console log.  This scenario is probably not
well-tested and may contain bugs.

## Known Issues

* The overlay will be partially (or fully) hidden by the black bars drawn
  on the left and right sides of the screen on maps that are narrower than
  the screen (which depends on the screen, but most likely e.g. is the bus
  stop).