using RPGKit2D.Interaction;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(InteractionContext ctx);
}
