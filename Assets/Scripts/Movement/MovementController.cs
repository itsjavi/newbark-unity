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
            if (!input.HasDirection())
            {
                Debug.LogFormat("<b>Movement Controller</b>: No Movement Detected. input=(" + input + ")");
                return;
            }

            InputInfo prevInput = inputController.GetPreviousInputInfo();

            var route = CreateRoute(prevInput.direction, input.direction, tilesPerStep);

            if (route.destination.direction == MoveDirection.NONE)
            {
                Debug.LogWarning("<b>Movement Controller</b>: Destination Direction is NONE");
                Debug.LogWarning("<b>Movement Controller</b>: Destination Direction is NONE");
                SnapToGrid();
                return;
            }

            if (route.destination.coords == transform.position.ToVector2())
            {
                Debug.LogFormat("<b>Movement Controller</b>: Arrived to Destination");
                SnapToGrid();
                return;
            }

            if (IsMoving() && (route.destination.direction != MoveDirection.NONE) &&
                (route.destination.direction == collisionController.lastCollision.direction))
            {
                // if moving to the direction of the last collision, just snap
                Debug.LogFormat("<b>Movement Controller</b>: Movement Blocked By Collision");
                SnapToGrid();
                return;
            }

            WalkTo(route);

            var pos = transform.position;

            Debug.LogFormat(
                "<b>Movement Controller</b>: Movement Detected, route=(" + route +
                "), currentPosition=" + pos.ToFormattedString()
            );
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
            var currPosition = transform.position;
            var newPosition = MovementCalculator.CalcSnappedPosition(position, anchorPointOffset);

            if (currPosition.ToVector2() == newPosition)
            {
                return;
            }

            Debug.LogFormat("<b>Movement Controller</b>: Snapping To Grid from=" +
                            currPosition.ToFormattedString() + ", to=" + newPosition.ToFormattedString());

            transform.position = newPosition;
            ResetRotation();
        }

        public void SnapToGrid()
        {
            SnapToGrid(transform.position);
        }

        private void ResetRotation()
        {
            Debug.LogFormat("<b>Movement Controller</b>: Resetting rotation to zero.");
            // override in case collision physics caused object rotation
            transform.rotation = MovementCalculator.ZeroRotation();
        }
    }
}