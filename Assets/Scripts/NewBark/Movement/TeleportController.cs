using NewBark.Input;
using NewBark.Support.Extensions;
using NewBark.UI;
using UnityEngine;

namespace NewBark.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerMoveController))]
    public class TeleportController : MonoBehaviour
    {
        private int _stairsWaitTime = 500;

        private bool _paused;
        private TeleportPortal _pendingTeleport;

        public TransitionController transitionController;
        public PlayerMoveController PlayerMove => GetComponent<PlayerMoveController>();

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

        public bool Teleport(Vector2 lookingDirection, Vector2 absolutePosition)
        {
            PlayerMove.Director.Stop();

            if (!PlayerMove.Director.Move(lookingDirection, absolutePosition)) return false;
            transform.position = absolutePosition;
            PlayerMove.Director.Stop();
            return true;
        }

        public TeleportPortal CalculatePortal(TeleportPortal destination)
        {
            if (destination.dropZoneLookAt == GameButton.None)
            {
                destination.calculatedDropZoneLookAt = PlayerMove.Director.Path.Move.GetDirectionVector();
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
            if (!Teleport(destination.calculatedDropZoneLookAt, destination.calculatedDropZone))
            {
                return false;
            }

            if (destination.calculatedDropZoneLookAt == Vector2.zero)
            {
                return true;
            }

            // turn around if necessary, to avoid turn around timeout
            if (destination.dropZoneLookAt != GameButton.None)
            {
                PlayerMove.Director.LookAt(destination.calculatedDropZoneLookAt,
                    destination.dropZoneSteps > 0 ? 0 : _stairsWaitTime);
            }

            if (destination.dropZoneSteps == 0)
            {
                return true;
            }

            // move the necessary steps in that direction
            // TODO: refactor into a separate function to be able to delay it too (door enter animation cannot be seen on fade in)
            if (!PlayerMove.Director.Move(destination.calculatedDropZoneLookAt, destination.dropZoneSteps,
                PlayerMove.speed))
            {
                Debug.LogWarning("Moving dropzone steps FAILED...");
            }

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
            if (!IsPortal(other) || IsTeleporting() || IsPaused())
            {
                return;
            }

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
            Pause();
            GameManager.Input.DeselectTarget();
        }

        void OnTransitionInStart()
        {
            Resume();
        }

        void OnTransitionInEnd()
        {
            _pendingTeleport = null;
            GameManager.Input.RestoreTarget();
        }
    }
}
