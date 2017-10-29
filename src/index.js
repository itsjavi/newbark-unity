"use strict";
import Melon from 'melonjs';
import Game from 'system/Game';
import LoadingScreen from 'scenes/LoadingScreen';

if (window === undefined) {
  throw new Error('The environment is not compatible. The window object is undefined.');
}

// Set the default loading screen
Melon.DefaultLoadingScreen = LoadingScreen;

window.onReady(
  function onReady() {
    Game.load();
  }
);
