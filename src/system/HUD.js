import me from 'melonjs';

/**
 * a HUD container and child items
 */
let sysHUD = {
  Container: me.Container.extend({
    init: function () {
      // call the constructor
      this._super(me.Container, 'init');

      // persistent across level change
      this.isPersistent = true;

      // make sure we use screen coordinates
      this.floating = true;

      // give a name
      this.name = "HUD";

      // add our child score object at the top left corner
      this.addChild(new sysHUD.ScoreItem(5, 5));
    }
  }),

  ScoreItem: me.Renderable.extend({
    /**
     * constructor
     */
    init: function (x, y) {

      // call the parent constructor
      // (size does not matter here)
      this._super(me.Renderable, 'init', [x, y, 10, 10]);

      this.score = -1;
      this._prevScore = -1; // Needed for detecting a change on update
    },

    /**
     * update function
     */
    update: function () {
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
    draw: function (context) {
      // draw it baby !
    }
  })
};

export default sysHUD;
