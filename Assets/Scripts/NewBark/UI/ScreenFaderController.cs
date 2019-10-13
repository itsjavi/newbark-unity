using UnityEngine;
using UnityEngine.Events;

namespace NewBark.UI
{
    [RequireComponent(typeof(Animator))]
    public class ScreenFaderController : MonoBehaviour
    {
        public Animator animator;
        private bool _fading;
        public UnityEvent onFadeStart;
        public UnityEvent onFadeComplete;
    

        void Awake()
        {
            LoadAnimator();
        }

        private void LoadAnimator()
        {
            if (animator)
            {
                return;
            }

            var screenFader = GameObject.FindGameObjectWithTag("ScreenFader");

            if (!screenFader)
            {
                throw new MissingComponentException("There is no game object tagged as 'ScreenFader' in this scene.");
            }

            animator = screenFader.GetComponent<Animator>();

            if (!animator)
            {
                throw new MissingComponentException("The ScreenFader object has no 'Animator' attached.");
            }
        }
    
        public bool IsFading()
        {
            return _fading;
        }

        public bool IsTotallyFadedOut()
        {
            return !IsFading() && animator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut");
        }
    
        public bool IsFadeOut()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut");
        }
    
        public bool IsFadeIn()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn");
        }
    
        public bool IsNoFade()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("NoFade");
        }

        public void FadeIn()
        {
            if (!IsTotallyFadedOut())
            {
                return;
            }

            _fading = true;
            onFadeStart.Invoke();
            animator.SetTrigger("FadeInTrigger");
        }

        public void FadeOut()
        {
            if (IsTotallyFadedOut())
            {
                return;
            }

            _fading = true;
            onFadeStart.Invoke();
            animator.SetTrigger("FadeOutTrigger");
        }

        public void OnFadeComplete()
        {
            _fading = false;
            onFadeComplete.Invoke();
        }
    }
}
