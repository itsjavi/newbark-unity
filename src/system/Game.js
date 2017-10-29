"use strict";
import Melon from 'melonjs';
import Config from 'config';
import Screen from 'system/Screen';
import Player from 'system/Player';
import Controls from 'system/Controls';
import assets from 'assets';
import scenes from 'scenes';

export default {
  // Run on page load.
  load() {
    // Initialize the video.
    if (!Melon.video.init
      (
        Screen.width,
        Screen.height,
        {
          wrapper: Config.wrapper, // ID of the HTML element
          scale: Screen.scale,
          renderer: Melon.video[Config.video.renderer],
          antiAlias: false
        }
      )
    ) {
      alert("Your browser does not support the HTML5 " + Config.video.renderer + " renderer.");
      return;
    }

    if (Config.debug && Melon.debug) {
      Melon.debug.renderHitBox = true;
    }

    // Initialize the audio.
    Melon.audio.init("mp3,ogg");

    // Set and load all resources.
    // This will also automatically switch to the loading screen (Melon.state.LOADING)
    // It will call 'onLoad' once all resources are loaded.
    Melon.loader.preload(assets._files, this.onLoad.bind(this));
  },

  // Run on game resources loaded.
  onLoad() {
    // Add the player to the entity pool
    Melon.pool.register('player', Player);

    // Bind game buttons
    Controls.bind();

    // Start the game
    this.start();
  },

  // Loads to the initial scene(s)
  start() {
    // Sets the scenes for the different states
    Melon.state.set(Melon.state.READY, new scenes.TitleScreen());
    Melon.state.set(Melon.state.PLAY, new scenes[Config.initial_scene]());

    // Start the gaMelon.
    Melon.state.change(Melon.state.PLAY);
  },

  reset() {
    this.start();
  }
};
