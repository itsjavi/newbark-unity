using System.Collections.Generic;
using UnityEngine;

namespace NewBark.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private Dictionary<string, AudioChannel> _channels;

        public AudioManager()
        {
            _channels = new Dictionary<string, AudioChannel>();
        }

        private void Awake()
        {
            foreach (var channel in GetComponentsInChildren<AudioChannel>())
            {
                _channels.Add(channel.name.ToLower(), channel);
            }
        }

        public AudioChannel GetBgmChannel()
        {
            return GetChannel("bgm");
        }

        public AudioChannel GetSfxChannel()
        {
            return GetChannel("sfx");
        }

        public AudioChannel GetChannel(string channelName)
        {
            channelName = channelName.ToLower();
            return !HasChannel(channelName) ? null : _channels[channelName];
        }

        public bool HasChannel(string channelName)
        {
            return _channels.ContainsKey(channelName);
        }
    }
}
