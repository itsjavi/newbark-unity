'use strict';
import {Melon, $, _} from 'externals';
import Config from 'config';
import Events from 'system/Events';
import Movement from 'system/Movement';

export default {
  get enabled() {
    return (Config.debug === true);
  },

  disable() {
    Config.debug = false;
  },

  enable() {
    Config.debug = true;
  },

  /**
   * @returns {Zepto|HTMLElement}
   */
  get element() {
    return $('#debug');
  },

  init() {
    // Set debug to false (even if true) if the location does not have #debug
    Config.debug = (
      Config.debug === true
      && !_.isEmpty(window.location.hash)
      && !!(window.location.hash.toLowerCase().match(/debug/ig))
    );

    if (this.enabled) {
      // Make it available so it can be tweaked on run time
      Melon.game.config = Config;
      // Melon.debug.renderHitBox = true; // TODO: check the utility of this
    }

    // Show/hide debug elements on level load, depending on current debug mode
    Events.subscribe(Events.LEVEL_LOADED, () => {
      if (this.enabled) {
        this.show();
      } else {
        this.hide();
      }
    });
  },

  hide() {
    this.element.hide();
    Melon.game.world.children.forEach(function (child, i) {
      if (child.name.match(/debug/ig)) {
        // Hide debug-only elements
        Melon.game.world.children[i].alpha = 0;
      }
    });
  },

  show() {
    this.element.show();
    Melon.game.world.children.forEach(function (child, i) {
      if (child.name.match(/debug/ig)) {
        // Hide debug-only elements
        Melon.game.world.children[i].alpha = 1;
      }
    });
  },

  /**
   * @param {Object} data key-pair values
   */
  format(data) {
    let html = '<table class="debug-table">\n';

    _.forOwn(data, function (value, key) {
      let id = 'debug_' + key.replace(/\s/g, '_').toLowerCase();
      html += `<tr><td><b>${key}:</b></td><td id="${id}">${value}</td></tr>` + '\n';
    });

    return html + '</table>';
  },

  /**
   * @param {(Object|string)} data key-pair values or string
   */
  debug(data) {
    if (!this.enabled) {
      return;
    }

    if (_.isString(data)) {
      data = {data: data};
    }

    this.element.html(this.format(data));
  },

  debugUpdate(direction, deltaTime, remainingPixels, pixelBuffer) {
    if (!this.enabled) {
      return;
    }

    this.debug({
      'FPS': Movement.fps,
      'Delta Time': deltaTime,
      'Direction': direction,
      'Pixels per move': Movement.pixelsPerMove,
      'Pixels per frame': Movement.pixelsPerFrame,
      'Remaining Pixels': remainingPixels,
      'Pixel Buffer': pixelBuffer,
      'Last Collision': '-',
    });
  },

  debugCollision(collisionResponse, elementSelector = '#debug_last_collision') {
    if (!this.enabled) {
      return;
    }
    let el = $(elementSelector);
    if (!el.length) {
      return;
    }
    let name = collisionResponse.b.name || 'default';
    el.html(`${name}`);
  }
};
