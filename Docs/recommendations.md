
# Unity (2018) recommendations for new 2D tile-based games

- Use Animation Blend Trees for idle/animate states with multiple directions each
- Deactivate Anti Alias in Settings/Quality for all quality levels.
- Use a Sprite Material with Pixel Snap enabled.
- Remove default (main) camera and create a new one to avoid "Screen position out of view frustum" errors in the console.
- Do not use the built-in Unity Tilemap Collider unless you don't need additional data from the collision object,
use game objects to build box colliders. This way is more flexible and you can add scripts, extra data, etc.
- Disable vsync
