game.utils = {
  "isHighDensityDisplay": function () {
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

  "getBestScreenScale": function () {
    return this.isHighDensityDisplay() ? 2.0 : 1.0;
  },

  "getScreenScale": function () {
    return game.config.screen.scale == "auto" ? "auto" :
      (game.config.screen.scale == "best" ? this.getBestScreenScale() : game.config.screen.scale);
  },
  "nearestMultiple": function (num, multiple) {
    if (num > 0)
      return Math.ceil(n / multiple) * multiple;
    else if (num < 0)
      return Math.floor(n / multiple) * multiple;
    else
      return multiple;
  }
};