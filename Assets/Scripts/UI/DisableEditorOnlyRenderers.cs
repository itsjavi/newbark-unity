using UnityEngine;

public class DisableEditorOnlyRenderers : MonoBehaviour
{
    void Awake()
    {
        HideGameObjects(GameObject.FindGameObjectsWithTag("EditorOnly"));
        HideGameObjects(GameObject.FindGameObjectsWithTag("Invisible"));
    }

    private void HideGameObjects(GameObject[] gameObjects)
    {
        foreach (var obj in gameObjects)
        {
            if (obj.gameObject.HasComponent<Renderer>())
            {
                obj.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
