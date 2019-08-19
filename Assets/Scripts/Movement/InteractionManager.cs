using System;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public class InteractionManager
    {
        [Tooltip("Maximum distance over which to cast the ray.")]
        public float maxDistance = 1f;

        public void RaycastUpdate(InputInfo input, Vector2 currentPosition, Vector2 currentFaceDirection)
        {
            if (currentFaceDirection == Vector2.zero)
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentFaceDirection, maxDistance);

            // Debug.DrawRay(transform.position, dirVector, Color.green);
            OnRaycastHit(hit, input);
        }

        private void OnRaycastHit(RaycastHit2D hit, InputInfo input)
        {
            if (!hit.collider)
            {
                return;
            }

            var interactable = hit.collider.gameObject.GetComponentSafe<Interactable>();
            if (!interactable)
            {
                return;
            }

            // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
            interactable.Interact(input.direction, input.action);
        }
    }
}