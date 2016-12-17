var __is_electron = (typeof window === 'undefined' || (window && window.process && window.process.type));

var __game_config = {
  fps: 32,
  tileSize: 32,
  debug: true,
  screen: {
    wrapper: "screen", // HTML element ID
    scale: (__is_electron ? "auto" : "best"), // "auto" or "best"
    size: "GBC",

    // Doubled sizes of handheld consoles.
    // Originals: GBC = 160x144, GBA = 240x160, NDS = 256x192, 3DS = 400x240
    widths: {
      GBC: 320,
      GBA: 480,
      NDS: 512,
      N3DS: 800,
      custom: 640, // 4x GBC
    },
    heights: {
      GBC: 288,
      GBA: 320,
      NDS: 384,
      N3DS: 480,
      custom: 576 // 4x GBC
    },
    getWidth: function () {
      return this.widths[this.size];
    },
    getHeight: function () {
      return this.heights[this.size];
    },
  },
  sound: {
    enabled: true,
    volume: {
      music: 40,
      effects: 80
    }
  },
  player: {
    moveVelocity: 5,
    moveDiagonal: false,
    moveByTile: true,
    moveByTileTime: 1000
  },
  entities: {
    // Entities to register in the pool
    "player": "Player"
  },
  scenes: {
    "initial": "S01_NewBarkTown"
  }
};

// Export for main.js, which runs in Node
if (typeof module !== 'undefined' && module.exports) {
  module.exports = __game_config;
}

// Add it to the global game object
if (typeof game !== 'undefined') {
  game.config = __game_config;
  game.ENV.isElectron = __is_electron;
}