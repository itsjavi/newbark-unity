using UnityEngine;
using System.Collections;

public abstract class InputConsumer : MonoBehaviour
{
    // shouldHandle is normally true
    // if InputConsumer is registered by AlwaysNotify = true and object is not top consumer, then shouldHandle = false
    public abstract void OnUpdateHandleInput(/*bool shouldHandle*/);
}
