import me from 'melonjs';

let sysButtons = {
  UP: "up",
  RIGHT: "right",
  DOWN: "down",
  LEFT: "left",
  L1: "l1",
  R1: "r1",
  X: "x",
  Y: "y",
  A: "a",
  B: "b",
  SELECT: "select",
  START: "start",

  bind: function () {
    // enable the keyboard
    me.input.bindKey(me.input.KEY.LEFT, this.LEFT, false, true);
    me.input.bindKey(me.input.KEY.RIGHT, this.RIGHT, false, true);
    me.input.bindKey(me.input.KEY.UP, this.UP, false, true);
    me.input.bindKey(me.input.KEY.DOWN, this.DOWN, false, true);

    me.input.bindKey(me.input.KEY.Q, this.L1);
    me.input.bindKey(me.input.KEY.W, this.R1);

    me.input.bindKey(me.input.KEY.A, this.SELECT);
    me.input.bindKey(me.input.KEY.S, this.START);

    me.input.bindKey(me.input.KEY.X, this.X);
    me.input.bindKey(me.input.KEY.Z, this.Y);

    me.input.bindKey(me.input.KEY.SPACE, this.A);
    me.input.bindKey(me.input.KEY.B, this.B);

    // enable the gamepad
    if (navigator && ('getGamepads' in navigator)) {
      // TODO: detect pad connect/disconnect
      let axes = me.input.GAMEPAD.AXES;
      let buttons = me.input.GAMEPAD.BUTTONS;

      me.input.bindGamepad(0, {type: "axes", code: axes.LX, threshold: -0.5}, me.input.KEY.LEFT);
      me.input.bindGamepad(0, {type: "axes", code: axes.LX, threshold: 0.5}, me.input.KEY.RIGHT);
      me.input.bindGamepad(0, {type: "axes", code: axes.LY, threshold: -0.5}, me.input.KEY.UP);
      me.input.bindGamepad(0, {type: "axes", code: axes.LY, threshold: 0.5}, me.input.KEY.DOWN);

      me.input.bindGamepad(0, {type: "axes", code: axes.RX, threshold: -0.5}, me.input.KEY.LEFT);
      me.input.bindGamepad(0, {type: "axes", code: axes.RX, threshold: 0.5}, me.input.KEY.RIGHT);
      me.input.bindGamepad(0, {type: "axes", code: axes.RY, threshold: -0.5}, me.input.KEY.UP);
      me.input.bindGamepad(0, {type: "axes", code: axes.RY, threshold: 0.5}, me.input.KEY.DOWN);

      me.input.bindGamepad(0, {type: "buttons", code: buttons.LEFT}, me.input.KEY.LEFT);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.RIGHT}, me.input.KEY.RIGHT);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.UP}, me.input.KEY.UP);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.DOWN}, me.input.KEY.DOWN);

      me.input.bindGamepad(0, {type: "buttons", code: buttons.L1}, me.input.KEY.Q);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.R1}, me.input.KEY.W);

      me.input.bindGamepad(0, {type: "buttons", code: buttons.SELECT}, me.input.KEY.A);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.START}, me.input.KEY.S);

      me.input.bindGamepad(0, {type: "buttons", code: buttons.FACE_3}, me.input.KEY.X);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.FACE_4}, me.input.KEY.Z);

      me.input.bindGamepad(0, {type: "buttons", code: buttons.FACE_1}, me.input.KEY.SPACE);
      me.input.bindGamepad(0, {type: "buttons", code: buttons.FACE_2}, me.input.KEY.B);
    }
  },
  isPressed: function (buttonName) {
    return me.input.isKeyPressed(buttonName);
  },
  isUpPressed: function () {
    return this.isPressed(this.UP);
  },
  isDownPressed: function () {
    return this.isPressed(this.DOWN);
  },
  isLeftPressed: function () {
    return this.isPressed(this.LEFT);
  },
  isRightPressed: function () {
    return this.isPressed(this.RIGHT);
  },
  isAPressed: function () {
    return this.isPressed(this.A);
  },
  isBPressed: function () {
    return this.isPressed(this.B);
  },
  isXPressed: function () {
    return this.isPressed(this.X);
  },
  isYPressed: function () {
    return this.isPressed(this.Y);
  },
  isSelectPressed: function () {
    return this.isPressed(this.SELECT);
  },
  isStartPressed: function () {
    return this.isPressed(this.START);
  },
  isL1Pressed: function () {
    return this.isPressed(this.L1);
  },
  isR1Pressed: function () {
    return this.isPressed(this.R1);
  },
  getPressed: function () {
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
  isDirectionButtonPressed: function () {
    let button = this.getPressed();
    return (button === this.LEFT
      || button === this.RIGHT
      || button === this.UP
      || button === this.DOWN);

  },
  getPressedAxis: function (button) {
    button = button ? button : this.getPressed();

    if (button === this.LEFT || button === this.RIGHT) {
      return "x";
    } else if (button === this.UP || button === this.DOWN) {
      return "y";
    } else {
      return false;
    }
  },
  getSecondAxis: function (pressedAxis) {
    if (pressedAxis === false) {
      return false;
    }
    return pressedAxis === "x" ? "y" : "x";
  }
};

export default sysButtons;
