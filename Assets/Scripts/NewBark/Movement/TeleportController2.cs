using System.Collections;
using NewBark.Input;
using NewBark.Support.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(MovementController2))]
    public class TeleportController2 : MonoBehaviour
    {
        public bool externalUnlock;

        private bool _teleporting;
        private int _stairsWaitTime = 500;

        private bool _paused;
        private TeleportPortal _pendingTeleport;

        public UnityEvent onTeleportStart;
        public UnityEvent onTeleportFinish;

        public BoxCollider2D Collider => GetComponent<BoxCollider2D>();
        public MovementController2 Movement => GetComponent<MovementController2>();

        private void FixedUpdate()
        {
            // TODO: refactor with internal events

            if (_pendingTeleport && !IsPaused())
            {
                Resume();
                return;
            }

            if (!_pendingTeleport && IsPaused())
            {
                Unpause();
                return;
            }

            // this is necessary to detect if the teleport has finished
            if (IsTeleporting() && !Movement.IsMoving() && !Movement.IsTurningAround())
            {
                if (!externalUnlock)
                {
                    Unlock();
                }

                onTeleportFinish.Invoke();
                Debug.Log("onTeleportFinish.Invoke");
            }
        }

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

            if (!_pendingTeleport) return;

            Teleport(_pendingTeleport, true);

            _pendingTeleport = null;
        }

        private bool IsPortal(Collider2D other)
        {
            return other.gameObject.HasComponent<TeleportPortal>();
        }

        private TeleportPortal GetPortal(Collider2D other)
        {
            return other.gameObject.GetComponent<TeleportPortal>();
        }

        public void Lock()
        {
            //Debug.Log("Locked");
            _teleporting = true;
            Movement.DisableInputCapture();
        }

        public void Unlock()
        {
            //Debug.Log("Unlocked");
            Unpause();
            _teleporting = false;
            Movement.EnableInputCapture();
        }

        public bool Teleport(Vector2 absolutePosition, Vector2 lookingDirection)
        {
            Movement.Stop();
            Lock();

            if (!Movement.Move(absolutePosition, lookingDirection)) return false;
            transform.position = absolutePosition;
            //Debug.Log("Teleported"); // move immediatelly
            Movement.Stop();
            return true;
        }

        public TeleportPortal CalculatePortal(TeleportPortal destination)
        {
            if (destination.dropZoneLookAt == InputButton.None && Movement.PreviousDirection != null)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination">Calculated destination</param>
        /// <param name="isCalculated">If true, the destination won't be recalculated</param>
        /// <returns></returns>
        public bool Teleport(TeleportPortal destination, bool isCalculated = false)
        {
            if (!isCalculated)
            {
                destination = CalculatePortal(destination);
            }

            // first, move to the drop zone immediately, without animation
            if (!Teleport(destination.calculatedDropZone, destination.calculatedDropZoneLookAt))
            {
                Debug.LogError("Teleport not possible...");
                return false;
            }

            if (destination.calculatedDropZoneLookAt == Vector2.zero)
            {
                //Debug.Log("dir = Vector2.zero");
                return true;
            }

            // turn around if necessary, to avoid turn around timeout
            if (destination.dropZoneLookAt != InputButton.None)
            {
                Movement.LookAt(destination.calculatedDropZoneLookAt,
                    destination.dropZoneSteps > 0 ? 0 : _stairsWaitTime);
            }

            if (destination.dropZoneSteps == 0)
            {
                //Debug.Log("destination.moveSteps = 0");
                return true;
            }

            // move the necessary steps in that direction
            
            // TODO: refactor into a separate function to be able to delay it too (door enter animation cannot be seen on fade in)
            if (!Movement.Move(destination.calculatedDropZoneLookAt, destination.dropZoneSteps))
            {
                Debug.LogWarning("Moving dropzone steps FAILED...");
            }

            return true;
        }

        public void DelayedResume(float seconds)
        {
            StartCoroutine(DelayedResumeCoroutine(seconds));
        }

        IEnumerator DelayedResumeCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Resume();
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

        void OnTriggerEnter2D(Collider2D other)
        {
//            Debug.Log("OnTriggerEnter2D");
            // For the OnTriggerStay2D event to be fired while the object is in contact, the Rigid2D body Sleep Mode
            // has to be on "Never Sleep", otherwise this is only triggered once

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
//                return;
//            }
//
//            Debug.Log("Is OK, scheduling teleport...");

            if (!IsPortal(other) || IsTeleporting() || IsPaused())
            {
                return;
            }

            var calculatedPortal = CalculatePortal(GetPortal(other));

            onTeleportStart.Invoke();
            _pendingTeleport = calculatedPortal;
            if (_pendingTeleport.soundEffect)
            {
                GameManager.Audio.PlaySfx(_pendingTeleport.soundEffect);
            }
        }
    }
}
