game.buttons = {
  UP: "up",
  RIGHT: "right",
  DOWN: "down",
  LEFT: "left",
  A: "a",
  B: "b",
  X: "x",
  Y: "y",
  SELECT: "select",
  START: "start",
  "bind": function () {
    // enable the keyboard
    me.input.bindKey(me.input.KEY.LEFT, this.LEFT, false, true);
    me.input.bindKey(me.input.KEY.RIGHT, this.RIGHT, false, true);
    me.input.bindKey(me.input.KEY.UP, this.UP, false, true);
    me.input.bindKey(me.input.KEY.DOWN, this.DOWN, false, true);

    me.input.bindKey(me.input.KEY.SPACE, this.A);
    me.input.bindKey(me.input.KEY.B, this.B);
    me.input.bindKey(me.input.KEY.X, this.X);
    me.input.bindKey(me.input.KEY.Z, this.Y);
    me.input.bindKey(me.input.KEY.A, this.SELECT);
    me.input.bindKey(me.input.KEY.S, this.START);
  },
  "isPressed": function (buttonName) {
    return me.input.isKeyPressed(buttonName);
  },
  "isUpPressed": function () {
    return this.isPressed(this.UP);
  },
  "isDownPressed": function () {
    return this.isPressed(this.DOWN);
  },
  "isLeftPressed": function () {
    return this.isPressed(this.LEFT);
  },
  "isRightPressed": function () {
    return this.isPressed(this.RIGHT);
  },
  "isAPressed": function () {
    return this.isPressed(this.A);
  },
  "isBPressed": function () {
    return this.isPressed(this.B);
  },
  "isXPressed": function () {
    return this.isPressed(this.X);
  },
  "isYPressed": function () {
    return this.isPressed(this.Y);
  },
  "isSelectPressed": function () {
    return this.isPressed(this.SELECT);
  },
  "isStartPressed": function () {
    return this.isPressed(this.START);
  },
  "getPressed": function () {
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

    return false;
  },
  "isDirectionButtonPressed": function () {
    var button = this.getPressed();
    return !!(button == this.LEFT
    || button == this.RIGHT
    || button == this.UP
    || button == this.DOWN);

  },
  "getPressedAxis": function (button) {
    button ? button : this.getPressed();

    if (button == this.LEFT || button == this.RIGHT) {
      return "x";
    } else if (button == this.UP || button == this.DOWN) {
      return "y";
    } else {
      return false;
    }
  },
  "getSecondAxis": function (pressedAxis) {
    if (pressedAxis === false) {
      return false;
    }
    return pressedAxis == "x" ? "y" : "x";
  }
};