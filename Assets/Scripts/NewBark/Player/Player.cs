using System;
using NewBark.SaveSystem;
using UnityEngine;

namespace NewBark.Player
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            LoadState();
        }

        private void OnApplicationQuit()
        {
            SaveState();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            SaveState();
        }

        public void LoadState()
        {
            var state = SaveManager.LoadPlayerState();
            if (state is null)
            {
                return;
            }

            transform.position = new Vector3(state.positionX, state.positionY, transform.position.z);
        }

        public void SaveState()
        {
            SaveManager.SavePlayerState(this);
        }
    }
}
