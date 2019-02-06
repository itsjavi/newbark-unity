using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(DIRECTION_BUTTON dir, ACTION_BUTTON button);
}
