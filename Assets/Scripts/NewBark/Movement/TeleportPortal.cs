using NewBark.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewBark.Movement
{
    public class TeleportPortal : MonoBehaviour
    {
        public AudioClip soundEffect;
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        [FormerlySerializedAs("moveSteps")] public int dropZoneSteps;
        public GameButton dropZoneLookAt = GameButton.None;
        [HideInInspector] public Vector2 calculatedDropZone;
        [HideInInspector] public Vector2 calculatedDropZoneLookAt;
    }
}