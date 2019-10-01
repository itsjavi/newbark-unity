using UnityEngine;

public abstract class Interactable : InputConsumer
{
    // todo: remove DIRECTION_BUTTON dir
    public abstract void Interact(DIRECTION_BUTTON dir, ACTION_BUTTON button);
}
