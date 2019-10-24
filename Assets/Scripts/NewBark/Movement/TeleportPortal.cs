using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class TeleportPortal : MonoBehaviour
    {
        public AudioClip soundEffect;
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        public int dropZoneSteps;
        public GameButton dropZoneLookAt = GameButton.None;
        [HideInInspector] public Vector2 calculatedDropZone;
        [HideInInspector] public Vector2 calculatedDropZoneLookAt;
    }
}
