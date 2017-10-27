import me from 'melonjs';
import config from 'config';
import audio from 'system/audio';
import buttons from 'system/buttons';
import utils from 'system/utils';
import HUD from 'system/HUD';
import assets from 'build/assets';
import * as entities from 'build/entities';
import * as scenes from 'build/scenes';

me.DefaultLoadingScreen = scenes.LoadingScreen;

/**
 * @global
 */
var game = {
  ENV: {
    isElectron: config.isElectron
  },
  me: me,
  data: { // an object where to store game information
    // score
    score: 0
  },
  HUD: HUD,
  config: config,
  assets: assets,
  audio: audio,
  buttons: buttons,
  utils: utils,
  entities: entities,
  scenes: scenes,

  // Run on page load.
  "load": function () {
    // Set global game FPS
    me.sys.fps = this.config.fps;

    // Initialize the video.
    if (!me.video.init
      (
        this.config.screen.getWidth(),
        this.config.screen.getHeight(),
        {
          wrapper: this.config.screen.wrapper, // ID of the HTML element
          scale: this.utils.getScreenScale(),
          renderer: me.video.CANVAS
        }
      )
    ) {
      alert("Your browser does not support HTML5 canvas or WebGL renderer.");
      return;
    }

    if (document.getElementById('debugPanelJs') && game.config.debug) {
      me.debug.renderHitBox = true;
    }

    // Initialize the audio.
    me.audio.init("mp3,ogg");

    // Set and load all resources.
    // This will also automatically switch to the loading screen (me.state.LOADING)
    // It will call 'onLoad' once all resources are loaded.
    me.loader.preload(this.assets, this.onLoad.bind(this));
  },

  // Run on game resources loaded.
  "onLoad": function () {
    // add our entities in the entity pool
    for (let entityName in this.config.entities) {
      if (!game.entities[this.config.entities[entityName]]) {
        throw new Error("Entity is undefined: " + entityName);
      }
      me.pool.register(entityName, game.entities[this.config.entities[entityName]]);
    }

    // Bind game buttons
    this.buttons.bind();

    // Start the game
    this.start();
  },

  // Loads to the initial scene(s)
  "start": function () {
    // Sets the scenes for the different states
    me.state.set(me.state.MENU, new this.scenes.TitleScreen());
    me.state.set(me.state.PLAY, new this.scenes[this.config.scenes.initial]());

    // Start the game.
    me.state.change(me.state.PLAY);
  },

  "restart": function () {
    // WIP
    this.start();
  },

  "pause": function () {
    // WIP
  },

  "resume": function () {
    // WIP
  },

  "save": function () {
    // WIP
  },
};

export default game;
