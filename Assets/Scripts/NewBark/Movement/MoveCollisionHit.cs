using UnityEngine;

namespace NewBark.Movement
{
    public class MoveCollisionHit
    {
        public Move move;
        public RaycastHit2D hit;

        public MoveCollisionHit(Move move, RaycastHit2D hit)
        {
            this.move = move;
            this.hit = hit;
        }
    }
}
