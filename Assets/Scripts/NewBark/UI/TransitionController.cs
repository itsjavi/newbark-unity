using System;
using System.Collections;
using UnityEngine;

namespace NewBark.UI
{
    [RequireComponent(typeof(Animator))]
    public class TransitionController : MonoBehaviour
    {
        public Animator Animator => GetComponent<Animator>();
        public GameObject messageReceiver;
        public string transitionName;
        public string transitionNamePast;
        public float animationSpeed = 1f;
        private bool _isOutIn;

        private void Start()
        {
            Animator.speed = animationSpeed;
        }

        public bool IsTransitionedIn()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName(transitionNamePast + "In");
        }

        public bool IsTransitionedOut()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName(transitionNamePast + "Out");
        }

        public void TransitionIn()
        {
            if (IsTransitionedIn())
            {
                return;
            }

            StopAllCoroutines();
            Animator.SetTrigger(transitionName + "InTrigger");
            messageReceiver.SendMessage("OnTransitionInStart", SendMessageOptions.DontRequireReceiver);
        }

        public void TransitionOut()
        {
            if (IsTransitionedOut())
            {
                return;
            }

            StopAllCoroutines();
            Animator.SetTrigger(transitionName + "OutTrigger");
            messageReceiver.SendMessage("OnTransitionOutStart",
                SendMessageOptions.DontRequireReceiver);
        }

        public void TransitionOutIn()
        {
            if (IsTransitionedOut())
            {
                return;
            }

            TransitionOut();
            _isOutIn = true;
        }

        // This method should be called by a keyframe event
        public void OnTransitionInAnimationComplete()
        {
            StartCoroutine(DelayedTransitionInComplete());
        }

        // This method should be called by a keyframe event
        public void OnTransitionOutAnimationComplete()
        {
            StartCoroutine(DelayedTransitionOutComplete());
        }

        IEnumerator DelayedTransitionInComplete()
        {
            while (!IsTransitionedIn())
            {
                yield return null;
            }

            messageReceiver.SendMessage("OnTransitionInEnd", SendMessageOptions.DontRequireReceiver);
        }

        IEnumerator DelayedTransitionOutComplete()
        {
            while (!IsTransitionedOut())
            {
                yield return null;
            }

            messageReceiver.SendMessage("OnTransitionOutEnd", SendMessageOptions.DontRequireReceiver);
            if (_isOutIn)
            {
                _isOutIn = false;
                TransitionIn();
            }
        }
    }
}
