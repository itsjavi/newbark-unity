using NewBark.Tilemap;
using UnityEditor;
using UnityEngine;

namespace NewBark
{
    [RequireComponent(typeof(AnimationController2))]
    public class PlayerController : MonoBehaviour
    {
        public AnimationController2 AnimationController => GetComponent<AnimationController2>();

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
