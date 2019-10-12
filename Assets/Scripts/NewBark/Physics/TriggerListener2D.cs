using System.Linq;
using NewBark.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Physics
{
    public class TriggerListener2D : MonoBehaviour
    {
        [Tag] public string[] tagFilter;
        public UnityEvent onEnter;
        public UnityEvent onStay;
        public UnityEvent onExit;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (tagFilter.Length > 0 && !tagFilter.Any(other.tag.Equals))
            {
                return;
            }

            onEnter.Invoke();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (tagFilter.Length > 0 && !tagFilter.Any(other.tag.Equals))
            {
                return;
            }

            onStay.Invoke();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (tagFilter.Length > 0 && !tagFilter.Any(other.tag.Equals))
            {
                return;
            }

            onExit.Invoke();
        }
    }
}
