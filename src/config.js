'use strict';
export default {
  "debug": true, // default debug status, changeable via the URL hash #debug (needs reload)
  "wrapper": "game",
  "video": {
    "fps": 32,
    "tile_size": 32,
    "renderer": "CANVAS", // CANVAS or WEBGL
    "scale": 2, // "auto" or number
    "preset": "GBC",
    "presets": {
      "GBC": {
        "width": 160,
        "height": 144,
        "ratio": 2
      },
      "GBA": {
        "width": 240,
        "height": 160,
        "ratio": 2
      },
      "NDS": {
        "width": 256,
        "height": 192,
        "ratio": 2
      },
      "3DS": {
        "width": 400,
        "height": 240,
        "ratio": 1
      },
      "SWITCH": {
        "width": 1280,
        "height": 720,
        "ratio": 1
      }
    }
  },
  "sound": {
    "enabled": true,
    "volume": {
      "music": 40,
      "effects": 80
    }
  },
  "player": {
    "velocity_factor": 1, // this affects the number of tiles to move
    "allow_diagonal": false
  },
  "initial_scene": "S01_NewBarkTown"
};
