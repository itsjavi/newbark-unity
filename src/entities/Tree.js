'use strict';
import {Melon} from 'externals';

/**
 * Represents a tree where the trainer can find Pokemon using "headbutt".
 *
 * Name: tree
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
