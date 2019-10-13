using System;
using NewBark.Audio;
using NewBark.Input;
using NewBark.State;
using NewBark.Support;
using NewBark.Tilemap;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark
{
    public class GameManager : MonoBehaviour
    {
        public static GameData Data { get; set; }
        public static PlayerController Player => Singleton<PlayerController>.Instance;
        public static InputController Input => Singleton<InputController>.Instance;
        public static AudioController Audio => Singleton<AudioController>.Instance;

        public bool autoLoad;
        public bool autoSave;
        public UnityEvent onLoadState;
        public UnityEvent onBeforeLoadState;
        public UnityEvent onBeforeSaveState;
        public UnityEvent onSaveState;

        private void Start()
        {
            LoadState();
        }

        private void Update()
        {
            Data.playTime += Time.deltaTime;
        }

        private void OnApplicationQuit()
        {
            SaveState();
        }

        private void LoadState()
        {
            if (!autoLoad)
            {
                return;
            }

            onBeforeLoadState.Invoke();
            Data = SaveManager.Load();
            if (Data is null)
            {
                Data = new GameData();
                return;
            }

            //
            Player.transform.position = Data.playerPosition;
            Player.AnimationController.UpdateAnimation(Data.playerDirection);
            AreaTitleTrigger.SwitchTo(Data.areaTitleTrigger);
            //
            onLoadState.Invoke();
        }

        private void SaveState()
        {
            if (!autoSave)
            {
                return;
            }

            onBeforeSaveState.Invoke();
            Data.saveDate = DateTime.Now;
            //
            Data.areaTitleTrigger = AreaTitleTrigger.LastTriggerName;
            Data.playerPosition = Player.transform.position;
            Data.playerDirection = Player.AnimationController.GetLastAnimationDirection();
            //
            SaveManager.Save(Data);
            onSaveState.Invoke();
        }
    }
}
