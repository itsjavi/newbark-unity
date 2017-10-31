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

    this.defaultSettings.anchorPoint = {
      // set the anchor point to X center and Y 8px (0.25 // 8px = 25% of 32)
      x: 0.5, y: (8 / 32)
    };

    // Debug frame
    this.debugAnimation.frames = [0];

    // Walking frames
    this.animations.walk_up.frames = [9, 8, 10, 8];
    this.animations.walk_right.frames = [2, 1];
    this.animations.walk_down.frames = [6, 5, 7, 5];
    this.animations.walk_left.frames = [4, 3];

    // Stand frames
    this.animations.stand_up.frames = [8];
    this.animations.stand_right.frames = [1];
    this.animations.stand_down.frames = [5];
    this.animations.stand_left.frames = [3];

  },
});
