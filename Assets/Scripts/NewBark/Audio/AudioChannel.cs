using System.Collections;
using UnityEditor;
using UnityEngine;

namespace NewBark.Audio
{
    public class AudioChannel : MonoBehaviour
    {
        public AudioSource m_AudioSource;
        public float m_SwitchFadeOutTime;

        public string GetClipAsset()
        {
            return AssetDatabase.GetAssetPath(m_AudioSource.clip);
        }

        public void LoadClipAsset(string path)
        {
            m_AudioSource.clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            m_AudioSource.Play();
        }

        public void SwitchClip(AudioClip newClip)
        {
            if (newClip == m_AudioSource.clip && m_AudioSource.isPlaying)
            {
                return;
            }

            if (!m_AudioSource.isPlaying || m_SwitchFadeOutTime == 0)
            {
                PlayClip(newClip);
                return;
            }

            StartCoroutine(SwitchClipCoroutine(newClip, m_SwitchFadeOutTime));
        }

        public void PlayClipIfStopped(AudioClip newClip)
        {
            if (m_AudioSource.isPlaying)
            {
                return;
            }
            m_AudioSource.clip = newClip;
            m_AudioSource.Play();
        }

        public void PlayClip(AudioClip newClip)
        {
            m_AudioSource.clip = newClip;
            m_AudioSource.Play();
        }

        private IEnumerator SwitchClipCoroutine(AudioClip newClip, float fadeOutTime)
        {
            float startVolume = m_AudioSource.volume;

            while (m_AudioSource.volume > 0)
            {
                m_AudioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;

                yield return null;
            }

            m_AudioSource.Stop();
            m_AudioSource.volume = startVolume;
            PlayClip(newClip);
        }
    }
}
