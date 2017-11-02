'use strict';

/**
 * Simple physics calculator
 */
class Calc {

  /**
   * @param {number} distance
   * @param {number} velocity
   * @returns {number}
   */
  static time(distance, velocity) {
    return distance / velocity;
  }

  /**
   * @param {number} velocity
   * @param {number} time
   * @returns {number}
   */
  static distance(velocity, time) {
    return velocity * time;
  }

  /**
   * @param {number} distance
   * @param {number} time
   * @param {number} [speed] Velocity multiplier
   * @returns {number}
   */
  static acceleration(distance, time, speed = 1) {
    return this.velocity(distance, time, speed) / time;
  }

  /**
   * @param {number} distance
   * @param {number} time
   * @param {number} [speed] Velocity multiplier
   * @returns {number}
   */
  static velocity(distance, time, speed = 1) {
    return (distance / time) * speed;
  }
}

export default Calc;
