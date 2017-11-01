'use strict';
import {Melon, assets} from 'externals';
import Config from 'config';
import Resolution from 'system/Resolution';
import Controls from 'system/Controls';
import Debug from 'system/Debug';
import LoadingScreen from 'scenes/LoadingScreen';
import StaticCollisionBody from 'system/physics/StaticCollisionBody'

class GameLoader {

  /**
   * Initializes the engine and triggers the Game.load function whenever the window is ready.
   * This needs to be called before anything else.
   *
   * @param {system/Game} gameClass Game to load
   */
  constructor(gameClass) {
    if (window === undefined) {
      throw new Error('The environment is not compatible. The window object is undefined.');
    }

    // Create and expose the game object
    /**
     * @type {system/Game.prototype}
     */
    this.game = Melon.$game = new gameClass(Config);

    // Set FPS
    Melon.sys.fps = Config.video.fps;

    // Disable gravity
    Melon.sys.gravity = 0;

    // For this engine we don't need to apply physics when colliding
    Melon.Body = StaticCollisionBody;

    // Set the default loading screen
    Melon.DefaultLoadingScreen = LoadingScreen;

    Resolution.init();
    Debug.init();
  }

  /**
   * Initializes video and audio and calls the onLoad function when the assets are pre-loaded.
   */
  load() {
    // Initialize the video.
    if (!Melon.video.init
      (
        Resolution.defaultWidth,
        Resolution.defaultHeight,
        {
          wrapper: Config.wrapper, // ID of the HTML element
          scale: Resolution.scale,
          renderer: Melon.video[Config.video.renderer],
          antiAlias: false
        }
      )
    ) {
      alert("Your browser does not support the HTML5 " + Config.video.renderer + " renderer.");
      return;
    }

    // Initialize the audio.
    Melon.audio.init("mp3,ogg");

    // Initialize and bind the controls
    Controls.bind();

    // Set and load all resources.
    // This will also automatically switch to the loading screen (Melon.state.LOADING)
    // It will call 'onLoad' once all resources are loaded.
    Melon.loader.preload(assets._files, this.game.start.bind(this.game));
  }

  loadOnReady() {
    window.onReady(
      () => {
        // Load the game
        this.load();
      }
    );
  }
}

export default GameLoader;
