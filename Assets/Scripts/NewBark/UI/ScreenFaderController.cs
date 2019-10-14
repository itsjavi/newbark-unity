using UnityEngine;
using UnityEngine.Events;

namespace NewBark.UI
{
    [RequireComponent(typeof(Animator))]
    public class ScreenFaderController : MonoBehaviour
    {
        public Animator Animator => GetComponent<Animator>();
        public UnityEvent onFadeStart;
        public UnityEvent onFadeInStart;
        public UnityEvent onFadeOutStart;
        public UnityEvent onFadeFinish;
        public UnityEvent onFadeInFinish;
        public UnityEvent onFadeOutFinish;

        public bool IsFadedIn()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName("FadedIn");
        }

        public bool IsFadedOut()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName("FadedOut");
        }

//        private string GetCurrentClipName()
//        {
//            return Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
//        }

        public void FadeIn()
        {
//            if (!IsFadedOut())
//            {
//                Debug.Log("cannot fade in");
//                return;
//            }

            onFadeInStart.Invoke();
            onFadeStart.Invoke();
            Animator.SetTrigger("FadeInTrigger");
            //Debug.Log("FadeIn");
        }

        public void FadeOut()
        {
//            if (!IsFadedIn())
//            {
//                Debug.Log("cannot fade out");
//                return;
//            }

            onFadeOutStart.Invoke();
            onFadeStart.Invoke();
            Animator.SetTrigger("FadeOutTrigger");
            //Debug.Log("FadeOut");
        }

        public void OnFadeInAnimationComplete()
        {
            onFadeInFinish.Invoke();
            onFadeFinish.Invoke();
            //Debug.Log("OnFadeInAnimationComplete");
        }

        public void OnFadeOutAnimationComplete()
        {
            onFadeOutFinish.Invoke();
            onFadeFinish.Invoke();
            //Debug.Log("OnFadeOutAnimationComplete");
        }
    }
}
