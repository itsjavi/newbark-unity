using UnityEngine;

public abstract class Interactable : InputConsumer
{
    public abstract void Interact(ACTION_BUTTON button);
}
