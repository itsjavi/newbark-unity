using System;
using System.Collections.Generic;
using NewBark.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark.Movement
{
    [RequireComponent(typeof(AnimationController2))]
    public class MovementController2 : MonoBehaviour
    {
        public AudioClip collisionSound;
        public float speed = 5;
        private float _initialSpeed = 5;
        private float AnimationSpeed => (speed * 10) / 60;

        private int tilesPerStep = 1; // TODO: make raycast detect collisions properly if > 1

        [Tooltip("Time to turn around to a different position, in milliseconds.")]
        public float turnAroundWaitTime = 125;

        public float clampOffset = 0.5f;

        private float _turnAroundWaitTimeCounter;
        private bool _stopCurrentMovementOnTurnAround = true;

        private Vector2? _previousDestination;
        private Vector2? _previousDirection;
        private Vector2? _currentDestination;
        private Vector2? _currentDirection;


        public Vector2? PreviousDestination => _previousDestination;
        public Vector2? PreviousDirection => _previousDirection;
        public Vector2? CurrentDestination => _currentDestination;
        public Vector2? CurrentDirection => _currentDirection;

        public AnimationController2 AnimationController => GetComponent<AnimationController2>();

        private void Start()
        {
            _initialSpeed = speed;
        }

        void Update()
        {
            DrawHit(AnimationController.GetLastAnimationDirection());
        }

        private bool IsTurningAround()
        {
            if (_turnAroundWaitTimeCounter > 0)
            {
                _turnAroundWaitTimeCounter -= Time.deltaTime * 1000;
                return true;
            }

            if (_turnAroundWaitTimeCounter < 0)
            {
                _turnAroundWaitTimeCounter = 0;
                if (_stopCurrentMovementOnTurnAround)
                {
                    Stop();
                }

                return true;
            }

            return false;
        }

        private void FixedUpdate()
        {
            if (IsTurningAround())
            {
                return;
            }

            if (_currentDestination == null)
            {
                return;
            }

            var tr = transform;
            var dest = _currentDestination.Value;

            if (HasArrived(tr.position, dest))
            {
                // fix possible wrong decimals
                tr.position = GetClampedPosition(tr.position);
                // Debug.Log("Arrived to destination.");
                Stop();
                return;
            }

            tr.position = Vector3.MoveTowards(tr.position, dest, Time.fixedDeltaTime * speed);
            //Debug.Log("Updated position = " + tr.position);

            // Lock rotation, just in case
            // tr.rotation = new Quaternion(0, 0, 0, 0);
        }

        public bool HasArrived(Vector2 current, Vector2 destination)
        {
            return destination == current;
        }

        public void OnButtonDirectionalHold(KeyValuePair<InputButton, InputAction> btn)
        {
            Move(btn.Value.ReadValue<Vector2>(), tilesPerStep);
        }

        public void OnMultipleButtonsHold(Dictionary<InputButton, InputAction> buttons)
        {
            if (IsRunMode(buttons))
            {
                StartRunMode();
            }
        }

        public void OnButtonBPerformed(InputAction.CallbackContext ctx)
        {
            if (IsRunMode())
            {
                StopRunMode();
            }
        }

        public void OnButtonDirectionalCanceled(InputAction.CallbackContext ctx)
        {
            // Without this check, turn-around movement wouldn't have animation or a very short one:
            if (IsTurningAround())
            {
                return;
            }

            AnimationController.StopAnimation();
        }

        private Vector2 LockDiagonal(Vector2 direction)
        {
            if (Math.Abs(direction.x) > 0 && Math.Abs(direction.y) > 0)
            {
                // LOCK DIAGONAL MOVEMENT
                direction.y = 0;
            }

            return direction;
        }

        private bool Move(Vector2 destination, Vector2 direction)
        {
            if (!CanMove())
            {
                // Debug.Log("Cant move ");
                return false;
            }

            AnimationController.UpdateAnimation(direction, direction, AnimationSpeed);

            if (direction != _previousDirection)
            {
                // Should have turn around without moving
                _previousDirection = direction;
                _turnAroundWaitTimeCounter = turnAroundWaitTime;
                return true;
            }

            var col = CheckCollision(direction);

            if (col)
            {
                Debug.Log("Detected collision with " + col.gameObject.name);
                GameManager.Audio.PlaySfxWhenIdle(collisionSound);
                return false;
            }

            _currentDirection = direction;
            _currentDestination = destination;

            return true;
        }

        public bool Move(Vector2 direction, int tiles = 1)
        {
            direction = LockDiagonal(direction) * tiles;
            return Move(transform.position + (Vector3) direction, direction);
        }

        public bool IsMoving()
        {
            return (_currentDestination != null);
        }

        public void StartRunMode()
        {
            speed = _initialSpeed * 2;
        }

        public void StopRunMode()
        {
            speed = _initialSpeed;
        }

        public bool IsRunMode()
        {
            return speed > _initialSpeed;
        }

        public bool IsRunMode(Dictionary<InputButton, InputAction> buttons)
        {
            var btnB = false;
            var btnDir = false;

            foreach (var keyValue in buttons)
            {
                if (keyValue.Key == InputButton.B)
                {
                    btnB = true;
                    continue;
                }

                if (GameManager.Input.IsDirectional(keyValue.Key))
                {
                    btnDir = true;
                }

                if (btnDir && btnB)
                {
                    return true;
                }
            }

            return false;
        }

        public void Stop()
        {
            _previousDestination = _currentDestination;
            _previousDirection = _currentDirection;
            _currentDestination = null;
            _currentDirection = null;
            AnimationController.StopAnimation();
        }

        public bool CanMove()
        {
            return !IsMoving() && (GameManager.Input.target == gameObject);
            // Debug.Log("Input target is not " + name);
        }

        public bool CanMove(Vector2 towards)
        {
            return CanMove() && !WillCollide(towards);
        }

        private Vector3 GetClampedPosition(Vector3 position)
        {
            return new Vector3(
                GetClampedPositionAxis(position.x, clampOffset), 
                GetClampedPositionAxis(position.y, clampOffset), 
                0
                );
        }

        private float GetClampedPositionAxis(float val, float offset)
        {
            float mod = val % 1f;

            if (System.Math.Abs(mod - clampOffset) < double.Epsilon) // more precise than: if (mod == fraction)
            {
                return val;
            }

            if (val < 0f)
            {
                return (val - mod) - clampOffset;
            }

            return (val - mod) + clampOffset;
        }

        // =============================================================================================
        // TODO: refactor following methods into CollisionController:

        public bool WillCollide(Vector2 direction)
        {
            return CheckCollision(direction) != null;
        }

        public Collider2D CheckCollision(Vector2 direction)
        {
            return CheckHit(direction).collider;
        }

        public RaycastHit2D CheckHit(Vector2 direction, int layerIndex, int distance)
        {
            return Physics2D.Raycast(
                transform.position, direction, distance, 1 << layerIndex
            );
        }

        public RaycastHit2D CheckHit(Vector2 direction, int layerIndex = GameManager.CollisionsLayer)
        {
            return CheckHit(direction, layerIndex, tilesPerStep);
        }

        void DrawHit(Vector2 dir)
        {
            var hit = CheckHit(dir);
            if (!hit || !hit.collider)
            {
                Debug.DrawRay(transform.position, dir, Color.green);
                Debug.DrawRay(transform.position, hit.point, Color.blue);
                return;
            }

            Debug.DrawRay(transform.position, dir, Color.red);
            Debug.DrawRay(transform.position, hit.point, Color.blue);
        }
    }
}
