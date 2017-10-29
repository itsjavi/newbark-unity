'use strict';
import Melon from 'melonjs';

let ScoreItem = Melon.Renderable.extend({
  /**
   * constructor
   */
  init(x, y) {

    // call the parent constructor
    // (size does not matter here)
    this._super(Melon.Renderable, 'init', [x, y, 10, 10]);

    this.score = -1;
    this._prevScore = -1; // Needed for detecting a change on update
  },

  /**
   * update function
   */
  update() {
    // we don't do anything fancy here, so just
    // return true if the score variable has been updated
    if (this.score !== this._prevScore) {
      this._prevScore = this.score;
      return true;
    }
    return false;
  },

  /**
   * draw the score
   */
  draw(context) {
    // draw it baby !
  }
});

/**
 * The main game Head-Up Display (HUD) container with child items
 */
export default Melon.Container.extend({
  ScoreItem: ScoreItem,
  init() {
    // call the constructor
    this._super(Melon.Container, 'init');

    // persistent across level change
    this.isPersistent = true;

    // make sure we use screen coordinates
    this.floating = true;

    // give a name
    this.name = "HUD";

    // add our child score object at the top left corner
    this.addChild(new this.ScoreItem(5, 5));
  }
});
