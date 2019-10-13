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
        public int tilesPerStep = 1;
        public int speed = 4;
        public float clampOffset = 0.5f;

        private Vector2? _currentDestination;
        private Vector2? _currentDirection;
        public AnimationController2 AnimationController => GetComponent<AnimationController2>();


        void Update()
        {
            DrawHit(AnimationController.GetLastAnimationDirection());
        }

        private void FixedUpdate()
        {
            if (_currentDestination == null)
            {
                return;
            }

            if (_currentDirection == null)
            {
                return;
            }


            var tr = transform;
            var dest = _currentDestination.Value;
            var dir = _currentDirection.Value;

            if (HasArrived(tr.position, dest))
            {
                Debug.Log("Arrived to dest.");
                _currentDestination = null;
                _currentDirection = null;
                return;
            }

            tr.position = Vector3.MoveTowards(tr.position, dest, Time.fixedDeltaTime * speed);
            tr.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("Updated position = " + tr.position);
        }

        public bool HasArrived(Vector2 current, Vector2 destination)
        {
            return destination == current || destination.x > current.x || destination.y > current.y;
        }

        public void OnButtonDirectionalHold(KeyValuePair<InputButton, InputAction> btn)
        {
            if (!CanMove())
            {
                Debug.Log("Cant move");
                return;
            }

            var direction = btn.Value.ReadValue<Vector2>();

            AnimationController.UpdateAnimation(direction, direction);

            var col = CheckCollision(direction);

            if (col)
            {
                Debug.Log("Will collide with " + col.gameObject.name);
                GameManager.Audio.PlaySfx(collisionSound);
                return;
            }

            AnimationController.UpdateAnimation(speed);
            //transform.position += (Vector3) direction;
            // todo: track current destination and do not move until finished
            _currentDirection = direction;
            _currentDestination = transform.position + (Vector3) direction;
            Debug.Log("_currentDirection set: " + _currentDirection);
            Debug.Log("_currentDestination set: " + _currentDestination);

            //iTween.MoveBy(gameObject, direction, 0.24F);
            //Debug.Log("Moving");
        }

        public void OnButtonDirectionalCanceled(InputAction.CallbackContext ctx)
        {
            AnimationController.UpdateAnimation(0f);
        }

        public bool IsMoving()
        {
            return _currentDestination != null;
        }

        public void Stop()
        {
            _currentDestination = null;
            _currentDirection = null;
            AnimationController.UpdateAnimation(0f);
        }

        public bool CanMove()
        {
            if (GameManager.Input.target == gameObject) return !IsMoving();
            Debug.Log("Input target is not " + name);
            return false;
        }

        public bool CanMove(Vector2 direction)
        {
            return CanMove() && !WillCollide(direction);
        }

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
            return CheckHit(direction, layerIndex, 1);
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
