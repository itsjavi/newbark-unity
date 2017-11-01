'use strict';
import {Melon, _, $} from 'externals';
import entities from 'entities/_all';
import scenes from 'scenes/_all';

class Game {

  constructor(config) {
    /**
     * @type {window.me.$game.config}
     */
    this.initialConfig = _.cloneDeep(config);

    /**
     * @type {window.me.$game.config}
     */
    this.config = config;

    /**
     * @type {(Zepto|HTMLElement)}
     */
    this.element = $(this.config.wrapper);

    // Register all entities
    _.forOwn(entities, function (entity, entityName) {
      // Add all the entities to the entity pool
      // For every tile type, we should have a registered entity
      Melon.pool.register(entityName.toLowerCase(), entity);
    });
  }

  start() {
    // Sets the scenes for the different states
    Melon.state.set(Melon.state.READY, new scenes.TitleScreen());
    Melon.state.set(Melon.state.PLAY, new scenes[this.config.initial_scene]());

    // Start the gaMelon.
    Melon.state.change(Melon.state.PLAY);
  }
}

export default Game;
