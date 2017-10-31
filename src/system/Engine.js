import Melon from 'melonjs';
import Config from 'config';

/* melonjs engine wrapper */
let engine = {
  /**
   * @return {Number}
   */
  get fps() {
    return Melon.sys.fps;
  },
  /**
   * @param {Number} value
   */
  set fps(value) {
    Melon.sys.fps = value;
  },
  /**
   *
   * @param {Melon.ScreenObject} loadingScreen
   * @returns {engine}
   */
  load(loadingScreen = null) {
    if (window === undefined) {
      throw new Error('The environment is not compatible. The window object is undefined.');
    }

    // Set global game FPS (note that 59.7 was the original GB and GBC frame rate)
    this.fps = Config.video.fps;

    // Disable gravity
    Melon.sys.gravity = 0;

    // Set debug to false (even if true) if the location does not have #debug
    Config.debug = (Config.debug === true
      && window.location.hash && (window.location.hash.toLowerCase().match(/debug/ig)));

    if (Config.debug) {
      // Make it available so it can be tweaked on run time
      Melon.game.config = Config;
    }

    if (Config.video.preset === 'GBC' && Config.video.scale === 2) {
      document.body.classList.add('with-frame');
    }

    // For this engine we don't need to apply physics when colliding
    Melon.Body = Melon.Body.extend({
      respondToCollision(response) {
        let overlap = response.overlapV;

        // Move out of the other object shape
        this.entity.pos.sub(overlap);

        // adjust velocity
        if (overlap.x !== 0) {
          this.vel.x = 0;
        }
        if (overlap.y !== 0) {
          this.vel.y = 0;
        }
        // Cancel falling and jumping
        this.falling = false;
        this.jumping = false;
      }
    });

    if (!Config.debug) {
      // Hide debug elements if not on debug mode
      Melon.game.onLevelLoaded = () => {
        Melon.game.world.children.forEach(function (child, i) {
          if (child.name.match(/debug/ig)) {
            // Hide debug-only elements
            Melon.game.world.children[i].alpha = 0;
          }
        });
      };
    }

    // Set the default loading screen
    if (loadingScreen) {
      Melon.DefaultLoadingScreen = loadingScreen;
    }

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
