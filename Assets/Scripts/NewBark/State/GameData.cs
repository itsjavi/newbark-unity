using System;
using System.Collections;
using NewBark.Input;
using NewBark.Physics;

namespace NewBark.State
{
    [Serializable]
    public class GameData
    {
        public static readonly int SchemaVersion = 2;
        public static readonly int MinCompatibleSchemaVersion = 2;

        public DateTime startDate = DateTime.Now;
        public DateTime saveDate = DateTime.Now;
        public float playTime;

        public Hashtable areaTitle;
        public SerializableVector2 playerPosition;
        public DirectionButton playerDirection;
    }
}
