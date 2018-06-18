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
            var r = obj.gameObject.GetComponent<Renderer>();
            if (r)
            {
                r.enabled = false;
            }
        }
    }
}
