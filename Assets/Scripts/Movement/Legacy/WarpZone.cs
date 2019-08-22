using Movement;
using Movement.Commands;
using UnityEngine;
using UnityEngine.Serialization;

public class WarpZone : MonoBehaviour
{
    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public WalkCommand postDropMove;
}