using System;
using UnityEngine;

namespace NewBark.Support
{
    [Serializable]
    public struct SerializableVector3 : IEquatable<Vector3>, IEquatable<Vector2>
    {
        public float x;
        public float y;
        public float z;

        public static implicit operator SerializableVector3(Vector3 v)
        {
            return new SerializableVector3 {x = v.x, y = v.y, z = v.z};
        }

        public static implicit operator Vector3(SerializableVector3 sv)
        {
            return new Vector3 {x = sv.x, y = sv.y, z = sv.z};
        }

        public static implicit operator SerializableVector3(Vector2 v)
        {
            return new SerializableVector3 {x = v.x, y = v.y, z = 0};
        }

        public static implicit operator Vector2(SerializableVector3 sv)
        {
            return new Vector2 {x = sv.x, y = sv.y};
        }

        public static implicit operator SerializableVector3(SerializableVector2 v)
        {
            return new SerializableVector3 {x = v.x, y = v.y, z = 0};
        }

        public static implicit operator SerializableVector2(SerializableVector3 sv)
        {
            return new SerializableVector2 {x = sv.x, y = sv.y};
        }

        public bool Equals(SerializableVector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public bool Equals(SerializableVector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public bool Equals(Vector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case Vector2 v:
                    return Equals(v);
                case Vector3 v:
                    return Equals(v);
                default:
                    return obj.GetType() == GetType() && Equals((SerializableVector3) obj);
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();
                return hashCode;
            }
        }
    }
}
