using NewBark.Input;
using NewBark.Support.Extensions;
using UnityEngine;

namespace NewBark.Movement
{
    [RequireComponent(typeof(MovementController2))]
    public class TeleportController2 : MonoBehaviour
    {
        private bool _teleporting;

        public MovementController2 Movement => GetComponent<MovementController2>();

        private void FixedUpdate()
        {
            // this is necessary to detect if the teleport has finished
            if (IsTeleporting() && !Movement.IsMoving() && !Movement.IsTurningAround())
            {
                Unlock();
            }
        }

        private bool IsPortal(Collider2D other)
        {
            return other.gameObject.HasComponent<TeleportPortal>();
        }

        private TeleportPortal GetPortal(Collider2D other)
        {
            return other.gameObject.GetComponent<TeleportPortal>();
        }

        public bool Teleport(Vector2 absolutePosition, Vector2 lookingDirection)
        {
            Lock();
            Movement.Stop();
            return Movement.MoveImmediate(absolutePosition, lookingDirection);
        }

        private void Lock()
        {
            _teleporting = true;
            Movement.DisableInputCapture();
        }

        private void Unlock()
        {
            _teleporting = false;
            Movement.EnableInputCapture();
        }

        public bool Teleport(TeleportPortal destination)
        {
            Vector2 dir;

            if (destination.moveDirection == DirectionButton.NONE && Movement.PreviousDirection != null)
            {
                dir = Movement.PreviousDirection.Value;
            }
            else
            {
                dir = LegacyInputManager.GetDirectionButtonVector(destination.moveDirection);
            }

            Vector2 coords = (Vector2) destination.dropZone.position + destination.dropZoneOffset;

            // first, move to the drop zone immediately, without animation
            if (!Teleport(coords, dir))
            {
                Unlock();
                //Debug.Log("no teleport possible");
                return false;
            }

            if (dir == Vector2.zero)
            {
                //Debug.Log("dir = Vector2.zero");
                return true;
            }

            // turn around if necessary, to avoid turn around timeout
            Movement.LookAt(dir, 500);

            if (destination.moveSteps == 0)
            {
                //Debug.Log("destination.moveSteps = 0");
                return true;
            }

            // move the necessary steps in that direction
            return Movement.Move(dir, destination.moveSteps);
        }

        public Vector2 CalculateFinalDestination(Vector2 origin, TeleportPortal destination)
        {
            var dir = LegacyInputManager.GetDirectionButtonVector(destination.moveDirection);
            return (origin + (Vector2) destination.dropZone.position + destination.dropZoneOffset +
                    (dir * destination.moveSteps));
        }

        private bool IsTeleporting()
        {
            if (!_teleporting)
            {
                return false;
            }

            if (Movement.CurrentDestination == transform.position)
            {
                return false;
            }

            return true;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            // For the OnTriggerStay2D event to be fired while the object is in contact, the Rigid2D body Sleep Mode
            // has to be on "Never Sleep", otherwise this is only triggered once

            if (!IsPortal(other) || IsTeleporting())
            {
                return;
            }

            Teleport(GetPortal(other));
        }
    }
}
