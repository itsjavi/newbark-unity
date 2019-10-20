using System;
using NewBark.Tilemap;
using UnityEditor;
using UnityEngine;

namespace NewBark
{
    [RequireComponent(typeof(AnimationController))]
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public AnimationController playerAnimationController;
        public AnimationController grassAnimationController;

        private void Awake()
        {
            playerAnimationController = GetComponent<AnimationController>();
        }

        public Vector2 GetFaceDirection()
        {
            return playerAnimationController.GetLastAnimationDirection();
        }

        public bool WillCollide(Vector2 direction)
        {
            return CheckCollision(direction) != null;
        }

        public Collider2D CheckCollision(Vector2 direction)
        {
            return CheckHit(direction).collider;
        }

        public RaycastHit2D CheckHit(Vector2 direction, int layerIndex = GameManager.CollisionsLayer, float distance = 1f)
        {
            return Physics2D.Raycast(
                transform.position, direction, distance, 1 << layerIndex
            );
        }

        void DrawHit(Vector2 dir)
        {
            var hit = CheckHit(dir);
            if (!hit || !hit.collider)
            {
                Debug.DrawRay(transform.position, dir, Color.green);
                Debug.DrawRay(transform.position, hit.point, Color.blue);
                return;
            }

            Debug.DrawRay(transform.position, dir, Color.red);
            Debug.DrawRay(transform.position, hit.point, Color.blue);
        }

#if UNITY_EDITOR
        void Update()
        {
            DrawHit(playerAnimationController.GetLastAnimationDirection());
        }

        void OnDrawGizmos()
        {
            var position = transform.position;
            Handles.Label(
                position + new Vector3(-4, 3),
                position.x + ", " + position.y + ", " + AreaTitleTrigger.LastTriggerTitle,
                new GUIStyle {fontSize = 8, normal = {textColor = Color.blue}}
            );
        }
#endif
    }
}
