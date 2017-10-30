'use strict';
import Melon from 'melonjs';
import Config from 'config';

export default {
  get preset() {
    return Config.video.presets[Config.video.preset];
  },
  get defaultWidth() {
    return this.preset.width * this.preset.ratio;
  },
  get defaultHeight() {
    return this.preset.height * this.preset.ratio;
  },
  get width() {
    return Melon.game.viewport.width;
  },
  get height() {
    return Melon.game.viewport.height;
  },
  get scale() {
    return Config.video.scale;
  }
};
