using System;
using System.Collections.Generic;
using Movement.GridLocation;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        public CollisionController collisionController;

        [Tooltip("Offset between the transform dimensions and the transform anchor point")]
        public Vector2 anchorPointOffset = new Vector2(0.5f, 0.5f);

        [Tooltip("Initial movement speed. This can be customized per movement command individually.")]
        public int speed = GridRoute.DefaultSpeed;

        public int tilesPerStep = 1;

        private bool _paused;
        private Stack<GridRelativeRoute> _routeStack = new Stack<GridRelativeRoute>();

        [Header("Events")] public UnityEvent onMoveStart;
        public UnityEvent onMoveFinish;
        public UnityEvent onMoveCancel;

        public GridRoute CreateRoute(MoveDirection originDirection, MoveDirection direction, int steps)
        {
            var route = new GridRelativeRoute();
            route.steps = steps;
            route.direction = direction;
            route.origin.direction = originDirection;
            route.origin.coords = transform.position;
            route.anchorPointOffset = anchorPointOffset;

            return MovementCalculator.CalcRoute(route);
        }

        public void UpdateMovement(InputController inputController)
        {
            InputInfo input = inputController.GetInputInfo();
            if (!input.HasDirection() && !IsMoving())
            {
                return;
            }

            InputInfo prevInput = inputController.GetPreviousInputInfo();

            var route = CreateRoute(prevInput.direction, input.direction, tilesPerStep);

            if (
                !IsMoving()
                || (route.destination.direction == MoveDirection.NONE)
                || (route.destination.coords == transform.position.AsVector2())
            )
            {
                SnapToGrid();
                return;
            }

            if (IsMoving() && (route.destination.direction != MoveDirection.NONE) &&
                (route.destination.direction == collisionController.lastCollision.direction))
            {
                // if moving to the direction of the last collision, just snap
                SnapToGrid();
                return;
            }
            
            WalkTo(route);
        }

        public bool HasMovesLeft()
        {
            return _routeStack.Count > 0;
        }

        public bool IsCurrentMoveComplete()
        {
            return true;
        }

        public bool IsMoving()
        {
            return !IsPaused() && !IsCurrentMoveComplete();
        }

        public bool IsIdle()
        {
            return !IsPaused() && !IsMoving();
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void StopAll()
        {
            if (!IsCurrentMoveComplete() || HasMovesLeft())
            {
                onMoveCancel.Invoke();
            }

            _routeStack.Clear();
        }

        public void StopCurrent()
        {
        }

        public bool IsPaused()
        {
            return _paused;
        }

        public void WalkTo(GridRelativeRoute path)
        {
            WalkTo(MovementCalculator.CalcRoute(path));
        }

        public void WalkTo(GridRoute route)
        {
            transform.position =
                MovementCalculator.CalcDeltaPosition(transform.position, route.destination.coords, speed);
            ResetRotation();
        }

        public void SnapToGrid(Vector2 position)
        {
            transform.position = MovementCalculator.CalcSnappedPosition(position, anchorPointOffset);
            ResetRotation();
        }

        public void SnapToGrid()
        {
            SnapToGrid(transform.position);
        }

        private void ResetRotation()
        {
            // override in case collision physics caused object rotation
            transform.rotation = MovementCalculator.ZeroRotation();
        }
    }
}