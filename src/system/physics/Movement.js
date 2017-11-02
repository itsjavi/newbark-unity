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
      config.player.velocity = Calc.velocity(this.distancePerMove, this.fps) * config.player.speed;
    }
    return config.player.velocity;
  }

  /**
   * @return {Boolean}
   */
  static get allowDiagonal() {
    if (config.player.allow_diagonal === undefined) {
      config.player.allow_diagonal = false;
    }
    return config.player.allow_diagonal;
  }
}

export default Movement;
