using UnityEngine;

namespace RPGKit2D.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract void Interact(InteractionContext ctx);
    }
}
