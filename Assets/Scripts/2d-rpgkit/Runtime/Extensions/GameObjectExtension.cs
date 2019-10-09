using UnityEngine;

public static class GameObjectExtension
{
    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.TryGetComponent(typeof(T), out _);
    }
}
