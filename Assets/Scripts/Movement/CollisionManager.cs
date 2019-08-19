using UnityEngine;

namespace Movement
{
    public class CollisionManager
    {
        public AudioSource collisionSound;
        public AnimationManager animationManager;
        public MovementManager movementManager;

        [field: Header("Debug Info")] public Collision2D LastCollision { get; private set; }
        public MoveDirection LastCollisionDirection { get; private set; }

        public void CollisionEnter2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = animationManager.GetCurrentFaceDirection();

            // keep character snapped in the tile
            movementManager.Snap();

            PlayCollisionSound();
        }

        public void CollisionStay2D(Collision2D other)
        {
            LastCollision = other;
            LastCollisionDirection = animationManager.GetCurrentFaceDirection();

            if (movementManager.IsMoving())
            {
                PlayCollisionSound();
            }

            movementManager.StopAll();
        }

        public bool PlayCollisionSound()
        {
            if (collisionSound is null || collisionSound.isPlaying) return false;
            collisionSound.Play();
            return true;
        }
    }
}