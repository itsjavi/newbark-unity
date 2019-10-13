using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewBark.Audio
{
    public class AudioController : MonoBehaviour
    {
        public float bgmVolume = 0.075f;
        public float sfxVolume = 0.35f;

        [Tooltip("Fade-out transition time when switching BGM audio clip.")]
        public float bgmTransitionTime = 1.2f;

        private bool _loaded;

        private Dictionary<AudioChannel, AudioSource> _channels;
        public AudioSource BgmChannel => _channels[AudioChannel.BGM];
        public AudioSource SfxChannel => _channels[AudioChannel.SFX];

        private void OnValidate()
        {
            if (!_loaded)
            {
                return;
            }

            // update when changed in editor
            BgmChannel.volume = bgmVolume;
            SfxChannel.volume = sfxVolume;
        }

        private void Awake()
        {
            // GameObjects cannot be instantiated in constructors!
            // Components cannot be instantiated, only added to GameObjects!
            // This is the only way to create a dictionary with a Component:
            _channels = new Dictionary<AudioChannel, AudioSource>
            {
                {AudioChannel.BGM, new GameObject().AddComponent<AudioSource>()},
                {AudioChannel.SFX, new GameObject().AddComponent<AudioSource>()}
            };
        }

        private void Start()
        {
            BgmChannel.loop = true;
            BgmChannel.playOnAwake = false;
            BgmChannel.volume = bgmVolume;

            SfxChannel.loop = false;
            SfxChannel.playOnAwake = false;
            SfxChannel.volume = sfxVolume;

            _loaded = true;
        }

        public void PlayBgmTransition(AudioClip newClip)
        {
            if (newClip == BgmChannel.clip && BgmChannel.isPlaying)
            {
                return;
            }

            if (!BgmChannel.isPlaying || bgmTransitionTime == 0)
            {
                Play(BgmChannel, newClip);
                return;
            }

            StartCoroutine(PlayBgmTransitionCoroutine(BgmChannel, newClip, bgmTransitionTime));
        }

        public void PlayBgmWhenIdle(AudioClip newClip)
        {
            PlayWhenIdle(BgmChannel, newClip);
        }

        public void PlaySfxWhenIdle(AudioClip newClip)
        {
            PlayWhenIdle(SfxChannel, newClip);
        }

        public void PlayBgm(AudioClip newClip)
        {
            Play(BgmChannel, newClip);
        }

        public void PlaySfx(AudioClip newClip)
        {
            Play(SfxChannel, newClip);
        }

        public void PlayWhenIdle(AudioSource source, AudioClip newClip)
        {
            if (source.isPlaying)
            {
                return;
            }

            if (!source.isPlaying && source.clip != null)
            {
                source.clip = null;
                return;
            }

            Play(source, newClip);
        }

        public void Play(AudioSource source, AudioClip newClip)
        {
            if (source.clip != newClip)
            {
                source.clip = newClip;
            }

            source.Play();
        }

        public void PlayWhenIdle(AudioChannel channel, AudioClip newClip)
        {
            PlayWhenIdle(_channels[channel], newClip);
        }

        public void Play(AudioChannel channel, AudioClip newClip)
        {
            Play(_channels[channel], newClip);
        }

        private IEnumerator PlayBgmTransitionCoroutine(AudioSource source, AudioClip newClip, float fadeOutTime)
        {
            float startVolume = source.volume;

            while (source.volume > 0)
            {
                source.volume -= startVolume * Time.deltaTime / fadeOutTime;

                yield return null;
            }

            source.Stop();
            source.volume = startVolume;
            Play(source, newClip);
        }
    }
}
