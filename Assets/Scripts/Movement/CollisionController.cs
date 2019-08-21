using UnityEngine;

namespace Movement
{
    public class CollisionController : MonoBehaviour
    {
        public AudioSource defaultCollisionSound;
        public AnimationController animationController;
        public MovementController movementController;
        private Collider2D _lastCollision;
        private MoveDirection _lastCollisionDirection;

        void OnTriggerEnter2D(Collider2D other)
        {
            // if (warpController.IsWarpZone(col) || other.gameObject.CompareTag("Bounds"))
            if (other.gameObject.CompareTag("Bounds"))
            {
                return;
            }
            
            _lastCollision = other;
            _lastCollisionDirection = animationController.GetCurrentFaceDirection();

            // keep character snapped in the tile
            movementController.Snap();

            PlayCollisionSound(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            // if (warpController.IsWarpZone(col) || other.gameObject.CompareTag("Bounds"))
            if (other.gameObject.CompareTag("Bounds"))
            {
                return;
            }
            
            _lastCollision = other;
            _lastCollisionDirection = animationController.GetCurrentFaceDirection();

            if (movementController.IsMoving())
            {
                PlayCollisionSound(other);
            }

            movementController.StopAll();
        }

        private bool PlayCollisionSound(Collider2D other)
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