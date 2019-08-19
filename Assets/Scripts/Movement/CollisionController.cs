using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class CollisionController : MonoBehaviour
    {
        public AudioSource defaultCollisionSound;

        [FormerlySerializedAs("animationManager")]
        public AnimationController animationController;

        [FormerlySerializedAs("movementManager")]
        public MovementController movementController;

        [field: Header("Debug Info")] public Collision2D LastCollision { get; private set; }
        public MoveDirection LastCollisionDirection { get; private set; }

        void OnCollisionEnter2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = animationController.GetCurrentFaceDirection();

            // keep character snapped in the tile
            movementController.Snap();

            PlayCollisionSound(other);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = animationController.GetCurrentFaceDirection();

            if (movementController.IsMoving())
            {
                PlayCollisionSound(other);
            }

            movementController.StopAll();
        }

        private bool PlayCollisionSound(Collision2D other)
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