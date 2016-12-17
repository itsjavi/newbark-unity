/**
 * Player Entity
 */
game.entities.Player = me.Entity.extend({
  /**
   * constructor
   */
  init: function (x, y, settings) {
    // call the constructor
    this._super(me.Entity, 'init', [x, y, settings]);

    // set the default horizontal & vertical speed (accel vector)
    this.body.setVelocity(1, 1);
    this.body.setFriction(0, 0);

    // set the anchor point to X center and Y -8px of one tile (25%)
    this.anchorPoint.set(0.5, 0.25);

    //  do not fall
    this.body.gravity = 0;
    this.body.jumping = false;
    this.body.falling = false;

    // set the display to follow our position on both axis
    me.game.viewport.follow(this.pos, me.game.viewport.AXIS.BOTH);

    // ensure the player is updated even when outside of the viewport
    this.alwaysUpdate = true;

    var createAnimation = function (framesIds) {
      var animation = [];
      framesIds.forEach(function (id) {
        animation.push({
          name: id,
          delay: 250
        });
      });
      return animation;
    };

    // define walking animations
    this.renderable.addAnimation("walk_left", createAnimation([3, 2]));
    this.renderable.addAnimation("walk_right", createAnimation([1, 0]));
    this.renderable.addAnimation("walk_up", createAnimation([8, 7, 9, 7]));
    this.renderable.addAnimation("walk_down", createAnimation([5, 4, 6, 4]));

    // define standing animations
    this.renderable.addAnimation("stand_left", [2]);
    this.renderable.addAnimation("stand_right", [0]);
    this.renderable.addAnimation("stand_up", [7]);
    this.renderable.addAnimation("stand_down", [4]);

    // set a standing animation as default
    this.renderable.setCurrentAnimation("stand_down");


    // NEW Variable Initialization:
    this.walkBuffer = {frames: 0, axis: null, direction: null};
    this.lastPressedButton = null;
  },

  incrementWalkBuffer_old: function () {
    if (this.walkBuffer.frames < me.sys.fps) { // Let's have a -1 frame error margin
      this.walkBuffer.shouldMove = false;
      this.walkBuffer.frames++;
    } else {
      //this.lastPressedButton = null;
      this.walkBuffer.shouldMove = true;
      this.walkBuffer.frames = 0;
    }
  },

  stopWalking: function () {
    if (this.lastPressedButton !== null) {
      this.renderable.setCurrentAnimation("stand_" + this.lastPressedButton);
      this.lastPressedButton = null;
    }
  },

  walk: function (direction) {
    var axis, secondAxis, diffVel = (game.config.tileSize / me.sys.fps);

    axis = game.buttons.getPressedAxis(direction);

    if (!axis) {
      return false;
    }

    secondAxis = game.buttons.getSecondAxis(axis);
    this.walkBuffer.frames++;

    if (
      this.walkBuffer.frames == game.config.tileSize
      && this.walkBuffer.direction == direction
    ) {
      // Finished
      this.stopWalking();
      this.walkBuffer.frames = 0;
      return false;
    }

    if (this.walkBuffer.direction != direction) {
      // New direction
      this.walkBuffer.frames = 0;
    }
    this.walkBuffer.axis = axis;
    this.walkBuffer.direction = direction;

    if (!game.config.player.moveDiagonal) {
      this.body.vel[secondAxis] = 0;
    }

    // update the entity velocity
    if (direction == game.buttons.LEFT || direction == game.buttons.UP) {
      this.body.vel[axis] -= diffVel;
    } else {
      this.body.vel[axis] += diffVel;
    }

    //console.log(this.walkBuffer.frames);

    // change to the walking animation
    if (!this.renderable.isCurrentAnimation("walk_" + direction)) {
      this.renderable.setCurrentAnimation("walk_" + direction);
    }
    this.lastPressedButton = direction;
  },

  /**
   * update the entity
   */
  update: function (dt) {
    var pressedButton = game.buttons.getPressed();

    if (
      pressedButton
      && game.buttons.isDirectionButtonPressed()
      && this.walkBuffer.frames == 0
    ) {
      this.walk(pressedButton);
    } else if (
      this.walkBuffer.frames > 0
      && this.walkBuffer.frames < me.sys.fps
    ) {
      // Finish animation
      this.walk(this.walkBuffer.direction);
    } else {
      this.walkBuffer.frames = 0;
      this.body.vel.x = 0;
      this.body.vel.y = 0;
      this.stopWalking();
    }

    // apply physics to the body (this moves the entity)
    this.body.update(dt);

    // handle collisions against other shapes
    me.collision.check(this);

    // return true if we moved or if the renderable was updated
    return (this._super(me.Entity, 'update', [dt]) || this.body.vel.x !== 0 || this.body.vel.y !== 0);
  },

  /**
   *
   * Collision handler
   * (called when colliding with other objects)
   * @param {me.collision.ResponseObject} collisionResponse
   * @param {me.Entity|me.Renderable} collisionObject
   * @returns {boolean} Return false to avoid collision, return true or nothing to collide.
   */
  onCollision: function (collisionResponse, collisionObject) {
    if (collisionResponse.overlap < 1) {
      return false;
    }

    collisionResponse.b.body.gravity = 0;
    collisionResponse.b.body.friction.set(0, 0);
    collisionObject.body.gravity = 0;
    collisionObject.body.friction.set(0, 0);

    //console.log("Collided:'" + collisionResponse.a.name + "' with '" + collisionResponse.b.name + "'");
    game.audio.playSfx("collide");
    //return true;
  },

  getCurrentTile: function () {

  },

  getTileAt: function (x, y) {

  }
});
