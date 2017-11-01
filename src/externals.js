// All dependencies that are loaded separately and non-ES6 modules compliant libraries should be exported here
'use strict';

import me from 'melonjs-ext';
import Zepto from 'zepto-ext';
import _ from 'lodash-ext';

// Better IDE code completion:
let ext = {
  /**
   * @return {(Zepto|$|window.Zepto)}
   */
  get $() {
    return window.Zepto;
  },

  /**
   * @return {(_|window._)}
   */
  get _() {
    return window._;
  },

  /**
   * @return {(me|window.me)}
   */
  get me() {
    return window.me;
  },

  /**
   * @return {(window.me.game.assets)}
   */
  get assets() {
    return window.me.game.assets;
  },
};

let Melon = ext.me, $ = Zepto, assets = ext.assets;
Melon.Events = Melon.event;

export default ext;

export {
  $,
  _,
  Melon,
  assets
}
