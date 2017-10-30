import Melon from 'melonjs';
import Config from 'config';
import Controls from 'system/Controls';
import Sound from 'system/Sound';
import assets from 'assets';
import _ from "lodash";

let createAnimation = function (frames) {
  let animation = [];
  frames.forEach(function (frame) {
    if (isNaN(frame)) {
      animation.push(frame);
    } else {
      animation.push({
        name: frame,
        delay: 250
      });
    }
  });
  return animation;
};

let Controllable = Melon.Entity.extend({

  initProperties() {
    this.snapToGrid = true; // This will make every move to be by tile_size pixels
    this.tileSize = Config.video.tile_size;

    // Define an animation per desired button e.g. animationType_buttonName
    this.animations = {
      walk_up: {frames: []},
      walk_right: {frames: []},
      walk_down: {frames: []},
      walk_left: {frames: []},
      stand_up: {frames: []},
      stand_right: {frames: []},
      stand_down: {frames: []},
      stand_left: {frames: []},
    };

    this.initialAnimation = 'stand_down';

    /**
     * Current animation status
     * @type {{}}
     */
    this.animation = {
      name: null,
      axis: null, // x or y
      direction: null, // button name
      currentPixel: 0, // pixel buffer (in case is less than 1 per frame)
      remainingPixels: 0, // remaining pixels to move (usually max = tile size)
    };

    this.button = {
      get name(){
        return Controls.getPressed()
      },
      get animation(){
        return 'walk_' + this.name.toLowerCase();
      },
      get axis(){
        return Controls.getPressedAxis()
      },
      get oppositeAxis(){
        return Controls.getPressedOppositeAxis()
      }
    };

    // OLD :
    this.walkBuffer = {
      frames: 0,
      remainingPixels: 32,
      axis: null,
      direction: null
    };

    this.lastPressedButton = null;
  },

  init(x, y, settings = {}) {
    this.initProperties();

    settings = _.extend(
      {
        // collisionType: 'PLAYER_OBJECT',
        anchorPoint: {
          // set the anchor point to X center and Y -8px of one tile (25%)
          //x: 0.5, y: 0.25
          x: 0, y: 0
        }
      },
      settings
    );

    this._super(Melon.Entity, 'init', [x, y, settings]);

    // set the default horizontal & vertical speed (accel vector)
    this.body.vel.set(1, 1);
    this.body.maxVel.set(1, 1);
    this.body.friction.set(0, 0);

    //  do not fall
    this.body.gravity = 0;
    this.body.jumping = false;
    this.body.falling = false;

    // set the display to follow our position on both axis
    Melon.game.viewport.follow(this.pos, Melon.game.viewport.AXIS.BOTH);

    // ensure the player is updated even when outside of the viewport
    this.alwaysUpdate = true;

    // Register animations
    _.forOwn(this.animations, (animation, animationName) => {
      if (animation.frames.length === 0) {
        return;
      }

      this.renderable.addAnimation(animationName, createAnimation(
        (animation.frames.length === 1) ? animation.frames : createAnimation(animation.frames)
      ));
    });

    // set a standing animation as default
    this.renderable.setCurrentAnimation(this.initialAnimation);
  },

  moveTo(x, y) {

  },

  move(direction) {

  },

  isMoving() {

  },

  pause() {

  },

  stop() {

  },


  stopWalking() {
    if (this.lastPressedButton !== null) {
      this.renderable.setCurrentAnimation("stand_" + this.lastPressedButton.toLowerCase());
      this.lastPressedButton = null;
    }
  },

  walk(direction) {
    let axis, oppositeAxis, pixelsPerFrame = Config.video.tile_size / Melon.sys.fps;

    axis = Controls.getPressedAxis(direction);

    if (!axis) {
      return false;
    }

    oppositeAxis = Controls.getPressedOppositeAxis(axis);
    this.walkBuffer.frames++;

    if (
      this.walkBuffer.frames === Config.video.tile_size
      && this.walkBuffer.direction === direction
    ) {
      // Finished
      this.stopWalking();
      this.walkBuffer.frames = 0;
      return false;
    }

    if (this.walkBuffer.direction !== direction) {
      // New direction
      this.walkBuffer.frames = 0;
    }
    this.walkBuffer.axis = axis;
    this.walkBuffer.direction = direction;

    if (!Config.player.move_diagonally) {
      this.body.vel[oppositeAxis] = 0;
    }

    // update the entity velocity
    if (direction === Controls.LEFT || direction === Controls.UP) {
      this.body.vel[axis] -= pixelsPerFrame;
    } else {
      this.body.vel[axis] += pixelsPerFrame;
    }

    //console.log(this.walkBuffer.frames);

    // change to the walking animation
    if (!this.renderable.isCurrentAnimation("walk_" + direction.toLowerCase())) {
      this.renderable.setCurrentAnimation("walk_" + direction.toLowerCase());
    }
    this.lastPressedButton = direction;
  },

  /**
   * update the entity
   */
  update(dt) {
    let pressedButton = Controls.getPressed();

    if (
      pressedButton
      && Controls.isDirectionButtonPressed()
      && this.walkBuffer.frames === 0
    ) {
      this.walk(pressedButton);
    } else if (
      this.walkBuffer.frames > 0
      && this.walkBuffer.frames < Melon.sys.fps
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
    Melon.collision.check(this);

    // return true if we moved or if the renderable was updated
    return (this._super(Melon.Entity, 'update', [dt]) || this.body.vel.x !== 0 || this.body.vel.y !== 0);
  },

  /**
   *
   * Collision handler
   * (called when colliding with other objects)
   * @param {Melon.collision.ResponseObject} collisionResponse
   * @param {Melon.Entity|Melon.Renderable} collisionObject
   * @returns {boolean} Return false to avoid collision, return true or nothing to collide.
   */
  onCollision(collisionResponse, collisionObject) {
    if (collisionResponse.overlap < 0.9) {
      return false;
    }

    collisionResponse.a.body.gravity = 0;
    collisionResponse.a.body.friction.set(0, 0);
    collisionResponse.a.body.jumping = false;
    collisionResponse.a.body.falling = false;

    collisionResponse.b.body.gravity = 0;
    collisionResponse.b.body.friction.set(0, 0);
    collisionResponse.b.body.jumping = false;
    collisionResponse.b.body.falling = false;

    collisionObject.body.gravity = 0;
    collisionObject.body.friction.set(0, 0);
    collisionObject.body.jumping = false;
    collisionObject.body.falling = false;

    Sound.playEffect(assets.audios.collide);

    console.log("Collided:'" + collisionResponse.a.name + "' with '" + collisionResponse.b.name + "'");
    console.info(collisionObject);
    //return true;
  },
});

export default Controllable;
