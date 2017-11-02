'use strict';
import {Melon, $, _, config} from 'externals';
import Events from 'system/Events';
import Movement from 'system/physics/Movement';

export default {
  get enabled() {
    return (config.debug === true);
  },

  disable() {
    config.debug = false;
  },

  enable() {
    config.debug = true;
  },

  /**
   * @returns {Zepto|HTMLElement}
   */
  get element() {
    return $('#debug');
  },

  init() {
    // Set debug to false (even if true) if the location does not have #debug
    config.debug = (
      config.debug === true
      && !_.isEmpty(window.location.hash)
      && !!(window.location.hash.toLowerCase().match(/debug/ig))
    );

    if (this.enabled) {
      // Melon.debug.renderHitBox = true; // TODO: check the utility of this
      this.debugUpdate('-', '-', 0, 0);
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

  debugUpdate(direction, deltaTime, remainingPixels, deltaVelocity = null) {
    if (!this.enabled) {
      return;
    }

    this.debug({
      'FPS': Movement.fps,
      'Delta Time': deltaTime,
      'Velocity (pixels per frame)': Movement.velocity,
      'Distance (pixels per move)': Movement.distancePerMove,
      'Distance (current move)': remainingPixels,
      'Velocity (current move)': !isNaN(deltaVelocity) ? deltaVelocity : 0,
      'Last Collision': '-',
      'Direction': direction
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
