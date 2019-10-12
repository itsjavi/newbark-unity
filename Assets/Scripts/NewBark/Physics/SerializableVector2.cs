using System;
using UnityEngine;

namespace NewBark.Physics
{
    [Serializable]
    public struct SerializableVector2 : IEquatable<Vector2>, IEquatable<Vector3>
    {
        public float x;
        public float y;

        public static implicit operator SerializableVector2(Vector2 v)
        {
            return new SerializableVector2 {x = v.x, y = v.y};
        }

        public static implicit operator Vector2(SerializableVector2 sv)
        {
            return new Vector2 {x = sv.x, y = sv.y};
        }

        public static implicit operator SerializableVector2(Vector3 v)
        {
            return new SerializableVector2 {x = v.x, y = v.y};
        }

        public static implicit operator Vector3(SerializableVector2 sv)
        {
            return new Vector3 {x = sv.x, y = sv.y};
        }

        public static implicit operator SerializableVector2(SerializableVector3 v)
        {
            return new SerializableVector2 {x = v.x, y = v.y};
        }

        public static implicit operator SerializableVector3(SerializableVector2 sv)
        {
            return new SerializableVector3 {x = sv.x, y = sv.y, z = 0};
        }

        public bool Equals(SerializableVector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public bool Equals(SerializableVector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public bool Equals(Vector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case Vector2 vector2:
                    return Equals(vector2);
                case Vector3 v:
                    return Equals(v);
                default:
                    return obj.GetType() == GetType() && Equals((SerializableVector2) obj);
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }
    }
}
