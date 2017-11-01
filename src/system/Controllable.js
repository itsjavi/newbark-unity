'use strict';
import {Melon, assets, _} from 'externals';
import Controls from 'system/Controls';
import Sound from 'system/Sound';
import Movement from 'system/Movement';
import Debug from 'system/Debug';

let createAnimation = function (frames) {
  if (frames.length === 1) {
    // no delay needed
    return frames;
  }

  let animation = [];
  frames.forEach(function (frame) {
    if (frame.name) { // e.g. {name:7, delay: 100}
      animation.push(frame);
    } else if (_.isArray(frame) && (frame.length >= 2)) { // e.g. [7, 100]
      animation.push({
        name: frame[0],
        delay: frame[1]
      });
    } else { // e.g. 7
      animation.push({
        name: frame,
        delay: 125
      });
    }
  });
  return animation;
};

let Controllable = Melon.Entity.extend({
  initProperties() {
    this.defaultSettings = {
      anchorPoint: new Melon.Vector2d(0, 0)
    };
    this.pixelBuffer = 0;
    this.remainingPixels = 0;
    this.lastPressedButton = null;

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
    this.debugAnimation = {frames: [0]};
  },

  init(x, y, settings = {}) {
    this.initProperties();

    settings = _.extend(this.defaultSettings, settings);

    this._super(Melon.Entity, 'init', [x, y, settings]);

    this.mainSprite = this.renderable;

    // Register main sprite animations
    _.forOwn(this.animations, (animation, animationName) => {
      if (animation.frames.length === 0) {
        return;
      }

      this.mainSprite.addAnimation(animationName, createAnimation(animation.frames));
    });

    if (Debug.enabled) {
      this.debugSprite = new Melon.Sprite(0, 0, {
          "image": this.mainSprite.image,
          "framewidth": this.mainSprite.framewidth,
          "frameheight": this.mainSprite.frameheight,
          "spacing": this.mainSprite.spacing,
          "margin": this.mainSprite.margin
        }
      );
      this.debugSprite.addAnimation('debug', createAnimation(this.debugAnimation.frames));
      this.debugSprite.setCurrentAnimation('debug');
      this.debugSprite.alwaysUpdate = true;
      this.mainSprite.alwaysUpdate = true;
      this.debugSprite.inViewport = true;
      this.mainSprite.inViewport = true;

      // Wrap sprites in a container
      let container = new Melon.Container();
      container.ancestor = this;
      container.inViewport = true;
      container.alwaysUpdate = true;
      container.updateBoundsPos(0, 0);
      container.addChild(this.debugSprite);
      container.addChild(this.mainSprite);
      container.updateChildBounds();
      this.renderable = container;
    }

    // set a standing animation as default
    this.mainSprite.setCurrentAnimation(this.initialAnimation);

    // set the default horizontal & vertical speed (accel vector)
    this.body.vel.set(1, 1);
    this.body.maxVel.set(1, 1);
    this.body.friction.set(0, 0);

    //  do not fall
    this.body.gravity = 0;
    this.body.jumping = false;
    this.body.falling = false;

    // Set the sprite anchor point
    this.anchorPoint.set(settings.anchorPoint.x, settings.anchorPoint.y);

    // set the display to follow our position on both axis
    Melon.game.viewport.follow(this.pos, Melon.game.viewport.AXIS.BOTH);

    // ensure the player is updated even when outside of the viewport
    this.alwaysUpdate = true;
  },

  /**
   * update the entity
   */
  update(deltaTime) {
    this.renderable.inViewport = true;
    this.debugSprite.inViewport = true;
    this.mainSprite.inViewport = true;

    if (!this.isMoving()) {
      this.body.vel.x = 0;
      this.body.vel.y = 0;
      if (this.lastPressedButton) {
        this.mainSprite.setCurrentAnimation("stand_" + this.lastPressedButton.toLowerCase());
      }
      this.lastPressedButton = null;
    }

    let direction = this.getMoveDirection();

    if (!direction && !this.isMoving()) {
      // No action and no pending animation
      return false;
    }

    if (direction && !this.lastPressedButton) {
      // Assign last button
      this.lastPressedButton = direction;
    }

    if (direction && !this.isMoving()) {
      // New move
      this.remainingPixels = Movement.pixelsPerMove;
    }

    if (direction && this.processPixelsToMove()) {
      this.move(direction);
    }

    if (Debug.enabled) {
      Debug.debugUpdate(direction, deltaTime, this.remainingPixels, this.pixelBuffer);
    }

    // Always show
    if (this.debugSprite !== undefined) {
      this.debugSprite.inViewport = Debug.enabled === true;
    }
    this.mainSprite.inViewport = true;

    // ---------------------------------------------------------------------------------------------------------
    // apply physics to the body (this moves the entity)
    this.body.update(deltaTime);

    // handle collisions against other shapes
    Melon.collision.check(this);

    // return true if we moved or if the renderable was updated
    return (this._super(Melon.Entity, 'update', [deltaTime]) || this.body.vel.x !== 0 || this.body.vel.y !== 0);
  },

  move(pressedButton) {
    if (!pressedButton) {
      throw new Error(
        `Called move() with no button.`
      );
    }

    let axis = Controls.getPressedAxis(pressedButton);

    if (!axis) {
      return false;
    }

    let oppositeAxis = Controls.getPressedOppositeAxis(axis);

    if (!Movement.allowDiagonal) {
      this.body.vel[oppositeAxis] = 0;
    }

    if (pressedButton === Controls.LEFT || pressedButton === Controls.UP) {
      this.body.vel[axis] -= Movement.velocityFactor;
    } else {
      this.body.vel[axis] += Movement.velocityFactor;
    }

    // change to the walking animation
    if (!this.mainSprite.isCurrentAnimation("walk_" + pressedButton.toLowerCase())) {
      this.mainSprite.setCurrentAnimation("walk_" + pressedButton.toLowerCase());
    }
  },

  /**
   * @returns {Integer}
   */
  processPixelsToMove() {
    let minPixToMove = 1;

    if (this.remainingPixels === 0 && this.pixelBuffer === 0) {
      // Do not move
      return 0;
    }

    if (this.remainingPixels > 0) {
      this.pixelBuffer += Movement.pixelsPerFrame;
    }

    if (this.remainingPixels === 0 && ((this.pixelBuffer < 1) && (this.pixelBuffer > 0))) {
      // Remaining decimals, means that we only have 1 pixel left
      this.pixelBuffer = 0;
      return minPixToMove;
    }

    if (this.pixelBuffer < 1) {
      // Do not move until it is 1 or greater
      return 0;
    }

    if (this.remainingPixels === 0 && (this.pixelBuffer > 1)) {
      console.warn(
        `Unexpected value for pixelBuffer: ${this.pixelBuffer}. Remaining pixels: ${this.remainingPixels}`
      );
      // Remaining decimals
      this.pixelBuffer -= 1;
      return 1;
    }

    this.remainingPixels -= minPixToMove;
    this.pixelBuffer -= minPixToMove;
    return minPixToMove;
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
    if (collisionResponse.overlap < 1) {
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

    Debug.debugCollision(collisionResponse);
  },

  isMoving() {
    return (this.remainingPixels > 0) || (this.pixelBuffer > 0);
  },

  getMoveDirection() {
    // todo: calc quick/long press

    if (this.isMoving() && this.lastPressedButton) {
      return this.lastPressedButton;
    }

    let pressedButton = Controls.getPressed();
    if (pressedButton && (Controls.getPressedAxis(pressedButton) !== false)) {
      return pressedButton;
    }

    return false;
  },
});

export default Controllable;
