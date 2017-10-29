"use strict";
import Melon from 'melonjs';
import Config from 'config';

export default {
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
  playMusic(name, volume) {
    if (!Config.sound.enabled) {
      return false;
    }
    this.channels.BGM = name;

    Melon.audio.playTrack(name, (!isNaN(volume) ? volume : Config.sound.volume.music / 100));
  },

  /**
   * @param {String} name
   * @param {Number} [volume]
   * @param {String} [channel] Sound channel name.
   * @return {(Number|boolean)} the sound instance ID or false if could not play it.
   */
  playEffect(name, volume, channel) {
    if (!Config.sound.enabled) {
      return false;
    }
    channel = channel || "SFX_1";

    if (this.channels[channel] !== false) {
      // Only allow one SFX at a time per channel, to avoid overlapping
      return false;
    }
    this.channels[channel] = name;
    Melon.audio.play(name, false,
      () => {
        this.channels[channel] = false;
      }, (!isNaN(volume) ? volume : Config.sound.volume.effects / 100)
    );
  }
};
