using System;
using NewBark.Movement;
using NewBark.State;
using NewBark.Tilemap;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark
{
    public class Player : MonoBehaviour
    {
        public AnimationController m_AnimationController;

        public bool autoSave;
        public UnityEvent onLoadState;
        public UnityEvent onBeforeLoadState;
        public UnityEvent onBeforeSaveState;
        public UnityEvent onSaveState;
        public GameData Data { get; private set; } = new GameData();

        void OnDrawGizmos()
        {
            if (!Application.isEditor)
            {
                return;
            }

            var position = transform.position;
            Handles.Label(
                position + new Vector3(-4, 3),
                position.x + ", " + position.y + ", " + AreaTitleTrigger.LastTriggerTitle,
                new GUIStyle {fontSize = 8, normal = {textColor = Color.blue}}
            );
        }

        private void Start()
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
            AreaTitleTrigger.SwitchTo(Data.areaTitleTrigger);
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
            Data.areaTitleTrigger = AreaTitleTrigger.LastTriggerName;
            Data.playerPosition = transform.position;
            Data.playerDirection = m_AnimationController.GetLastAnimationDirection();
            //
            SaveManager.Save(Data);
            onSaveState.Invoke();
        }
    }
}
