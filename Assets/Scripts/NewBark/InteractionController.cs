using NewBark.Attributes;
using NewBark.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark
{
    [RequireComponent(typeof(PlayerController))]
    public class InteractionController : MonoBehaviour
    {
        [Layer] public int interactableLayer = 8;
        public float raycastDistance = 1f;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        protected void OnButtonAPerformed(InputAction.CallbackContext ctx)
        {
            Vector3 direction = _playerController.GetFaceDirection();
            RaycastHit2D hit = _playerController.CheckHit(direction, interactableLayer, raycastDistance);
            GameObject obj = CheckInteractarableHit(hit);

            var pos = transform.position;

            if (!obj)
            {
                Debug.DrawRay(pos, direction, Color.red);
                Debug.DrawRay(pos, hit.point, Color.blue);
                return;
            }

            Debug.DrawRay(pos, direction, Color.green);
            Debug.DrawRay(pos, hit.point, Color.blue);

            obj.SendMessage("OnPlayerInteract", GameButton.A, SendMessageOptions.DontRequireReceiver);
        }

        public GameObject CheckInteractarableHit(RaycastHit2D hit)
        {
            if (!hit || !hit.collider || !hit.collider.gameObject)
            {
                return null;
            }

            return hit.collider.gameObject;
        }
    }
}
