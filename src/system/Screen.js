'use strict';
import {Melon} from 'externals';
import Config from 'config';

export default {
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
  }
};
