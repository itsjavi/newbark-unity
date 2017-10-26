import me from 'melonjs';
import config from 'config';

let sysAudio = {
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
  playBgm: function (name, volume) {
    if (!config.sound.enabled) {
      return false;
    }
    sysAudio.channels.BGM = name;
    me.audio.playTrack(name, !isNaN(volume) ? volume : config.sound.volume.music / 100);
  },

  /**
   * @param {String} name
   * @param {Number} [volume]
   * @param {String} [channel] Sound channel name.
   * @return {(Number|boolean)} the sound instance ID or false if could not play it.
   */
  playSfx: function (name, volume, channel) {
    if (!config.sound.enabled) {
      return false;
    }
    channel = channel || "SFX_1";

    if (sysAudio.channels[channel] !== false) {
      // Only allow one SFX at a time per channel, to avoid overlapping
      return false;
    }
    sysAudio.channels[channel] = name;
    me.audio.play(name, false, function () {
      sysAudio.channels[channel] = false;
    }, (!isNaN(volume) ? volume : config.sound.volume.effects / 100));
  }
};

export default sysAudio;
