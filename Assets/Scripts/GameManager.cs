using UnityEngine;

public class GameManager : MonoBehaviour
{
    // TODO: add in-game options to scale/go to fullscreen
    public int defaultWindowWidth = 320 * 2;
    public int defaultWindowHeight = 288 * 2;

    void Awake()
    {

    }

    void Update()
    {
        if (!(Screen.width % defaultWindowWidth == 0 && Screen.height % defaultWindowHeight == 0))
        {
            //int ratio = Mathf.Max(
            //    Mathf.FloorToInt((Screen.width / defaultWindowWidth)),
            //    Mathf.FloorToInt((Screen.height / defaultWindowHeight))
            //);
            //int scaledWidth = defaultWindowWidth * ratio;
            //int scaledHeight = defaultWindowHeight * ratio;

            // force resolution
            Screen.SetResolution(defaultWindowWidth, defaultWindowHeight, false);
        }
    }
}
