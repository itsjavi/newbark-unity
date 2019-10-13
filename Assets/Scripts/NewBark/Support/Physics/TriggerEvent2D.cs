using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Support.Physics
{
    [Serializable]
    public class TriggerEvent2D : UnityEvent<Collider2D>
    {
    }
}
