using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Video Settings")]
    public bool antialiasing = false;
    public bool vSync = false;
    public int fps = 60;

    void Awake()
    {
        QualitySettings.antiAliasing = antialiasing ? 1 : 0;
        QualitySettings.vSyncCount = vSync ? 1 : 0;
        Application.targetFrameRate = fps;
    }

    void Update()
    {
        Application.targetFrameRate = fps;
    }
}
