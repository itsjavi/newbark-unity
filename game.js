/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 7);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
// All dependencies that are loaded separately and non-ES6 modules compliant libraries should be exported here


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.config = exports.assets = exports.Melon = exports._ = exports.$ = undefined;

var _melonjsExt = __webpack_require__(9);

var _melonjsExt2 = _interopRequireDefault(_melonjsExt);

var _zeptoExt = __webpack_require__(10);

var _zeptoExt2 = _interopRequireDefault(_zeptoExt);

var _lodashExt = __webpack_require__(11);

var _lodashExt2 = _interopRequireDefault(_lodashExt);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

// Better IDE code completion:
var ext = {
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

  /**
   * @return {(window.me.$config)}
   */
  get config() {
    return window.me.$config;
  }
};

var Melon = ext.me,
    $ = _zeptoExt2.default,
    assets = ext.assets,
    config = ext.config;
Melon.Events = Melon.event;

exports.default = ext;
exports.$ = $;
exports._ = _lodashExt2.default;
exports.Melon = Melon;
exports.assets = assets;
exports.config = config;

/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

var Input = _externals.Melon.input,
    KEYS = Input.KEY,
    PAD_AXES = Input.GAMEPAD.AXES,
    PAD_BUTTONS = Input.GAMEPAD.BUTTONS;

exports.default = {
  UP: "UP",
  RIGHT: "RIGHT",
  DOWN: "DOWN",
  LEFT: "LEFT",
  L1: "L1",
  R1: "R1",
  X: "X",
  Y: "Y",
  A: "A",
  B: "B",
  SELECT: "SELECT",
  START: "START",

  bind: function bind() {
    // enable the keyboard
    Input.bindKey(KEYS.LEFT, this.LEFT, false, true);
    Input.bindKey(KEYS.RIGHT, this.RIGHT, false, true);
    Input.bindKey(KEYS.UP, this.UP, false, true);
    Input.bindKey(KEYS.DOWN, this.DOWN, false, true);

    Input.bindKey(KEYS.Q, this.L1);
    Input.bindKey(KEYS.W, this.R1);
    Input.bindKey(KEYS.A, this.SELECT);
    Input.bindKey(KEYS.S, this.START);
    Input.bindKey(KEYS.X, this.X);
    Input.bindKey(KEYS.Z, this.Y);
    Input.bindKey(KEYS.SPACE, this.A);
    Input.bindKey(KEYS.B, this.B);

    // enable the first gamepad
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.LX, threshold: -0.5 }, KEYS.LEFT);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.LX, threshold: 0.5 }, KEYS.RIGHT);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.LY, threshold: -0.5 }, KEYS.UP);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.LY, threshold: 0.5 }, KEYS.DOWN);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.RX, threshold: -0.5 }, KEYS.LEFT);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.RX, threshold: 0.5 }, KEYS.RIGHT);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.RY, threshold: -0.5 }, KEYS.UP);
    Input.bindGamepad(0, { type: "axes", code: PAD_AXES.RY, threshold: 0.5 }, KEYS.DOWN);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.LEFT }, KEYS.LEFT);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.RIGHT }, KEYS.RIGHT);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.UP }, KEYS.UP);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.DOWN }, KEYS.DOWN);

    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.L1 }, KEYS.Q);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.R1 }, KEYS.W);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.SELECT }, KEYS.A);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.START }, KEYS.S);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.FACE_3 }, KEYS.X);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.FACE_4 }, KEYS.Z);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.FACE_1 }, KEYS.SPACE);
    Input.bindGamepad(0, { type: "buttons", code: PAD_BUTTONS.FACE_2 }, KEYS.B);

    this.bindTouchEvent(_externals.Melon.$game.element, 'swipeUp', KEYS.UP);
    this.bindTouchEvent(_externals.Melon.$game.element, 'swipeRight', KEYS.RIGHT);
    this.bindTouchEvent(_externals.Melon.$game.element, 'swipeDown', KEYS.DOWN);
    this.bindTouchEvent(_externals.Melon.$game.element, 'swipeLeft', KEYS.LEFT);
    this.bindTouchEvent(_externals.Melon.$game.element, 'swipeLeft', KEYS.LEFT);

    this.bindTouchEvent(_externals.Melon.$game.element, 'tap', KEYS.SPACE); // A
    this.bindTouchEvent(_externals.Melon.$game.element, 'longTap', KEYS.S); // START
  },
  bindTouchEvent: function bindTouchEvent(el, eventName, key) {
    var timeoutTime = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : 500;

    var eventTimeout = null;

    el.on(eventName, function () {
      Input.triggerKeyEvent(key, true); // keydown
      if (eventTimeout) {
        clearTimeout(eventTimeout); // cancel prev timeout
      }
      eventTimeout = setTimeout(function () {
        Input.triggerKeyEvent(key, false); // keyup after 500ms
      }, timeoutTime);
    });
  },
  isPressed: function isPressed(buttonName) {
    return Input.isKeyPressed(buttonName);
  },
  isUpPressed: function isUpPressed() {
    return this.isPressed(this.UP);
  },
  isDownPressed: function isDownPressed() {
    return this.isPressed(this.DOWN);
  },
  isLeftPressed: function isLeftPressed() {
    return this.isPressed(this.LEFT);
  },
  isRightPressed: function isRightPressed() {
    return this.isPressed(this.RIGHT);
  },
  isAPressed: function isAPressed() {
    return this.isPressed(this.A);
  },
  isBPressed: function isBPressed() {
    return this.isPressed(this.B);
  },
  isXPressed: function isXPressed() {
    return this.isPressed(this.X);
  },
  isYPressed: function isYPressed() {
    return this.isPressed(this.Y);
  },
  isSelectPressed: function isSelectPressed() {
    return this.isPressed(this.SELECT);
  },
  isStartPressed: function isStartPressed() {
    return this.isPressed(this.START);
  },
  isL1Pressed: function isL1Pressed() {
    return this.isPressed(this.L1);
  },
  isR1Pressed: function isR1Pressed() {
    return this.isPressed(this.R1);
  },
  getPressed: function getPressed() {
    if (this.isLeftPressed()) {
      return this.LEFT;
    }

    if (this.isRightPressed()) {
      return this.RIGHT;
    }

    if (this.isUpPressed()) {
      return this.UP;
    }

    if (this.isDownPressed()) {
      return this.DOWN;
    }

    if (this.isAPressed()) {
      return this.A;
    }

    if (this.isBPressed()) {
      return this.B;
    }

    if (this.isXPressed()) {
      return this.X;
    }

    if (this.isYPressed()) {
      return this.Y;
    }

    if (this.isSelectPressed()) {
      return this.SELECT;
    }

    if (this.isStartPressed()) {
      return this.START;
    }

    if (this.isL1Pressed()) {
      return this.L1;
    }

    if (this.isR1Pressed()) {
      return this.R1;
    }

    return false;
  },
  isDirectionButtonPressed: function isDirectionButtonPressed() {
    var button = this.getPressed();
    return button === this.LEFT || button === this.RIGHT || button === this.UP || button === this.DOWN;
  },
  getPressedAxis: function getPressedAxis(button) {
    button = button ? button : this.getPressed();

    if (button === this.LEFT || button === this.RIGHT) {
      return "x";
    } else if (button === this.UP || button === this.DOWN) {
      return "y";
    } else {
      return false;
    }
  },
  getPressedOppositeAxis: function getPressedOppositeAxis(pressedAxis) {
    if (pressedAxis === false) {
      return false;
    }
    return pressedAxis === "x" ? "y" : "x";
  }
};

/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

exports.default = {
  channels: {
    BGM: false,
    SFX_1: false,
    SFX_2: false
  },

  /**
   * @param {String} name
   * @param {Number} [volume]
   * @return {(Number|boolean)} the sound instance ID or false if could not play it.
   */
  playMusic: function playMusic(name, volume) {
    if (!_externals.config.sound.enabled) {
      return false;
    }
    this.channels.BGM = name;

    _externals.Melon.audio.playTrack(name, !isNaN(volume) ? volume : _externals.config.sound.volume.music / 100);
  },


  /**
   * @param {String} name
   * @param {Number} [volume]
   * @param {String} [channel] Sound channel name.
   * @return {(Number|boolean)} the sound instance ID or false if could not play it.
   */
  playEffect: function playEffect(name, volume, channel) {
    var _this = this;

    if (!_externals.config.sound.enabled) {
      return false;
    }
    channel = channel || "SFX_1";

    if (this.channels[channel] !== false) {
      // Only allow one SFX at a time per channel, to avoid overlapping
      return false;
    }
    this.channels[channel] = name;
    _externals.Melon.audio.play(name, false, function () {
      _this.channels[channel] = false;
    }, !isNaN(volume) ? volume : _externals.config.sound.volume.effects / 100);
  }
};

/***/ }),
/* 3 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _externals = __webpack_require__(0);

var _Calc = __webpack_require__(15);

var _Calc2 = _interopRequireDefault(_Calc);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

// TODO: convert class members into non-static and have pixel calculation functions and buffers inside.

/**
 * Snap to grid movement style helper class
 */
var Movement = function () {
  function Movement() {
    _classCallCheck(this, Movement);
  }

  _createClass(Movement, null, [{
    key: 'fps',

    /**
     * @return {Number}
     */
    get: function get() {
      return _externals.Melon.sys.fps;
    }

    /**
     * Constant number of pixels to move every time a direction button is pressed.
     * @return {Number}
     */

  }, {
    key: 'distancePerMove',
    get: function get() {
      if (_externals.config.player.distance_per_move === undefined) {
        _externals.config.player.distance_per_move = _externals.config.video.tile_size;
      }
      return _externals.config.player.distance_per_move;
    }

    /**
     * Constant velocity. Number of pixels to move per frame.
     * @return {Number}
     */

  }, {
    key: 'velocity',
    get: function get() {
      if (_externals.config.player.velocity === undefined) {
        var vel = _Calc2.default.velocity(_externals.config.video.tile_size, this.fps);

        vel = (vel <= 0.5 ? 1 : Math.round(vel)) * _externals.config.player.speed; // assure minimum of 1 and multiply by speed.

        _externals.config.player.velocity = Math.ceil(vel / 2) * 2; // assure velocity is always multiple of 2
      }
      return _externals.config.player.velocity;
    }
  }]);

  return Movement;
}();

exports.default = Movement;

/***/ }),
/* 4 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

var _Events = __webpack_require__(16);

var _Events2 = _interopRequireDefault(_Events);

var _Movement = __webpack_require__(3);

var _Movement2 = _interopRequireDefault(_Movement);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
  get enabled() {
    return _externals.config.debug === true;
  },

  disable: function disable() {
    _externals.config.debug = false;
  },
  enable: function enable() {
    _externals.config.debug = true;
  },


  /**
   * @returns {Zepto|HTMLElement}
   */
  get element() {
    return (0, _externals.$)('#debug');
  },

  init: function init() {
    var _this = this;

    // Set debug to false (even if true) if the location does not have #debug
    _externals.config.debug = _externals.config.debug === true && !_externals._.isEmpty(window.location.hash) && !!window.location.hash.toLowerCase().match(/debug/ig);

    if (this.enabled) {
      // Melon.debug.renderHitBox = true; // TODO: check the utility of this
      this.debugUpdate('-', '-', 0, 0);
    }

    // Show/hide debug elements on level load, depending on current debug mode
    _Events2.default.subscribe(_Events2.default.LEVEL_LOADED, function () {
      if (_this.enabled) {
        _this.show();
      } else {
        _this.hide();
      }
    });
  },
  hide: function hide() {
    this.element.hide();
    _externals.Melon.game.world.children.forEach(function (child, i) {
      if (child.name.match(/debug/ig)) {
        // Hide debug-only elements
        _externals.Melon.game.world.children[i].alpha = 0;
      }
    });
  },
  show: function show() {
    this.element.show();
    _externals.Melon.game.world.children.forEach(function (child, i) {
      if (child.name.match(/debug/ig)) {
        // Hide debug-only elements
        _externals.Melon.game.world.children[i].alpha = 1;
      }
    });
  },


  /**
   * @param {Object} data key-pair values
   */
  format: function format(data) {
    var html = '<table class="debug-table">\n';

    _externals._.forOwn(data, function (value, key) {
      var id = 'debug_' + key.replace(/\s/g, '_').toLowerCase();
      html += '<tr><td><b>' + key + ':</b></td><td id="' + id + '">' + value + '</td></tr>' + '\n';
    });

    return html + '</table>';
  },


  /**
   * @param {(Object|string)} data key-pair values or string
   */
  debug: function debug(data) {
    if (!this.enabled) {
      return;
    }

    if (_externals._.isString(data)) {
      data = { data: data };
    }

    this.element.html(this.format(data));
  },
  debugUpdate: function debugUpdate(direction, deltaTime, remainingPixels) {
    var deltaVelocity = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : null;

    if (!this.enabled) {
      return;
    }

    this.debug({
      'FPS': _Movement2.default.fps,
      'Delta Time': deltaTime,
      'Velocity (pixels per frame)': _Movement2.default.velocity,
      'Distance (pixels per move)': _Movement2.default.distancePerMove,
      'Distance (current move)': remainingPixels,
      'Velocity (current move)': !isNaN(deltaVelocity) ? deltaVelocity : 0,
      'Last Collision': '-',
      'Direction': direction
    });
  },
  debugCollision: function debugCollision(collisionResponse) {
    var elementSelector = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : '#debug_last_collision';

    if (!this.enabled) {
      return;
    }
    var el = (0, _externals.$)(elementSelector);
    if (!el.length) {
      return;
    }
    var name = collisionResponse.b.name || 'default';
    el.html('' + name);
  }
};

/***/ }),
/* 5 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

var _Resolution = __webpack_require__(6);

var _Resolution2 = _interopRequireDefault(_Resolution);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

/*
 * Game loading screen
 * Based on default one: https://raw.githubusercontent.com/melonjs/melonJS/master/src/loader/loadingscreen.js
 */

// a basic progress bar object
var ProgressBar = _externals.Melon.Renderable.extend({
  init: function init(v, w, h) {
    this._super(_externals.Melon.Renderable, "init", [v.x, v.y, w, h]);
    // flag to know if we need to refresh the display
    this.invalidate = false;

    // default progress bar height
    this.barHeight = 4;

    // current progress
    this.progress = 0;

    this.padding = 5;
  },


  // make sure the screen is refreshed every frame
  onProgressUpdate: function onProgressUpdate(progress) {
    this.progress = ~~(progress * (this.width - this.padding * 2));
    this.invalidate = true;
  },


  // make sure the screen is refreshed every frame
  update: function update() {
    if (this.invalidate === true) {
      // clear the flag
      this.invalidate = false;
      // and return true
      return true;
    }
    // else return false
    return false;
  },


  // draw function
  draw: function draw(renderer) {
    // draw the progress bar
    //renderer.setColor("#555555");
    //renderer.fillRect(0, (this.height / 2) - (this.barHeight / 2), this.width, this.barHeight);

    renderer.setColor("#64F865");
    renderer.fillRect(this.padding, this.height / 2 - this.barHeight / 2, this.progress, this.barHeight);

    renderer.setColor("white");
  }
});

/**
 * a default loading screen
 * @memberOf me
 * @ignore
 * @constructor
 */
exports.default = _externals.Melon.ScreenObject.extend({
  ProgressBar: ProgressBar,
  // call when the loader is resetted
  onResetEvent: function onResetEvent() {
    // background color
    _externals.Melon.game.world.addChild(new _externals.Melon.ColorLayer("background", "#000000", 0), 0);

    // progress bar
    var progressBar = new this.ProgressBar(new _externals.Melon.Vector2d(), _externals.Melon.video.renderer.getWidth(), _externals.Melon.video.renderer.getHeight());

    this.loaderHdlr = _externals.Melon.Events.subscribe(_externals.Melon.Events.LOADER_PROGRESS, progressBar.onProgressUpdate.bind(progressBar));

    this.resizeHdlr = _externals.Melon.Events.subscribe(_externals.Melon.Events.VIEWPORT_ONRESIZE, progressBar.resize.bind(progressBar));

    _externals.Melon.game.world.addChild(progressBar, 1);

    // Remove Melon logo
    this.iconCanvas = _externals.Melon.video.createCanvas(_Resolution2.default.width, _Resolution2.default.height, false);
  },


  // destroy object at end of loading
  onDestroyEvent: function onDestroyEvent() {
    // cancel the callback
    _externals.Melon.Events.unsubscribe(this.loaderHdlr);
    _externals.Melon.Events.unsubscribe(this.resizeHdlr);
    this.loaderHdlr = this.resizeHdlr = null;
  }
});

/***/ }),
/* 6 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

exports.default = {
  init: function init() {
    // Add a frame on the supported resolution
    if (_externals.config.video.resolution === 'GBC_2x' && this.scale === 1) {
      document.body.classList.add('default-resolution');
    }

    // The game canvas will need a zoom if the devicePixelRatio is different than 1
    if (!isNaN(this.scale) && this.pixelRatio > 1) {
      document.getElementById(_externals.config.wrapper).style.zoom = this.pixelRatio;
    }
  },


  get preset() {
    return _externals.config.video.resolutions[_externals.config.video.resolution];
  },
  get defaultWidth() {
    return this.preset.width;
  },
  get defaultHeight() {
    return this.preset.height;
  },
  get width() {
    return _externals.Melon.game.viewport.width;
  },
  get height() {
    return _externals.Melon.game.viewport.height;
  },
  get scale() {
    return _externals.config.video.scale;
  },
  get pixelRatio() {
    return window.devicePixelRatio ? window.devicePixelRatio : 1;
  }
};

/***/ }),
/* 7 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var _Game = __webpack_require__(8);

var _Game2 = _interopRequireDefault(_Game);

var _GameLoader = __webpack_require__(24);

var _GameLoader2 = _interopRequireDefault(_GameLoader);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var gameLoader = new _GameLoader2.default(_Game2.default);
gameLoader.loadOnReady();

/***/ }),
/* 8 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _externals = __webpack_require__(0);

var _all = __webpack_require__(12);

var _all2 = _interopRequireDefault(_all);

var _all3 = __webpack_require__(21);

var _all4 = _interopRequireDefault(_all3);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var Game = function () {
  function Game(config) {
    _classCallCheck(this, Game);

    /**
     * @type {window.me.$game.config}
     */
    this.initialConfig = _externals._.cloneDeep(config);

    /**
     * @type {window.me.$game.config}
     */
    this.config = config;

    /**
     * @type {(Zepto|HTMLElement)}
     */
    this.element = (0, _externals.$)('#' + this.config.wrapper);

    // Register all entities
    _externals._.forOwn(_all2.default, function (entity, entityName) {
      // Add all the entities to the entity pool
      // For every tile type, we should have a registered entity
      _externals.Melon.pool.register(entityName.toLowerCase(), entity);
    });
  }

  _createClass(Game, [{
    key: 'start',
    value: function start() {
      // Sets the scenes for the different states
      _externals.Melon.state.set(_externals.Melon.state.READY, new _all4.default.TitleScreen());
      _externals.Melon.state.set(_externals.Melon.state.PLAY, new _all4.default[this.config.initial_scene]());

      // Start the gaMelon.
      _externals.Melon.state.change(_externals.Melon.state.PLAY);
    }
  }]);

  return Game;
}();

exports.default = Game;

/***/ }),
/* 9 */
/***/ (function(module, exports) {

module.exports = window.me;

/***/ }),
/* 10 */
/***/ (function(module, exports) {

module.exports = window.Zepto;

/***/ }),
/* 11 */
/***/ (function(module, exports) {

module.exports = window._;

/***/ }),
/* 12 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* This is an auto-generated file. Please use gulp to update it. */


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.Warp = exports.Tree = exports.Text = exports.Shore = exports.Player = undefined;

var _Player = __webpack_require__(13);

var _Player2 = _interopRequireDefault(_Player);

var _Shore = __webpack_require__(17);

var _Shore2 = _interopRequireDefault(_Shore);

var _Text = __webpack_require__(18);

var _Text2 = _interopRequireDefault(_Text);

var _Tree = __webpack_require__(19);

var _Tree2 = _interopRequireDefault(_Tree);

var _Warp = __webpack_require__(20);

var _Warp2 = _interopRequireDefault(_Warp);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
  Player: _Player2.default,
  Shore: _Shore2.default,
  Text: _Text2.default,
  Tree: _Tree2.default,
  Warp: _Warp2.default
};
exports.Player = _Player2.default;
exports.Shore = _Shore2.default;
exports.Text = _Text2.default;
exports.Tree = _Tree2.default;
exports.Warp = _Warp2.default;

/***/ }),
/* 13 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _PlayerController = __webpack_require__(14);

var _PlayerController2 = _interopRequireDefault(_PlayerController);

var _externals = __webpack_require__(0);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

/**
 * Main player entity
 *
 * Name: player
 * Layer: characters
 */
exports.default = _PlayerController2.default.extend({
  initProperties: function initProperties() {
    this._super(_PlayerController2.default, 'initProperties');

    this.defaultSettings.anchorPoint = new _externals.Melon.Vector2d(
    // set the anchor point to X center and Y 8px (0.25 // 8px = 25% of 32)
    0.5, 8 / 32);

    /**
     * @see me.collision.types.PLAYER_OBJECT
     * @type {string}
     */
    this.defaultSettings.collisionType = 'PLAYER_OBJECT';

    // Debug frame
    this.animations.debug.frames = [[0, 1000], [11, 1000]];

    // Walking frames
    this.animations.walk_up.frames = [9, 8, 10, 8];
    this.animations.walk_right.frames = [2, 1];
    this.animations.walk_down.frames = [6, 5, 7, 5];
    this.animations.walk_left.frames = [4, 3];

    // Stand frames
    this.animations.stand_up.frames = [8];
    this.animations.stand_right.frames = [1];
    this.animations.stand_down.frames = [5];
    this.animations.stand_left.frames = [3];

    _externals.Melon.$game.player = this;
  }
});

/***/ }),
/* 14 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

var _Controls = __webpack_require__(1);

var _Controls2 = _interopRequireDefault(_Controls);

var _Sound = __webpack_require__(2);

var _Sound2 = _interopRequireDefault(_Sound);

var _Movement = __webpack_require__(3);

var _Movement2 = _interopRequireDefault(_Movement);

var _Debug = __webpack_require__(4);

var _Debug2 = _interopRequireDefault(_Debug);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var createAnimation = function createAnimation(frames) {
  if (frames.length === 1) {
    // no delay needed
    return frames;
  }

  var animation = [];
  frames.forEach(function (frame) {
    if (frame.name) {
      // e.g. {name:7, delay: 100}
      animation.push(frame);
    } else if (_externals._.isArray(frame) && frame.length >= 2) {
      // e.g. [7, 100]
      animation.push({
        name: frame[0],
        delay: frame[1]
      });
    } else {
      // e.g. 7
      animation.push({
        name: frame,
        delay: _Movement2.default.fps + _Movement2.default.fps / _Movement2.default.velocity
      });
    }
  });
  return animation;
};

var PlayerController = _externals.Melon.Entity.extend({
  initProperties: function initProperties() {
    this.defaultSettings = {
      anchorPoint: new _externals.Melon.Vector2d(0, 0)
    };
    this.remainingPixels = 0;
    this.lastPressedButton = null;

    // Define an animation per desired button e.g. animationType_buttonName
    this.animations = {
      debug: { frames: [0] },
      walk_up: { frames: [] },
      walk_right: { frames: [] },
      walk_down: { frames: [] },
      walk_left: { frames: [] },
      stand_up: { frames: [] },
      stand_right: { frames: [] },
      stand_down: { frames: [] },
      stand_left: { frames: [] }
    };

    this.initialAnimation = 'stand_down';
  },
  init: function init(x, y) {
    var _this = this;

    var settings = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : {};

    this.initProperties();

    settings = _externals._.extend(this.defaultSettings, settings);

    this._super(_externals.Melon.Entity, 'init', [x, y, settings]);

    // Register sprite animations
    _externals._.forOwn(this.animations, function (animation, animationName) {
      if (animation.frames.length === 0) {
        return;
      }

      _this.renderable.addAnimation(animationName, createAnimation(animation.frames));
    });

    // set a standing animation as default
    this.renderable.setCurrentAnimation(this.initialAnimation);

    // set the default horizontal & vertical speed (accel vector)
    this.body.vel.set(0, 0);
    this.body.maxVel.set(_Movement2.default.velocity, _Movement2.default.velocity);
    this.body.accel.set(0, 0);
    this.body.friction.set(0, 0);

    //  do not fall
    this.body.gravity = 0;
    this.body.jumping = false;
    this.body.falling = false;

    // Set the sprite anchor point
    this.anchorPoint.set(settings.anchorPoint.x, settings.anchorPoint.y);

    // ensure the player is updated even when outside of the viewport
    this.alwaysUpdate = true;

    // set the display to follow our position on both axis
    _externals.Melon.game.viewport.follow(this.pos, _externals.Melon.game.viewport.AXIS.BOTH);
  },


  /**
   * update the entity
   */
  update: function update(deltaTime) {
    var _this2 = this;

    if (!this.isMoving()) {
      this.body.vel.x = 0;
      this.body.vel.y = 0;

      if (this.lastPressedButton) {
        this.renderable.setCurrentAnimation('stand_' + this.lastPressedButton.toLowerCase());
        if (_Debug2.default.enabled) {
          if (this.debugTimeout) {
            clearTimeout(this.debugTimeout);
          }
          this.debugTimeout = setTimeout(function () {
            _this2.renderable.setCurrentAnimation('debug');
          }, 1000);
        }
      }

      this.lastPressedButton = null;
    }

    var direction = this.getMoveDirection();

    if (!direction && !this.isMoving()) {
      // No action and no pending animation
      _Debug2.default.debugUpdate('-', deltaTime, 0, 0);
      return this._super(_externals.Melon.Entity, 'update', [deltaTime]);
    }

    if (direction && !this.lastPressedButton) {
      // Assign last button
      this.lastPressedButton = direction;
    }

    if (direction && !this.isMoving()) {
      // New move
      this.remainingPixels = _Movement2.default.distancePerMove;
    }

    var velocity = 0;

    if (direction && (velocity = this.getVelocity()) !== 0) {
      // velocity = velocity * deltaTime;
      this.move(direction, velocity);
    }

    _Debug2.default.debugUpdate(direction, deltaTime, this.remainingPixels, velocity);

    // ---------------------------------------------------------------------------------------------------------
    // apply physics to the body (this moves the entity)
    this.body.update(deltaTime);

    // handle collisions against other shapes
    _externals.Melon.collision.check(this);

    // return true if we moved or if the renderable was updated
    return this._super(_externals.Melon.Entity, 'update', [deltaTime]) || this.hasVelocity();
  },
  move: function move(pressedButton, velocity) {
    if (!pressedButton || isNaN(velocity)) {
      return false;
    }

    var axis = _Controls2.default.getPressedAxis(pressedButton);

    if (!axis) {
      return false;
    }

    var oppositeAxis = _Controls2.default.getPressedOppositeAxis(axis);

    // Disable diagonal movement
    this.body.vel[oppositeAxis] = 0;

    if (pressedButton === _Controls2.default.LEFT || pressedButton === _Controls2.default.UP) {
      this.body.vel[axis] -= velocity;
    } else {
      this.body.vel[axis] += velocity;
    }

    // change to the walking animation
    if (!this.renderable.isCurrentAnimation('walk_' + pressedButton.toLowerCase())) {
      this.renderable.setCurrentAnimation('walk_' + pressedButton.toLowerCase());
    }

    return this.hasVelocity();
  },


  /**
   * @returns {(Integer|boolean)}
   */
  getVelocity: function getVelocity() {
    if (this.remainingPixels >= _Movement2.default.velocity) {
      // Move with constant velocity
      this.remainingPixels -= _Movement2.default.velocity;
      return _Movement2.default.velocity;
    } else if (this.remainingPixels !== 0 && this.remainingPixels < _Movement2.default.velocity) {
      // Move with remaining pixels
      var vel = this.remainingPixels;
      this.remainingPixels = 0;
      return vel;
    }

    // Do not move
    return 0;
  },
  isMoving: function isMoving() {
    return this.remainingPixels > 0;
  },
  hasVelocity: function hasVelocity() {
    return this.body.vel.x !== 0 || this.body.vel.y !== 0;
  },
  getMoveDirection: function getMoveDirection() {
    // todo: calc quick/long press

    if (this.isMoving() && this.lastPressedButton) {
      return this.lastPressedButton;
    }

    var pressedButton = _Controls2.default.getPressed();
    if (pressedButton && _Controls2.default.getPressedAxis(pressedButton) !== false) {
      return pressedButton;
    }

    return false;
  },


  /**
   *
   * Collision handler
   * (called when colliding with other objects)
   * @param {me.collision.ResponseObject.prototype} collisionResponse
   * @param {me.Entity|me.Renderable} collisionObject
   * @returns {boolean} Return false to avoid collision, return true or nothing to collide.
   */
  onCollision: function onCollision(collisionResponse, collisionObject) {
    if (collisionResponse.overlap === 0) {
      return false;
    }

    collisionResponse.a.body.gravity = 0;
    collisionResponse.b.body.accel.set(0, 0);
    collisionResponse.a.body.friction.set(0, 0);
    collisionResponse.a.body.jumping = false;
    collisionResponse.a.body.falling = false;

    collisionResponse.b.body.gravity = 0;
    collisionResponse.b.body.accel.set(0, 0);
    collisionResponse.b.body.friction.set(0, 0);
    collisionResponse.b.body.jumping = false;
    collisionResponse.b.body.falling = false;

    collisionObject.body.gravity = 0;
    collisionObject.body.accel.set(0, 0);
    collisionObject.body.friction.set(0, 0);
    collisionObject.body.jumping = false;
    collisionObject.body.falling = false;

    _Sound2.default.playEffect(_externals.assets.audios.collide);

    _Debug2.default.debugCollision(collisionResponse);
  }
});

exports.default = PlayerController;

/***/ }),
/* 15 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Simple physics calculator
 */

Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var Calc = function () {
  function Calc() {
    _classCallCheck(this, Calc);
  }

  _createClass(Calc, null, [{
    key: 'time',


    /**
     * @param {number} distance
     * @param {number} velocity
     * @returns {number}
     */
    value: function time(distance, velocity) {
      return distance / velocity;
    }

    /**
     * @param {number} velocity
     * @param {number} time
     * @returns {number}
     */

  }, {
    key: 'distance',
    value: function distance(velocity, time) {
      return velocity * time;
    }

    /**
     * @param {number} distance
     * @param {number} time
     * @param {number} [speed] Velocity multiplier
     * @returns {number}
     */

  }, {
    key: 'acceleration',
    value: function acceleration(distance, time) {
      var speed = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 1;

      return this.velocity(distance, time, speed) / time;
    }

    /**
     * @param {number} distance
     * @param {number} time
     * @param {number} [speed] Velocity multiplier
     * @returns {number}
     */

  }, {
    key: 'velocity',
    value: function velocity(distance, time) {
      var speed = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 1;

      return distance / time * speed;
    }
  }]);

  return Calc;
}();

exports.default = Calc;

/***/ }),
/* 16 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * @type {(me.event|Melon.event)}
 */
var Events = _externals.Melon.event;

exports.default = Events;

/***/ }),
/* 17 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * Name: shore
 * Layer: interactions
 */
exports.default = _externals.Melon.Entity.extend({
  /**
   * constructor
   */
  init: function init(x, y, settings) {
    this._super(_externals.Melon.Entity, 'init', [x, y, settings]);
  }
});

/***/ }),
/* 18 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * Represents a readable piece of text.
 *
 * Name: text
 * Layer: interactions
 */
exports.default = _externals.Melon.Entity.extend({
  /**
   * constructor
   */
  init: function init(x, y, settings) {
    this._super(_externals.Melon.Entity, 'init', [x, y, settings]);
  }
});

/***/ }),
/* 19 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * Represents a tree where the trainer can find Pokemon using "headbutt".
 *
 * Name: tree
 * Layer: interactions
 */
exports.default = _externals.Melon.Entity.extend({
  /**
   * constructor
   */
  init: function init(x, y, settings) {
    this._super(_externals.Melon.Entity, 'init', [x, y, settings]);
  }
});

/***/ }),
/* 20 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * Warps are a way to travel between maps
 *
 * Name: warp
 * Layer: interactions
 */
exports.default = _externals.Melon.Entity.extend({
  /**
   * constructor
   */
  init: function init(x, y, settings) {
    this._super(_externals.Melon.Entity, 'init', [x, y, settings]);
  }
});

/***/ }),
/* 21 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* This is an auto-generated file. Please use gulp to update it. */


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.TitleScreen = exports.S01_NewBarkTown = exports.LoadingScreen = undefined;

var _LoadingScreen = __webpack_require__(5);

var _LoadingScreen2 = _interopRequireDefault(_LoadingScreen);

var _S01_NewBarkTown = __webpack_require__(22);

var _S01_NewBarkTown2 = _interopRequireDefault(_S01_NewBarkTown);

var _TitleScreen = __webpack_require__(23);

var _TitleScreen2 = _interopRequireDefault(_TitleScreen);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = {
  LoadingScreen: _LoadingScreen2.default,
  S01_NewBarkTown: _S01_NewBarkTown2.default,
  TitleScreen: _TitleScreen2.default
};
exports.LoadingScreen = _LoadingScreen2.default;
exports.S01_NewBarkTown = _S01_NewBarkTown2.default;
exports.TitleScreen = _TitleScreen2.default;

/***/ }),
/* 22 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

var _Sound = __webpack_require__(2);

var _Sound2 = _interopRequireDefault(_Sound);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

exports.default = _externals.Melon.ScreenObject.extend({
  /**
   *  action to perform on state change
   */
  onResetEvent: function onResetEvent() {
    // load a level
    _externals.Melon.levelDirector.loadLevel(_externals.assets.maps.S01_NewBarkTown);
    _Sound2.default.playMusic(_externals.assets.audios.S01_NewBarkTown);
  },


  /**
   *  action to perform when leaving this screen (state change)
   */
  onDestroyEvent: function onDestroyEvent() {
    // WIP
  }
});

/***/ }),
/* 23 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

exports.default = _externals.Melon.ScreenObject.extend({
  /**
   *  action to perform on state change
   */
  onResetEvent: function onResetEvent() {
    // TODO
  },


  /**
   *  action to perform when leaving this screen (state change)
   */
  onDestroyEvent: function onDestroyEvent() {
    // TODO
  }
});

/***/ }),
/* 24 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _externals = __webpack_require__(0);

var _Resolution = __webpack_require__(6);

var _Resolution2 = _interopRequireDefault(_Resolution);

var _Controls = __webpack_require__(1);

var _Controls2 = _interopRequireDefault(_Controls);

var _Debug = __webpack_require__(4);

var _Debug2 = _interopRequireDefault(_Debug);

var _LoadingScreen = __webpack_require__(5);

var _LoadingScreen2 = _interopRequireDefault(_LoadingScreen);

var _StaticCollisionBody = __webpack_require__(25);

var _StaticCollisionBody2 = _interopRequireDefault(_StaticCollisionBody);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var GameLoader = function () {

  /**
   * Initializes the engine and triggers the Game.load function whenever the window is ready.
   * This needs to be called before anything else.
   *
   * @param {system/Game} gameClass Game to load
   */
  function GameLoader(gameClass) {
    _classCallCheck(this, GameLoader);

    if (window === undefined) {
      throw new Error('The environment is not compatible. The window object is undefined.');
    }

    // Create and expose the game object
    /**
     * @type {system/Game.prototype}
     */
    this.game = _externals.Melon.$game = new gameClass(_externals.config);

    // Set FPS
    _externals.Melon.sys.fps = _externals.config.video.fps;

    // Disable gravity
    _externals.Melon.sys.gravity = 0;

    // For this engine we don't need to apply physics when colliding
    _externals.Melon.Body = _StaticCollisionBody2.default;

    // Set the default loading screen
    _externals.Melon.DefaultLoadingScreen = _LoadingScreen2.default;
  }

  /**
   * Initializes video and audio and calls the onLoad function when the assets are pre-loaded.
   */


  _createClass(GameLoader, [{
    key: 'load',
    value: function load() {
      _Resolution2.default.init();
      _Debug2.default.init();

      // Initialize the video.
      if (!_externals.Melon.video.init(_Resolution2.default.defaultWidth, _Resolution2.default.defaultHeight, {
        wrapper: _externals.config.wrapper, // ID of the HTML element
        scale: _Resolution2.default.scale,
        renderer: _externals.Melon.video[_externals.config.video.renderer],
        antiAlias: false
      })) {
        alert("Your browser does not support the HTML5 " + _externals.config.video.renderer + " renderer.");
        return;
      }

      // Initialize the audio.
      _externals.Melon.audio.init("mp3,ogg");

      // Initialize and bind the controls
      _Controls2.default.bind();

      // Set and load all resources.
      // This will also automatically switch to the loading screen (Melon.state.LOADING)
      // It will call 'onLoad' once all resources are loaded.
      _externals.Melon.loader.preload(_externals.assets._files, this.game.start.bind(this.game));
    }
  }, {
    key: 'loadOnReady',
    value: function loadOnReady() {
      var _this = this;

      window.onReady(function () {
        // Load the game
        _this.load();
      });
    }
  }]);

  return GameLoader;
}();

exports.default = GameLoader;

/***/ }),
/* 25 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _externals = __webpack_require__(0);

/**
 * Represents a body that does not have a velocity when collides with another,
 * so it stops moving on collision but it does not bounce.
 *
 * @type {(me.Body|me.Rect)}
 */
var StaticCollisionBody = _externals.Melon.Body.extend({
  respondToCollision: function respondToCollision(response) {
    var overlap = response.overlapV;

    // Move out of the other object shape
    this.entity.pos.sub(overlap);
    // TODO: move to a full tile, calculating that pos.y/32 and pos.x/32 are integers

    // adjust velocity
    if (overlap.x !== 0) {
      this.vel.x = 0;
    }
    if (overlap.y !== 0) {
      this.vel.y = 0;
    }
    // Cancel falling and jumping
    this.falling = false;
    this.jumping = false;
  }
});

exports.default = StaticCollisionBody;

/***/ })
/******/ ]);