using UnityEngine;

public static class GameObjectExtension
{
    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.TryGetComponent(typeof(T), out _);
    }
    public static T GetComponentSafe<T>(this GameObject obj) where T : Component
    {
        obj.TryGetComponent(typeof(T), out Component component);

        return component as T;
    }
}
