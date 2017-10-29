"use strict";
import Melon from 'melonjs';
import Screen from 'system/Screen';

/*
 * Game Loading Screen
 * Based on default one: https://raw.githubusercontent.com/melonjs/melonJS/master/src/loader/loadingscreen.js
 */

// a basic progress bar object
let ProgressBar = Melon.Renderable.extend({

  init: function (v, w, h) {
    this._super(Melon.Renderable, "init", [v.x, v.y, w, h]);
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
    renderer.setColor("#CBF8D8");
    renderer.fillRect(0, (this.height / 2) - (this.barHeight / 2), this.width, this.barHeight);

    renderer.setColor("#808080");
    renderer.fillRect(0, (this.height / 2) - (this.barHeight / 2), this.progress, this.barHeight);

    renderer.setColor("white");
  }
});

/**
 * a default loading screen
 * @memberOf me
 * @ignore
 * @constructor
 */
export default Melon.ScreenObject.extend({
  ProgressBar: ProgressBar,
  // call when the loader is resetted
  onResetEvent: function () {
    // background color
    Melon.game.world.addChild(new Melon.ColorLayer("background", "#CBF8D8", 0), 0);

    // progress bar
    let progressBar = new this.ProgressBar(
      new Melon.Vector2d(),
      Melon.video.renderer.getWidth(),
      Melon.video.renderer.getHeight()
    );

    this.loaderHdlr = Melon.event.subscribe(
      Melon.event.LOADER_PROGRESS,
      progressBar.onProgressUpdate.bind(progressBar)
    );

    this.resizeHdlr = Melon.event.subscribe(
      Melon.event.VIEWPORT_ONRESIZE,
      progressBar.resize.bind(progressBar)
    );

    Melon.game.world.addChild(progressBar, 1);

    // Remove Melon logo
    this.iconCanvas = Melon.video.createCanvas(Screen.currentWidth, Screen.currentHeight, false);
  },

  // destroy object at end of loading
  onDestroyEvent: function () {
    // cancel the callback
    Melon.event.unsubscribe(this.loaderHdlr);
    Melon.event.unsubscribe(this.resizeHdlr);
    this.loaderHdlr = this.resizeHdlr = null;
  }
});
