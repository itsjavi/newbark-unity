using Movement.GridLocation;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class CollisionController : MonoBehaviour
    {
        public AudioSource defaultCollisionSound;
        public AnimationController animationController;
        public MovementController movementController;
        public GridCollision lastCollision;
        public UnityEvent onCollisionEnter;
        public UnityEvent onCollisionStay;
        public UnityEvent onCollisionExit;

        private bool IsIgnored(Collider2D other)
        {
            // if (warpController.IsWarpZone(col) || other.gameObject.CompareTag("Bounds"))
            if (other.gameObject.CompareTag("Bounds"))
            {
                return true;
            }

            return false;
        }

        private bool RegisterCollision(Collider2D other)
        {
            if (IsIgnored(other))
            {
                return false;
            }

            if (lastCollision is null)
            {
                lastCollision = new GridCollision();
            }

            lastCollision.collider = other;
            lastCollision.direction = animationController.GetCurrentFaceDirection();

            return true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!RegisterCollision(other))
            {
                return;
            }

            Debug.LogFormat("<b>Collision Trigger Enter</b>: " + lastCollision);

            onCollisionEnter.Invoke();

            // keep character snapped in the tile
            //  movementController.SnapToGrid();

            movementController.SnapToGrid();
            PlayCollisionSound(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (!RegisterCollision(other))
            {
                return;
            }

            Debug.LogFormat("<b>Collision Trigger Stay</b>: " + lastCollision);

            onCollisionStay.Invoke();

            movementController.Stop();
            movementController.SnapToGrid();
            PlayCollisionSound(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (IsIgnored(other))
            {
                return;
            }

            Debug.LogFormat("<b>Collision Trigger Exit</b>: " + lastCollision);

            onCollisionExit.Invoke();
            lastCollision = null;
            movementController.SnapToGrid();
        }

        public bool PlayCollisionSound(Collider2D other)
        {
            AudioSource audioSource = defaultCollisionSound;

            if (!(other is null) && other.gameObject.HasComponent<AudioSource>())
            {
                audioSource = other.gameObject.GetComponentSafe<AudioSource>();
            }

            if (audioSource is null || audioSource.clip is null || !audioSource.enabled ||
                audioSource.isPlaying) return false;

            audioSource.Play();
            return true;
        }
    }
}