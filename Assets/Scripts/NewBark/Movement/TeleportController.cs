using NewBark.Input;
using NewBark.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Movement
{
    public class TeleportController : MonoBehaviour
    {
        public MovementController movementController;
        public UnityEvent onWarpEnter;
        public UnityEvent onWarpStay;
        public UnityEvent onWarpFinish;

        private bool _warpingEnabled = true;
        private bool _isWarping = false;

        private void FixedUpdate()
        {
            if (_isWarping && !movementController.IsMoving())
            {
                _isWarping = false;
            }
        }

        public bool IsWarping()
        {
            return _isWarping;
        }

        public void EnableWarping()
        {
            _warpingEnabled = true;
        }

        public void DisableWarping()
        {
            _warpingEnabled = false;
        }

        public bool IsWarpingEnabled()
        {
            return _warpingEnabled;
        }

        private void WarpToDropStart(TeleportPortal destination)
        {
            Vector2 coords = (Vector2) destination.dropZone.transform.position + destination.dropZoneOffset;
            movementController.ClampPositionTo(new Vector3(coords.x, coords.y, 0));
        }

        private void MoveToDropEnd(TeleportPortal destination)
        {
            if (destination.moveSteps == 0)
            {
                if (destination.moveDirection != DirectionButton.NONE)
                {
                    movementController.TriggerDirectionButton(destination.moveDirection);
                }

                return;
            }

            movementController.Move(destination.moveDirection, destination.moveSteps);
        }

        private bool IsWarpZone(Collider2D other)
        {
            return other.gameObject.HasComponent<TeleportPortal>();
        }

        private TeleportPortal GetWarpZone(Collider2D other)
        {
            return other.gameObject.GetComponent<TeleportPortal>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
            {
                return;
            }

            onWarpEnter.Invoke();
        }

        // For the OnTriggerStay2D event to work properly, the Rigid2D body Sleep Mode has to be on "Never Sleep", otherwise this is only triggered once
        void OnTriggerStay2D(Collider2D other)
        {
            if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
            {
                return;
            }

            _isWarping = true;
            WarpToDropStart(GetWarpZone(other));
            onWarpStay.Invoke();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!IsWarpZone(other) || !IsWarpingEnabled() || IsWarping())
            {
                return;
            }

            MoveToDropEnd(GetWarpZone(other));
            onWarpFinish.Invoke();
        }
    }
}
