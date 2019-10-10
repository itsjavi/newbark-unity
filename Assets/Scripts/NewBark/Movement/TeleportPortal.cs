using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class TeleportPortal: MonoBehaviour
    {
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        public DirectionButton moveDirection = DirectionButton.NONE;
        public int moveSteps;
    }
}
