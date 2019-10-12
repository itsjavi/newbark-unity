using UnityEngine;

namespace NewBark.Tilemap
{
    [CreateAssetMenu(fileName = "Untitled_AreaTitle", menuName = "NewBark/Area Title")]
    public class AreaTitle : ScriptableObject
    {
        public string title;
        public string subtitle;
        public AudioClip music;
    }
}
