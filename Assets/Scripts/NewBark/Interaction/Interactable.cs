using UnityEngine;

namespace NewBark.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(InteractionContext ctx);
    }
}
