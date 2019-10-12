using NewBark.Audio;
using UnityEngine;

namespace NewBark.Tilemap
{
    public class AreaTitleController : MonoBehaviour
    {
        [HideInInspector] public AreaTitle areaTitle;
        public AudioChannel audioChannel;

        public void SwitchArea(AreaTitle newAreaTitle)
        {
            if (areaTitle == newAreaTitle)
            {
                return;
            }

            areaTitle = newAreaTitle;

            if (newAreaTitle.music != null)
            {
                audioChannel.SwitchClip(newAreaTitle.music);
            }

            Debug.Log("Area Title: " + newAreaTitle.title + " // " + newAreaTitle.subtitle);
        }
    }
}
