'use strict';
import {Melon, config} from 'externals';
import Calc from './Calc';

// TODO: convert class members into non-static and have pixel calculation functions and buffers inside.

/**
 * Snap to grid movement style helper class
 */
class Movement {
  /**
   * @return {Number}
   */
  static get fps() {
    return Melon.sys.fps;
  }

  /**
   * Constant number of pixels to move every time a direction button is pressed.
   * @return {Number}
   */
  static get distancePerMove() {
    if (config.player.distance_per_move === undefined) {
      config.player.distance_per_move = config.video.tile_size;
    }
    return config.player.distance_per_move;
  }

  /**
   * Constant velocity. Number of pixels to move per frame.
   * @return {Number}
   */
  static get velocity() {
    if (config.player.velocity === undefined) {
      let vel = Calc.velocity(config.video.tile_size, this.fps);

      vel = ((vel <= 0.5) ? 1 : Math.round(vel)) * config.player.speed; // assure minimum of 1 and multiply by speed.

      config.player.velocity = Math.ceil(vel / 2) * 2; // assure velocity is always multiple of 2
    }
    return config.player.velocity;
  }
}

export default Movement;
