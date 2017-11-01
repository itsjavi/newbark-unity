'use strict';
import {Melon} from 'externals';
import Config from 'config';

export default {
  init() {
    // Add a frame on the supported resolution
    if (this.preset === 'GBC_2x' && this.scale === 1) {
      document.body.classList.add('default-resolution');
    }

    // The game canvas will need a zoom if the devicePixelRatio is different than 1
    if (!isNaN(this.scale) && this.pixelRatio > 1) {
      document.getElementById(Config.wrapper).style.zoom = this.pixelRatio;
    }
  },

  get preset() {
    return Config.video.presets[Config.video.preset];
  },
  get defaultWidth() {
    return this.preset.width;
  },
  get defaultHeight() {
    return this.preset.height;
  },
  get width() {
    return Melon.game.viewport.width;
  },
  get height() {
    return Melon.game.viewport.height;
  },
  get scale() {
    return Config.video.scale;
  },
  get pixelRatio() {
    return window.devicePixelRatio ? window.devicePixelRatio : 1;
  }
};
