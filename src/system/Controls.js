'use strict';
import {Melon, $} from 'externals';
import Config from "config";

const Input = Melon.input,
  KEYS = Input.KEY,
  PAD_AXES = Input.GAMEPAD.AXES,
  PAD_BUTTONS = Input.GAMEPAD.BUTTONS;

export default {
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

  bind() {
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
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.LX, threshold: -0.5}, KEYS.LEFT);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.LX, threshold: 0.5}, KEYS.RIGHT);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.LY, threshold: -0.5}, KEYS.UP);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.LY, threshold: 0.5}, KEYS.DOWN);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.RX, threshold: -0.5}, KEYS.LEFT);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.RX, threshold: 0.5}, KEYS.RIGHT);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.RY, threshold: -0.5}, KEYS.UP);
    Input.bindGamepad(0, {type: "axes", code: PAD_AXES.RY, threshold: 0.5}, KEYS.DOWN);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.LEFT}, KEYS.LEFT);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.RIGHT}, KEYS.RIGHT);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.UP}, KEYS.UP);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.DOWN}, KEYS.DOWN);

    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.L1}, KEYS.Q);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.R1}, KEYS.W);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.SELECT}, KEYS.A);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.START}, KEYS.S);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.FACE_3}, KEYS.X);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.FACE_4}, KEYS.Z);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.FACE_1}, KEYS.SPACE);
    Input.bindGamepad(0, {type: "buttons", code: PAD_BUTTONS.FACE_2}, KEYS.B);

    let wrapper = $('#' + Config.wrapper); // game element

    this.bindTouchEvent(wrapper, 'swipeUp', KEYS.UP);
    this.bindTouchEvent(wrapper, 'swipeRight', KEYS.RIGHT);
    this.bindTouchEvent(wrapper, 'swipeDown', KEYS.DOWN);
    this.bindTouchEvent(wrapper, 'swipeLeft', KEYS.LEFT);
    this.bindTouchEvent(wrapper, 'swipeLeft', KEYS.LEFT);

    this.bindTouchEvent(wrapper, 'tap', KEYS.SPACE); // A
    this.bindTouchEvent(wrapper, 'longTap', KEYS.S); // START
  },

  bindTouchEvent(el, eventName, key, timeoutTime = 500) {
    let eventTimeout = null;

    el.on(eventName, () => {
      Input.triggerKeyEvent(key, true); // keydown
      if (eventTimeout) {
        clearTimeout(eventTimeout); // cancel prev timeout
      }
      eventTimeout = setTimeout(() => {
        Input.triggerKeyEvent(key, false); // keyup after 500ms
      }, timeoutTime);
    });
  },

  isPressed(buttonName) {
    return Input.isKeyPressed(buttonName);
  },
  isUpPressed() {
    return this.isPressed(this.UP);
  },
  isDownPressed() {
    return this.isPressed(this.DOWN);
  },
  isLeftPressed() {
    return this.isPressed(this.LEFT);
  },
  isRightPressed() {
    return this.isPressed(this.RIGHT);
  },
  isAPressed() {
    return this.isPressed(this.A);
  },
  isBPressed() {
    return this.isPressed(this.B);
  },
  isXPressed() {
    return this.isPressed(this.X);
  },
  isYPressed() {
    return this.isPressed(this.Y);
  },
  isSelectPressed() {
    return this.isPressed(this.SELECT);
  },
  isStartPressed() {
    return this.isPressed(this.START);
  },
  isL1Pressed() {
    return this.isPressed(this.L1);
  },
  isR1Pressed() {
    return this.isPressed(this.R1);
  },
  getPressed() {
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
  isDirectionButtonPressed() {
    let button = this.getPressed();
    return (button === this.LEFT
      || button === this.RIGHT
      || button === this.UP
      || button === this.DOWN);

  },
  getPressedAxis(button) {
    button = button ? button : this.getPressed();

    if (button === this.LEFT || button === this.RIGHT) {
      return "x";
    } else if (button === this.UP || button === this.DOWN) {
      return "y";
    } else {
      return false;
    }
  },
  getPressedOppositeAxis(pressedAxis) {
    if (pressedAxis === false) {
      return false;
    }
    return pressedAxis === "x" ? "y" : "x";
  }
};
