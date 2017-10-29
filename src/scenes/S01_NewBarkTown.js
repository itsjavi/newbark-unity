'use strict';
import Melon from 'melonjs';
import Sound from 'system/Sound';
import assets from 'assets';

export default Melon.ScreenObject.extend({
  /**
   *  action to perform on state change
   */
  onResetEvent() {
    // load a level
    Melon.levelDirector.loadLevel(assets.maps.S01_NewBarkTown);
    Sound.playMusic(assets.audios.S01_NewBarkTown);
  },

  /**
   *  action to perform when leaving this screen (state change)
   */
  onDestroyEvent() {
    // WIP
  }
});
