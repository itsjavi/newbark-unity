"use strict";
import Melon from 'melonjs';
import Game from 'system/Game';
import Config from 'config';
import LoadingScreen from 'scenes/LoadingScreen';

if (window === undefined) {
  throw new Error('The environment is not compatible. The window object is undefined.');
}
// Set the default loading screen
Melon.DefaultLoadingScreen = LoadingScreen;

// Set global game FPS
Melon.sys.fps = Config.video.fps;

window.onReady(
  function onReady() {
    // Load and start the game
    Game.load();
  }
);
