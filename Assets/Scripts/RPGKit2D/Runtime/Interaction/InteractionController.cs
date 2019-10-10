using RPGKit2D.Attributes;
using RPGKit2D.Dialog;
using RPGKit2D.Extensions;
using RPGKit2D.Input;
using RPGKit2D.Movement;
using UnityEngine;

namespace RPGKit2D.Interaction
{
    public class InteractionController : MonoBehaviour
    {
        public AnimationController m_animationController;
        public DialogManager m_DialogManager;
    
        [Layer] public int interactableLayer = 8;
        public float raycastDistance = 1f;

        void Update()
        {
            ActionButton action = LegacyInputManager.GetPressedActionButton();
            DirectionButton dir = m_animationController.GetLastAnimationDirection();
            Vector3 dirVector = LegacyInputManager.GetDirectionButtonVector(dir);
            RaycastHit2D hit = CheckInteractableRaycast(dirVector);

            if (!hit || !hit.collider || !hit.collider.gameObject.HasComponent<Interactable>())
            {
                Debug.DrawRay(transform.position, dirVector, Color.red);
                Debug.DrawRay(transform.position, hit.point, Color.blue);
                return;
            }

            Debug.DrawRay(transform.position, dirVector, Color.green);
            Debug.DrawRay(transform.position, hit.point, Color.blue);
        
            hit.collider.gameObject.GetComponent<Interactable>()
                .Interact(new InteractionContext(dir, action, m_DialogManager));
        }

        private RaycastHit2D CheckInteractableRaycast(Vector2 direction)
        {
            Vector2 startingPosition = transform.position;
            return Physics2D.Raycast(
                startingPosition, direction, raycastDistance, 1 << interactableLayer
            );
        }
    }
}
