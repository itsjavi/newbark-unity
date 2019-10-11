using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Physics
{
    [Serializable]
    public class TriggerEvent2D : UnityEvent<Collider2D>
    {
    }
}
