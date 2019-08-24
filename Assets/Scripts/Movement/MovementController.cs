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
        public InputController inputController;
        public AnimationController animationController;

        [Tooltip("Offset between the transform dimensions and the transform anchor point")]
        public Vector2 anchorPointOffset = new Vector2(0.5f, 0.5f);

        [Tooltip("Initial movement speed. This can be customized per movement command individually.")]
        public int speed = GridRoute.DefaultSpeed;

        public int tilesPerStep = 1;
        public GridRoute currentRoute = null;
        private bool _paused = false;

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

        private void FixedUpdate()
        {
            if (IsPaused())
            {
                return;
            }

            UpdateMovement();
        }

        public void UpdateMovement()
        {
            if (IsPaused())
            {
                return;
            }

            InputInfo input = inputController.GetInputInfo();
            InputInfo prevInput = inputController.GetPreviousInputInfo();

            if (!input.HasDirection())
            {
                if (animationController.IsMoving())
                {
                    animationController.UpdateAnimation(false);
                }

                SnapToGrid();
                return;
            }

            if (currentRoute is null || currentRoute.IsDefaults())
            {
                currentRoute = CreateRoute(prevInput.direction, input.direction, tilesPerStep);
                onMoveStart.Invoke();
            }

            if (prevInput.direction != currentRoute.destination.direction)
            {
                animationController.UpdateAnimation(currentRoute.destination.direction);
            }

            if (currentRoute.destination.coords == transform.position.ToVector2())
            {
                Debug.LogFormat("<b>Movement Controller</b>: Arrived to Destination");
                currentRoute = null;
                onMoveFinish.Invoke();
                SnapToGrid();
                return;
            }

            if (currentRoute.destination.direction == MoveDirection.NONE)
            {
                Debug.LogWarning("<b>Movement Controller</b>: Destination Direction is NONE. currentRoute = " +
                                 currentRoute);
                currentRoute = null;
                SnapToGrid();
                return;
            }

            if (IsMoving() && (currentRoute.destination.direction != MoveDirection.NONE) &&
                (currentRoute.destination.direction == collisionController.lastCollision.direction))
            {
                // if moving to the direction of the last collision, just snap
                Debug.LogFormat("<b>Movement Controller</b>: Movement Blocked By Collision");
                currentRoute = null;
                SnapToGrid();
                return;
            }

            animationController.UpdateAnimation(true);
            DeltaMoveTo(currentRoute);

            var pos = transform.position;

            Debug.LogFormat(
                "<b>Movement Controller</b>: Movement Detected, route=(" + currentRoute +
                "), currentPosition=" + pos.ToFormattedString()
            );
        }

        public void DeltaMoveTo(GridRelativeRoute path)
        {
            DeltaMoveTo(MovementCalculator.CalcRoute(path));
        }

        public void DeltaMoveTo(GridRoute route)
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

        public bool IsCurrentMoveComplete()
        {
            return (currentRoute is null) || (currentRoute.destination.coords == transform.position.ToVector2());
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

        public void Stop()
        {
            if (!IsCurrentMoveComplete())
            {
                onMoveCancel.Invoke();
            }

            currentRoute = null;
        }

        public bool IsPaused()
        {
            return _paused;
        }
    }
}