using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(MoveDirection dir, ActionButton button);
    public abstract void Interact(InputInfo inputInfo);
}