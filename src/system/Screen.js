"use strict";
import Melon from 'melonjs';
import Config from 'config';

export default {
  get width() {
    return Config.video.size_presets[Config.video.size].width * Config.video.size_zoom;
  },
  get height() {
    return Config.video.size_presets[Config.video.size].height * Config.video.size_zoom;
  },
  get currentWidth() {
    return Melon.game.viewport.width;
  },
  get currentHeight() {
    return Melon.game.viewport.height;
  },
  get scale() {
    return Config.video.size_fit === true ? "auto" : (this._isHighDensityDisplay() ? 2.0 : 1.0);
  },
  _isHighDensityDisplay() {
    return ((window.matchMedia
      && (window.matchMedia('only screen and (min-resolution: 192dpi), ' +
        'only screen and (min-resolution: 2dppx), ' +
        'only screen and (min-resolution: 75.6dpcm)').matches
        || window.matchMedia('only screen and (-webkit-min-device-pixel-ratio: 2), ' +
          'only screen and (-o-min-device-pixel-ratio: 2/1), ' +
          'only screen and (min--moz-device-pixel-ratio: 2), ' +
          'only screen and (min-device-pixel-ratio: 2)').matches))
      || (window.devicePixelRatio && window.devicePixelRatio >= 2));
  },
};
