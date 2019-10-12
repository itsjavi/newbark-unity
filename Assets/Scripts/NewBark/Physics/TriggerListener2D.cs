using NewBark.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Physics
{
    public class TriggerListener2D : MonoBehaviour
    {
        [Tag] public string tagFilter;
        public UnityEvent onEnter;
        public UnityEvent onStay;
        public UnityEvent onExit;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (tagFilter != null && !other.CompareTag(tagFilter))
            {
                return;
            }

            onEnter.Invoke();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (tagFilter != null && !other.CompareTag(tagFilter))
            {
                return;
            }

            onStay.Invoke();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (tagFilter != null && !other.CompareTag(tagFilter))
            {
                return;
            }

            onExit.Invoke();
        }
    }
}
