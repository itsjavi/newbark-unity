using UnityEngine;

public class DisableEditorOnlyRenderers : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("EditorOnly");
        foreach (var obj in objs)
        {
            Renderer r = obj.gameObject.GetComponent<Renderer>();
            if (r)
            {
                r.enabled = false;
            }
        }
    }
}
