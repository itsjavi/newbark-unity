using NewBark.Movement;
using NewBark.Tilemap;
using UnityEditor;
using UnityEngine;

namespace NewBark
{
    public class PlayerController : MonoBehaviour
    {
        public AnimationController AnimationController => GetComponent<AnimationController>();

#if UNITY_EDITOR
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
