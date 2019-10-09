using RPGKit2D.Input;
using UnityEngine;

namespace RPGKit2D.Movement
{
    public class TeleportPortal: MonoBehaviour
    {
        public Transform dropZone;
        public Vector2 dropZoneOffset;
        public DirectionButton moveDirection = DirectionButton.NONE;
        public int moveSteps;
    }
}
