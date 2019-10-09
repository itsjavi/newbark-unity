using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(DirectionButton dir, ActionButton action);
}
