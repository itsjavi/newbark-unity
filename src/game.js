/* Game namespace */
var game = {
  ENV: {
    isElectron: false
  },
  data: { // an object where to store game information
    // score
    score: 0
  },
  config: {},
  entities: {},
  scenes: {},

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
    for (var entityName in this.config.entities) {
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