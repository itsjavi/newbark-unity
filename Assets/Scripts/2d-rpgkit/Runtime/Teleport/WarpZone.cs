using UnityEngine;

public class WarpZone: MonoBehaviour
{
    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public DirectionButton moveDirection = DirectionButton.NONE;
    public int moveSteps;
}
