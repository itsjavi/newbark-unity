import melon from 'melonjs';

/* melonjs engine wrapper */
let engine = {
  /**
   * @return {Number}
   */
  get fps(){
    return melon.sys.fps;
  },
  /**
   * @param {Number} value
   */
  set fps(value){
    melon.sys.fps = value;
  },
  /**
   *
   * @param {melon.ScreenObject} loadingScreen
   * @returns {engine}
   */
  load(loadingScreen = null) {
    if (window === undefined) {
      throw new Error('The environment is not compatible. The window object is undefined.');
    }

    // Set the default loading screen
    if (loadingScreen) {
      melon.DefaultLoadingScreen = loadingScreen;
    }

    // Set global game FPS
    // 59.7 was the original GB and GBC frame rate
    this.fps = 60;
    melon.sys.gravity = 0;

    return this;
  },

  /**
   * @param {{load: Function}} game
   * @returns {engine}
   */
  play(game) {
    window.onReady(
      function onReady() {
        // Load and start the game
        game.load();
      }
    );

    return this;
  }
};

export default engine;
