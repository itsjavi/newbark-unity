using UnityEngine;

namespace NewBark
{
    public class DontDestroyList : MonoBehaviour
    {
        public GameObject[] objects;
        private void Awake()
        {
            foreach (var obj in objects)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
}