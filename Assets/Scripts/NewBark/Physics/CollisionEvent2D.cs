using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Physics
{
    [Serializable]
    public class CollisionEvent2D : UnityEvent<Collision2D>
    {
    }
}
