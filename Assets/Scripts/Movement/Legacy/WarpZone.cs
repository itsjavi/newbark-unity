using Movement;
using Movement.GridLocation;
using UnityEngine;
using UnityEngine.Serialization;

public class WarpZone : MonoBehaviour
{
    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public GridRelativeRoute postDropMove;
}