'use strict';
import {Melon} from 'externals';
import Screen from 'system/Screen';

/*
 * Game Loading Screen
 * Based on default one: https://raw.githubusercontent.com/melonjs/melonJS/master/src/loader/loadingscreen.js
 */

// a basic progress bar object
let ProgressBar = Melon.Renderable.extend({

  init(v, w, h) {
    this._super(Melon.Renderable, "init", [v.x, v.y, w, h]);
    // flag to know if we need to refresh the display
    this.invalidate = false;

    // default progress bar height
    this.barHeight = 4;

    // current progress
    this.progress = 0;

    this.padding = 5;
  },

  // make sure the screen is refreshed every frame
  onProgressUpdate(progress) {
    this.progress = ~~(progress * (this.width - (this.padding * 2)));
    this.invalidate = true;
  },

  // make sure the screen is refreshed every frame
  update() {
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
  draw(renderer) {
    // draw the progress bar
    //renderer.setColor("#555555");
    //renderer.fillRect(0, (this.height / 2) - (this.barHeight / 2), this.width, this.barHeight);

    renderer.setColor("#64F865");
    renderer.fillRect(this.padding, (this.height / 2) - (this.barHeight / 2), this.progress, this.barHeight);

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
  onResetEvent() {
    // background color
    Melon.game.world.addChild(new Melon.ColorLayer("background", "#000000", 0), 0);

    // progress bar
    let progressBar = new this.ProgressBar(
      new Melon.Vector2d(),
      Melon.video.renderer.getWidth(),
      Melon.video.renderer.getHeight()
    );

    this.loaderHdlr = Melon.Events.subscribe(
      Melon.Events.LOADER_PROGRESS,
      progressBar.onProgressUpdate.bind(progressBar)
    );

    this.resizeHdlr = Melon.Events.subscribe(
      Melon.Events.VIEWPORT_ONRESIZE,
      progressBar.resize.bind(progressBar)
    );

    Melon.game.world.addChild(progressBar, 1);

    // Remove Melon logo
    this.iconCanvas = Melon.video.createCanvas(Screen.width, Screen.height, false);
  },

  // destroy object at end of loading
  onDestroyEvent() {
    // cancel the callback
    Melon.Events.unsubscribe(this.loaderHdlr);
    Melon.Events.unsubscribe(this.resizeHdlr);
    this.loaderHdlr = this.resizeHdlr = null;
  }
});
