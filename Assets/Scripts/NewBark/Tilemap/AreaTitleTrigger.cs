using System;
using NewBark.Attributes;
using NewBark.Audio;
using NewBark.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace NewBark.Tilemap
{
    public class AreaTitleTrigger : MonoBehaviour
    {
        public static AreaTitleTrigger LastTrigger { get; private set; }
        public static string LastTriggerName => LastTrigger ? LastTrigger.name : null;
        public static string LastTriggerTitle => LastTrigger ? LastTrigger.areaTitle.title : null;

        public AreaTitle areaTitle;
        [Tag] public string tagFilter;
        public UnityEvent onAreaEnter;

        public static void SwitchTo(string triggerName)
        {
            if (triggerName == null)
            {
                return;
            }
            
            var trigger = GameObjectExtension.FindObjectByTypeAndName<AreaTitleTrigger>(triggerName);

            if (trigger == null)
            {
                throw new Exception("Area Title Trigger with name '" + triggerName + "' not found");
            }

            SwitchTo(trigger);
        }

        public static void SwitchTo(AreaTitleTrigger trigger)
        {
            if (trigger == null)
            {
                return;
            }
            
            if ((LastTrigger != null) && LastTrigger == trigger)
            {
                return;
            }

            LastTrigger = trigger;
            var areaTitle = trigger.areaTitle;

            if (areaTitle.music != null)
            {
                AudioChannelManager.Instance.PlayBgmTransition(areaTitle.music);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (tagFilter != null && !other.CompareTag(tagFilter))
            {
                return;
            }

            SwitchTo(this);
            onAreaEnter.Invoke();
        }
    }
}
