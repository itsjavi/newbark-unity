using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;

public class WarpZone: MonoBehaviour
{
    public Transform dropZone;
    public Vector2 dropZoneOffset;
    public MovementAction postDropMove;

    public UnityEvent onLeaveArea;

    public UnityEvent onEnterArea;
}