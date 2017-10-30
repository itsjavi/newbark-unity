'use strict';
import Engine from 'system/Engine';
import Game from 'system/Game';
import LoadingScreen from 'scenes/LoadingScreen';

Engine
  .load(LoadingScreen)
  .play(Game);
