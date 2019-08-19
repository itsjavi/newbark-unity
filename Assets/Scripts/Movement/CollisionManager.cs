using System;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public class CollisionManager
    {
        public AudioSource defaultCollisionSound;

        private AnimationManager _animationManager;
        private MovementManager _movementManager;

        [field: Header("Debug Info")] public Collision2D LastCollision { get; private set; }
        public MoveDirection LastCollisionDirection { get; private set; }

        public void Init(AnimationManager animationManager, MovementManager movementManager)
        {
            _animationManager = animationManager;
            _movementManager = movementManager;
        }

        public void CollisionEnter2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = _animationManager.GetCurrentFaceDirection();

            // keep character snapped in the tile
            _movementManager.Snap();

            PlayCollisionSound(other);
        }

        public void CollisionStay2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = _animationManager.GetCurrentFaceDirection();

            if (_movementManager.IsMoving())
            {
                PlayCollisionSound(other);
            }

            _movementManager.StopAll();
        }

        private bool PlayCollisionSound(Collision2D other)
        {
            AudioSource sound = defaultCollisionSound;

            if (!(other is null) && other.gameObject.HasComponent<AudioSource>())
            {
                sound = other.gameObject.GetComponentSafe<AudioSource>();
            }

            if (sound is null)
            {
                sound = defaultCollisionSound;
            }

            if (sound is null || !sound.enabled || sound.isPlaying) return false;
            sound.Play();
            return true;
        }
    }
}