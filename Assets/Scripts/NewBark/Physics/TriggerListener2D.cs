using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Physics
{
    public class TriggerListener2D: MonoBehaviour
    {
        public UnityEvent onEnter;
        public UnityEvent onStay;
        public UnityEvent onExit;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            onEnter.Invoke();
        }

        void OnTriggerStay2D(Collider2D other)
        {
            onStay.Invoke();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            onExit.Invoke();
        }
    }
}
