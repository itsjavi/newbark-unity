/*
 * Game Loading Screen
 * Based on default one: https://raw.githubusercontent.com/melonjs/melonJS/master/src/loader/loadingscreen.js
 */
(function () {
  // a basic progress bar object
  var ProgressBar = me.Renderable.extend({

    init: function (v, w, h) {
      this._super(me.Renderable, "init", [v.x, v.y, w, h]);
      // flag to know if we need to refresh the display
      this.invalidate = false;

      // default progress bar height
      this.barHeight = 4;

      // current progress
      this.progress = 0;
    },

    // make sure the screen is refreshed every frame
    onProgressUpdate: function (progress) {
      this.progress = ~~(progress * this.width);
      this.invalidate = true;
    },

    // make sure the screen is refreshed every frame
    update: function () {
      if (this.invalidate === true) {
        // clear the flag
        this.invalidate = false;
        // and return true
        return true;
      }
      // else return false
      return false;
    },

    // draw function
    draw: function (renderer) {
      // draw the progress bar
      renderer.setColor("black");
      renderer.fillRect(0, (this.height / 2) - (this.barHeight / 2), this.width, this.barHeight);

      renderer.setColor("#0D6101");
      renderer.fillRect(2, (this.height / 2) - (this.barHeight / 2), this.progress, this.barHeight);

      renderer.setColor("white");
    }
  });

  /**
   * a default loading screen
   * @memberOf me
   * @ignore
   * @constructor
   */
  me.DefaultLoadingScreen = me.ScreenObject.extend({
    // call when the loader is resetted
    onResetEvent: function () {
      // background color
      me.game.world.addChild(new me.ColorLayer("background", "#202020", 0), 0);

      // progress bar
      var progressBar = new ProgressBar(
        new me.Vector2d(),
        me.video.renderer.getWidth(),
        me.video.renderer.getHeight()
      );

      this.loaderHdlr = me.event.subscribe(
        me.event.LOADER_PROGRESS,
        progressBar.onProgressUpdate.bind(progressBar)
      );

      this.resizeHdlr = me.event.subscribe(
        me.event.VIEWPORT_ONRESIZE,
        progressBar.resize.bind(progressBar)
      );

      me.game.world.addChild(progressBar, 1);
      this.iconCanvas = me.video.createCanvas(me.game.viewport.width, me.game.viewport.height, false);
    },

    // destroy object at end of loading
    onDestroyEvent: function () {
      // cancel the callback
      me.event.unsubscribe(this.loaderHdlr);
      me.event.unsubscribe(this.resizeHdlr);
      this.loaderHdlr = this.resizeHdlr = null;
    }
  });
})();