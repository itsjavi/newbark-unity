using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Support.Physics
{
    [Serializable]
    public class CollisionEvent2D : UnityEvent<Collision2D>
    {
    }
}
