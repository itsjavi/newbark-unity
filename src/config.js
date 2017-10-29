"use strict";
export default {
  "debug": true,
  "wrapper": "game",
  "video": {
    "fps": 32,
    "renderer": "CANVAS",
    "tile_size": 32,
    "size": "GBC",
    "size_zoom": 2,
    "size_fit": false,
    "size_presets": {
      "GBC": {
        "width": 160,
        "height": 144
      },
      "GBA": {
        "width": 240,
        "height": 160
      },
      "NDS": {
        "width": 256,
        "height": 192
      },
      "3DS": {
        "width": 400,
        "height": 240
      },
      "SWITCH": {
        "width": 1280,
        "height": 720
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
    "move_velocity": 5,
    "move_diagonal": false,
    "move_tiled": true,
    "move_tiled_time": 1000
  },
  "initial_scene": "S01_NewBarkTown"
};
