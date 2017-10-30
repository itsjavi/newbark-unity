'use strict';
import Controllable from 'system/Controllable';

/**
 * Main player entity
 *
 * Name: player
 * Layer: characters
 */
export default Controllable.extend({
  initProperties() {
    this._super(Controllable, 'initProperties');

    this.animations.walk_up.frames = [8, 7, 9, 7];
    this.animations.walk_right.frames = [1, 0];
    this.animations.walk_down.frames = [5, 4, 6, 4];
    this.animations.walk_left.frames = [3, 2];

    this.animations.stand_up.frames = [7];
    this.animations.stand_right.frames = [0];
    this.animations.stand_down.frames = [4];
    this.animations.stand_left.frames = [2];
  },
});
