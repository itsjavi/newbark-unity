using UnityEngine;

namespace Movement
{
    public class InteractableController : MonoBehaviour
    {
        public MovementController movementController;
        public float raycastDistance = 1f;

        void FixedUpdate()
        {
            InputData input = InputController.GetPressedButtons();

            RaycastUpdate(input);
        }

        private void RaycastUpdate(InputData input)
        {
            Vector3 dirVector = movementController.GetFaceDirectionVector();

            if (dirVector == Vector3.zero)
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, dirVector, raycastDistance);

            // Debug.DrawRay(transform.position, dirVector, Color.green);
            OnRaycastHit(hit, input);
        }

        private void OnRaycastHit(RaycastHit2D hit, InputData input)
        {
            if (!hit.collider)
            {
                return;
            }

            var interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (!interactable)
            {
                return;
            }

            // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
            interactable.Interact(input.direction, input.action);
        }
    }
}