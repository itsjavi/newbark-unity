using System.Collections;
using UnityEditor;
using UnityEngine;

namespace NewBark.Tilemap
{
    [CreateAssetMenu(fileName = "Untitled_AreaTitle", menuName = "NewBark/Area Title")]
    public class AreaTitle : ScriptableObject
    {
        public string title;
        public string subtitle;
        public AudioClip music;

        public static AreaTitle FromHashtable(Hashtable data)
        {
            var area = CreateInstance<AreaTitle>();

            if (data == null)
            {
                return area;
            }

            area.title = (string) data["title"];
            area.subtitle = (string) data["subtitle"];
            var musicAsset = (string) data["music"];
            area.music = AssetDatabase.LoadAssetAtPath<AudioClip>(musicAsset);

            return area;
        }

        public Hashtable ToHashtable()
        {
            var data = new Hashtable
            {
                {"title", title}, {"subtitle", subtitle}, {"music", AssetDatabase.GetAssetPath(music)}
            };
            return data;
        }
    }
}
