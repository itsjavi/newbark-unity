using System;
using UnityEngine;

namespace NewBark.Player
{
    [Serializable]
    public class PlayerState
    {
        public float positionX;
        public float positionY;

        public PlayerState(Component player)
        {
            positionX = player.transform.position.x;
            positionY = player.transform.position.y;
        }
    }
}
