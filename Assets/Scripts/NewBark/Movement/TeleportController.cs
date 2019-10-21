using NewBark.Input;
using NewBark.Support.Extensions;
using NewBark.UI;
using UnityEngine;

namespace NewBark.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(MovementController))]
    public class TeleportController : MonoBehaviour
    {
        private int _stairsWaitTime = 500;

        private bool _paused;
        private TeleportPortal _pendingTeleport;

        public TransitionController transitionController;
        public MovementController Movement => GetComponent<MovementController>();

        public bool IsPaused()
        {
            return _paused;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public void Resume()
        {
            Unpause();

            if (_pendingTeleport is null)
            {
                Debug.LogWarning("pending teleport is null");
                return;
            }

            Teleport(_pendingTeleport, true);
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
            Movement.Stop();

            if (!Movement.ForceMove(absolutePosition, lookingDirection)) return false;
            transform.position = absolutePosition;
            // Debug.Log("Moved to abs position.");
            Movement.Stop();
            return true;
        }

        public TeleportPortal CalculatePortal(TeleportPortal destination)
        {
            if (destination.dropZoneLookAt == GameButton.None && Movement.PreviousDirection != null)
            {
                destination.calculatedDropZoneLookAt = Movement.PreviousDirection.Value;
            }
            else
            {
                destination.calculatedDropZoneLookAt = GameManager.Input.ButtonToVector2(destination.dropZoneLookAt);
            }

            destination.calculatedDropZone = (Vector2) destination.dropZone.position + destination.dropZoneOffset;

            return destination;
        }

        public bool Teleport(TeleportPortal destination, bool isCalculated = false)
        {
            if (!isCalculated)
            {
                destination = CalculatePortal(destination);
            }

            // first, move to the drop zone immediately, without animation
            if (!Teleport(destination.calculatedDropZone, destination.calculatedDropZoneLookAt))
            {
                // Debug.LogError("Teleport not possible...");
                return false;
            }

            if (destination.calculatedDropZoneLookAt == Vector2.zero)
            {
                // Debug.Log("dir = Vector2.zero");
                return true;
            }

            // turn around if necessary, to avoid turn around timeout
            if (destination.dropZoneLookAt != GameButton.None)
            {
                Movement.LookAt(destination.calculatedDropZoneLookAt,
                    destination.dropZoneSteps > 0 ? 0 : _stairsWaitTime);
            }

            if (destination.dropZoneSteps == 0)
            {
                // Debug.Log("Teleport steps = 0 ...");
                return true;
            }

            // move the necessary steps in that direction
            // TODO: refactor into a separate function to be able to delay it too (door enter animation cannot be seen on fade in)
            if (!Movement.ForceMove(destination.calculatedDropZoneLookAt, destination.dropZoneSteps))
            {
                Debug.LogWarning("Moving dropzone steps FAILED...");
            }
            else
            {
                //  Debug.Log("Moving dropzone steps: ");
                Debug.Log(destination.calculatedDropZoneLookAt);
                Debug.Log(destination.dropZoneSteps);
            }

            // Debug.Log("Teleport called.");
            return true;
        }

        private bool IsTeleporting()
        {
            if (!(_pendingTeleport is null))
            {
                return true;
            }

            return false;
        }

        // For the OnTriggerStay2D event to be fired while the object is in contact, the Rigid2D body Sleep Mode
        // has to be on "Never Sleep", otherwise this is only triggered once
        void OnTriggerEnter2D(Collider2D other)
        {
//            Debug.Log("OnTriggerEnter2D");
//
//            if (IsPaused())
//            {
//                Debug.Log("Is paused...");
//            }
//
//            if (IsTeleporting())
//            {
//                Debug.Log("Is teleporting...");
//            }
//
//            if (!IsPortal(other))
//            {
//                Debug.Log("Is not portal...");
//            }
//
//            if (!IsPortal(other) || IsTeleporting() || IsPaused())
//            {
//                return;
//            }
//
//            Debug.Log("Is OK, scheduling teleport...");

            var calculatedPortal = CalculatePortal(GetPortal(other));

            _pendingTeleport = calculatedPortal;
            if (_pendingTeleport.soundEffect)
            {
                GameManager.Audio.PlaySfx(_pendingTeleport.soundEffect);
            }

            if (!(transitionController is null))
            {
                transitionController.TransitionOutIn();
            }
            else
            {
                Resume();
                _pendingTeleport = null;
            }
        }

        void OnTransitionOutStart()
        {
            // Debug.Log("OnTransitionOutStart");
            Pause();
            GameManager.Input.DeselectTarget();
        }

        void OnTransitionOutEnd()
        {
            // Debug.Log("OnTransitionOutEnd");
        }

        void OnTransitionInStart()
        {
            // Debug.Log("OnTransitionInStart");
            Resume();
        }

        void OnTransitionInEnd()
        {
            // Debug.Log("OnTransitionInEnd");
            _pendingTeleport = null;
            GameManager.Input.RestoreTarget();
        }
    }
}
