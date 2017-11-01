'use strict';
import {Melon, config} from 'externals';

export default {
  init() {
    // Add a frame on the supported resolution
    if (config.video.resolution === 'GBC_2x' && this.scale === 1) {
      document.body.classList.add('default-resolution');
    }

    // The game canvas will need a zoom if the devicePixelRatio is different than 1
    if (!isNaN(this.scale) && this.pixelRatio > 1) {
      document.getElementById(config.wrapper).style.zoom = this.pixelRatio;
    }
  },

  get preset() {
    return config.video.resolutions[config.video.resolution];
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
    return config.video.scale;
  },
  get pixelRatio() {
    return window.devicePixelRatio ? window.devicePixelRatio : 1;
  }
};
