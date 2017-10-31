import Melon from 'melonjs';
import Config from 'config';

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
   * @param {Number} value
   */
  static set fps(value) {
    Melon.sys.fps = value;
  }

  /**
   * Number of pixels to move every time a direction button is pressed
   * @return {Number}
   */
  static get pixelsPerMove() {
    if (Config.player.pixels_per_move === undefined) {
      return Config.video.tile_size;
    }
    return Config.player.pixels_per_move;
  }

  /**
   * @param {Number} value
   */
  static set pixelsPerMove(value) {
    Config.player.pixels_per_move = value;
  }

  /**
   * Number of pixels to move every frame
   * @return {Number}
   */
  static get pixelsPerFrame() {
    if (Config.player.pixels_per_frame === undefined) {
      return (this.pixelsPerMove / this.fps);
    }
    return Config.player.pixels_per_frame;
  }

  /**
   * @param {Number} value
   */
  static set pixelsPerFrame(value) {
    Config.player.pixels_per_frame = value;
  }

  /**
   * Velocity added or subtracted every frame
   * @return {Number}
   */
  static get velocityFactor() {
    if (Config.player.velocity_factor === undefined) {
      return 1;//this.pixelsPerFrame;
    }
    return Config.player.velocity_factor;
  }

  /**
   * @param {Number} value
   */
  static set velocityFactor(value) {
    Config.player.velocity_factor = value;
  }

  /**
   * @return {Boolean}
   */
  static get allowDiagonal() {
    if (Config.player.allow_diagonal === undefined) {
      return false;
    }
    return Config.player.allow_diagonal;
  }

  /**
   * @param {Boolean} value
   */
  static set allowDiagonal(value) {
    Config.player.allow_diagonal = value;
  }
}


export default Movement;
