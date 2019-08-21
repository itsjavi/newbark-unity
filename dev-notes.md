# Dev Notes

## Solutions to solved problems
- TransparentFX layer has to be disabled in Camera's culling mask, to not render the elements on it
- For the OnTriggerStay2D event to work properly, the Rigid2DBody Sleep Mode has to be on "Never Sleep", otherwise this is only triggered once
- The Static flag in the inspector is useful, when enabled, for elements that never move. Optimizes the game, so less calculations are made.
- To avoid built-in physics and use own's, the BoxCollider2D needs to be in trigger mode. That also applies to the player, NPCs, etc. This prevents sending away other RigidBody2D objects.
- In order to detect the collision trigger, the player still needs a RigidBody2D with a material with friction=0, bounciness=0 and
mass=0.1, collision detection = discrete, sleep mode = never sleep, constaints freeze rotation = Z, everything else = 0

## ToDos
- Refactor Movement system (using editor-assigned properties, events like onMoveEnd, move disable/enable, better decoupling,...)
	- MoveTo does not work with warp post movement
	- We need a way to disable step-movement when teleporting, enabling it again for the postWarpMove
	- We need a way to reactivate warping and collisions when movement is finished
- Refactor Warping system after movement one, if needed
- add debug GUI and enable it via the PlayerHouse PC. add steps counter and show it, together with FPS
- add a way to disable input (eg when warping) without disabling movement actions


## Issues
- Maybe warpCoords make moveSteps to not work in WarpController
- Sometimes text dialogs are not in the right order, they get mixed and lost
- Triggering a dialog may not respond to buttons
- When fading on warp player can still move