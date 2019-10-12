using System;
using NewBark.Movement;
using NewBark.State;
using NewBark.Tilemap;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark
{
    public class Player : MonoBehaviour
    {
        public AnimationController m_AnimationController;
        public AreaTitleController m_AreaTitleController;

        public bool autoSave;
        public UnityEvent onLoadState;
        public UnityEvent onBeforeLoadState;
        public UnityEvent onBeforeSaveState;
        public UnityEvent onSaveState;
        public GameData Data { get; private set; } = new GameData();

        private void Awake()
        {
            LoadState();
        }

        private void OnApplicationQuit()
        {
            SaveState();
        }

        private void Update()
        {
            Data.playTime += Time.deltaTime;
        }

        public void LoadState()
        {
            if (!autoSave)
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
            transform.position = Data.playerPosition;
            m_AnimationController.UpdateAnimation(Data.playerDirection);
            m_AreaTitleController.SwitchArea(AreaTitle.FromHashtable(Data.areaTitle));
            //
            onLoadState.Invoke();
        }

        public void SaveState()
        {
            if (!autoSave)
            {
                return;
            }

            onBeforeSaveState.Invoke();
            Data.saveDate = DateTime.Now;
            //
            Data.areaTitle = m_AreaTitleController.areaTitle.ToHashtable();
            Data.playerPosition = transform.position;
            Data.playerDirection = m_AnimationController.GetLastAnimationDirection();
            //
            SaveManager.Save(Data);
            onSaveState.Invoke();
        }
    }
}
