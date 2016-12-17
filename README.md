# poketiled
Old-Style Pokemon RPG Game Engine written in MelonJS and Tiled Maps

Features :
- swiping disabled on iOS devices
- debug Panel (if #debug)
- default icons
- distribution build
- standalone build for desktop operating systems

## Getting started

To build, be sure you have [node](http://nodejs.org) installed. Clone the project:

    git clone https://github.com/metaunicorn/poketiled.git

Then in the cloned directory, simply run:

    npm install

You must also have `grunt-cli` installed globally:

    npm install -g grunt-cli

Running the game:

	grunt serve

And you will have the project running on http://localhost:8000

## Building Release Versions

### For browsers or mobile apps

When running `grunt` inside the project root folder, it  will create a `build`
directory containing the files that can be uploaded to a server,
or packaged into a mobile app.

Note that you may have to edit the file `Gruntfile.js` if you need to better
dictate the order your files load in.

----

## For standalone desktop versions

Standalone desktop versions will be automatically built using Electron.
For building an executable application for your current Operative System,
you just need to run `grunt && grunt dist` under this project root folder.

Then one of this files will be generated:

- Windows: `bin\electron.exe`
- macOS: `bin/Electron.app`
- Linux: `bin/electron`

Note that you may have to edit the file `main.js` in order to fine-tune
better the Electron app initialization.
