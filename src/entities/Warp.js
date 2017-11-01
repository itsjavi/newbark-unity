'use strict';
import {Melon} from 'externals';

/**
 * Warps are a way to travel between maps
 *
 * Name: warp
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
