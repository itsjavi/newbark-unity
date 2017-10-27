import game from 'game';

if (window) {
  window.game = game;
  window.onReady(
    function onReady() {
      game.load();
    }
  );
}

export default game;
