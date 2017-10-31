# NewBark
🌳 Retro Pokémon RPG game engine built on top of MelonJS and compatible with Tiled maps editor.

![screenshot](screenshot.png)

This is currently an early stage prototype and it is not intended to be a final game.
The main goal of this project is to build an RPG game engine for the web, inspired and based on the Pokemon games.

## Setup

First, you may need `gulp` installed globally.

Clone the project:

    git clone https://github.com/pokettomonstaa/NewBark.git
    cd NewBark

Then in the cloned directory, simply run:

    yarn install
    gulp

## Start

After building you need to run the game from a valid HTTP URL (it won't work with local file:// urls).
The recommended way for running it locally is using [`http-server`](https://www.npmjs.com/package/http-server),
which comes already bundled as npm start script:

    npm start

This will serve the static `dist/index.html` file and all the assets at 
[localhost:8080](http://localhost:8080) by default.

## Debug

For enabling the debug mode you need two things: set it to `true` in the config, and reload the game
URL using [`#debug`](http://localhost:8080/#debug) in the URL hash.

Debug mode enables the debug grid layer and some internal variables like FPS, current movement, collision object name,
etc.

## License

This software is copyrighted and licensed under the 
[MIT license](https://github.com/pokettomonstaa/NewBark/LICENSE).

### Disclaimer

This software comes bundled with data and graphics extracted from the
Pokémon series of video games. Some terminology from the Pokémon franchise is
also necessarily used within the software itself. This is all the intellectual
property of Nintendo, Creatures, inc., and GAME FREAK, inc. and is protected by
various copyrights and trademarks.

The author believes that the use of this intellectual property for a fan reference
is covered by fair use and that the software is significantly impaired without said
property included. Any use of this copyrighted property is at your own legal risk.

This software is not affiliated in any way with Nintendo,
Pokémon or any other game company.

A complete revision history of this software is available from
https://github.com/pokettomonstaa/NewBark
