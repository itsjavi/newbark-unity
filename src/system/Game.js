'use strict';
import {Melon, _, $} from 'externals';
import entities from 'entities/_all';
import scenes from 'scenes/_all';

class Game {

  constructor(config) {
    this.config = config;

    // Keep a copy of the original config
    this.config.initial = _.cloneDeep(config);

    /**
     * @type {(Zepto|HTMLElement)}
     */
    this.element = $(this.config.wrapper);
  }

  // Run on game resources loaded.
  start() {
    // Register all entities
    _.forOwn(entities, function (entity, entityName) {
      // Add all the entities to the entity pool
      // For every tile type, we should have a registered entity
      Melon.pool.register(entityName.toLowerCase(), entity);
    });

    // Sets the scenes for the different states
    Melon.state.set(Melon.state.READY, new scenes.TitleScreen());
    Melon.state.set(Melon.state.PLAY, new scenes[this.config.initial_scene]());

    // Start the gaMelon.
    Melon.state.change(Melon.state.PLAY);
  }
}

export default Game;
