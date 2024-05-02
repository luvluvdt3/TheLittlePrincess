// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using System;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// Add this to the Dialogue Manager to allow it to dispatch quest state updates
    /// to QuestStateListener components on other GameObjects.
    /// </summary>
    [AddComponentMenu("")] // Added automatically by QuestStateListener.
    public class QuestStateDispatcher : MonoBehaviour
    {

        private List<QuestStateListener> m_listeners = new List<QuestStateListener>();
        public List<QuestStateListener> listeners => m_listeners;

        protected virtual void OnEnable()
        {
            SaveSystem.saveDataApplied += UpdateListeners;
        }

        protected virtual void OnDisable()
        {
            SaveSystem.saveDataApplied -= UpdateListeners;
        }

        public virtual void AddListener(QuestStateListener listener)
        {
            if (listener == null) return;
            m_listeners.Add(listener);
        }

        public virtual void RemoveListener(QuestStateListener listener)
        {
            m_listeners.Remove(listener);
        }

        private void UpdateListeners()
        {
            for (int i = 0; i < m_listeners.Count; i++)
            {
                var listener = m_listeners[i];
                if (listener == null) continue;
                listener.UpdateIndicator();
            }
        }

        public virtual void OnQuestStateChange(string questName)
        {
            for (int i = 0; i < m_listeners.Count; i++)
            {
                var listener = m_listeners[i];
                if (listener == null) continue;
                if (string.Equals(questName, listener.questName))
                {
                    listener.OnChange();
                }
            }
        }

    }
}