game.audio = {
  "channels": {
    BGM: false,
    SFX_1: false,
    SFX_2: false
  },
  /**
   * @param {String} name
   * @param {Number} [volume]
   * @return {Number|bool} the sound instance ID or false if could not play it.
   */
  "playBgm": function (name, volume) {
    if (!game.config.sound.enabled) {
      return false;
    }
    game.audio.channels.BGM = name;
    me.audio.playTrack(name, !isNaN(volume) ? volume : game.config.sound.volume.music / 100);
  },
  /**
   * @param {String} name
   * @param {Number} [volume]
   * @param {String} [channel] Sound channel name.
   * @return {Number|bool} the sound instance ID or false if could not play it.
   */
  "playSfx": function (name, volume, channel) {
    if (!game.config.sound.enabled) {
      return false;
    }
    channel = channel || "SFX_1";

    if (game.audio.channels[channel] !== false) {
      // Only allow one SFX at a time per channel, to avoid overlapping
      return false;
    }
    game.audio.channels[channel] = name;
    me.audio.play(name, false, function () {
      game.audio.channels[channel] = false;
    }, (!isNaN(volume) ? volume : game.config.sound.volume.effects / 100));
  },
};