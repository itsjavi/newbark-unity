using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int antialiasing = 0;
    public int vSync = 0;
    public int frameRate = 64;

    void Awake()
    {
        QualitySettings.antiAliasing = antialiasing;
        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = frameRate;
    }

    void Update()
    {
        Application.targetFrameRate = frameRate;
    }
}
