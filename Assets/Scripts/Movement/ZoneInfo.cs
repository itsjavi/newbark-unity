using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ZoneInfo : MonoBehaviour
{
    public string zoneName;

    public bool popZoneNameOnEnter;

    public UnityEvent onLeave;

    public UnityEvent onEnter;
}
