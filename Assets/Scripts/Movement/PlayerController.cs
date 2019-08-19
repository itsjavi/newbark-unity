using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class PlayerController: MonoBehaviour
    {
        public AnimationManager animationManager;
        public MovementManager movementManager;
        public CollisionManager collisionManager;
        public InteractionManager interactionManager;

        private void Awake()
        {
            collisionManager.Init(animationManager, movementManager);
        }

        void FixedUpdate()
        {
            InputInfo input = InputManager.GetPressedButtons();
            if (input.direction != MoveDirection.NONE)
            {
                Debug.Log(input.direction);
            }
            
            animationManager.Update(input.direction);
            movementManager.Snap();

            interactionManager
                .RaycastUpdate(input, transform.position, animationManager.GetCurrentFaceDirectionVector());
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            collisionManager.CollisionEnter2D(other);
        }
        
        void OnCollisionStay2D(Collision2D other)
        {
            collisionManager.CollisionStay2D(other);
        }
    }
}