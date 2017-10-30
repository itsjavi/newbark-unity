'use strict';
import Melon from 'melonjs';

/**
 * Name: shore
 * Layer: interactions
 */
export default Melon.Entity.extend({
  /**
   * constructor
   */
  init(x, y, settings) {
    this._super(Melon.Entity, 'init', [x, y, settings]);
  },
});
