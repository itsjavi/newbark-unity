'use strict';
import {Melon} from 'externals';

/**
 * Represents a body that does not have a velocity when collides with another,
 * so it stops moving on collision but it does not bounce.
 *
 * @type {(me.Body|me.Rect)}
 */
let StaticCollisionBody = Melon.Body.extend({
  respondToCollision(response) {
    let overlap = response.overlapV;

    // Move out of the other object shape
    this.entity.pos.sub(overlap);
    // TODO: move to a full tile, calculating that pos.y/32 and pos.x/32 are integers

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

export default StaticCollisionBody;
