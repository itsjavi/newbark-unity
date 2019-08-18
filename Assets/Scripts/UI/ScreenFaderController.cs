using UnityEngine;
using UnityEngine.UI;

public class ScreenFaderController : MonoBehaviour
{
    public Animator animator;
    private bool _fading;

    public bool IsTransitioning()
    {
        return !_fading || animator.GetCurrentAnimatorStateInfo(0).IsName("NoFade");
    }

    public bool IsFadedOut()
    {
        return !IsTransitioning() && animator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut");
    }

    public void FadeIn()
    {
        if (!IsFadedOut())
        {
            return;
        }

        _fading = true;
        animator.SetTrigger("FadeInTrigger");
    }

    public void FadeOut()
    {
        if (IsFadedOut())
        {
            return;
        }

        _fading = true;
        animator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeComplete()
    {
        _fading = false;
    }
}