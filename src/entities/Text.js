'use strict';
import {Melon} from 'externals';

/**
 * Represents a readable piece of text.
 *
 * Name: text
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
