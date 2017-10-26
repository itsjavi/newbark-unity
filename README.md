# NewBark
🌳 Retro Pokémon RPG game engine built on top of MelonJS and compatible with Tiled maps editor.

## Features
- built on top of MelonJS, with a modern toolchain
- swiping disabled on iOS devices
- debug Panel (if #debug)
- default icons
- distribution build
- standalone build for desktop operating systems

## Build

First, you may need `gulp` installed globally.

Clone the project:

    git clone https://github.com/pokettomonstaa/NewBark.git

Then in the cloned directory, simply run:

    cd NewBark
    yarn install
    gulp

## Run it

After building you need to run the game from a valid HTTP URL (it won't work with local file:// urls).
The recommended way is to run it locally it using [`http-server`](https://www.npmjs.com/package/http-server):

    http-server ./dist/

This will serve the `index.html` file and any assets.
