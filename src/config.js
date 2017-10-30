'use strict';
export default {
  "debug": true,
  "wrapper": "game",
  "video": {
    "fps": 60,
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
    "tiles_per_second": 1,
    "velocity": 5,
    "move_diagonally": false,
    "tile_snapping": true,
    "tile_move_time": 1000
  },
  "initial_scene": "S01_NewBarkTown"
};
