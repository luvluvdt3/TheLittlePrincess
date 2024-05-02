// Copyright (c) Pixel Crushers. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    [Serializable]
    public class DialogueEntrySceneEvent
    {
        public string guid = string.Empty;
        public GameObjectUnityEvent onExecute = new GameObjectUnityEvent();
    }

    /// <summary>
    /// Holds scene-specific UnityEvents referenced by a dialogue database's dialogue entries.
    /// </summary>
    [AddComponentMenu("")]
    public class DialogueSystemSceneEvents : MonoBehaviour
    {
        [HelpBox("Do not remove this GameObject. It contains UnityEvents referenced by a dialogue database. This GameObject should not be a child of the Dialogue Manager or marked as Don't Destroy On Load.", HelpBoxMessageType.Info)]
        public List<DialogueEntrySceneEvent> dialogueEntrySceneEvents = new List<DialogueEntrySceneEvent>();

        private static List<DialogueSystemSceneEvents> m_sceneInstances = new List<DialogueSystemSceneEvents>();

#if UNITY_2019_3_OR_NEWER && UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitStaticVariables()
        {
            m_sceneInstances = new List<DialogueSystemSceneEvents>();
        }
#endif

        private void Awake()
        {
            RegisterInstance();
        }

        private void Start()
        {
            RegisterInstance();
        }

        private void OnDestroy()
        {
            m_sceneInstances.Remove(this);
        }

        private void RegisterInstance()
        {
            if (!m_sceneInstances.Contains(this))
            {
                m_sceneInstances.Add(this);
            }
        }

        public static int AddNewDialogueEntrySceneEvent(out string guid, DialogueSystemSceneEvents sceneInstanceToUse = null)
        {
            guid = string.Empty;
            if (sceneInstanceToUse == null) sceneInstanceToUse = GameObjectUtility.FindFirstObjectByType<DialogueSystemSceneEvents>();
            if (sceneInstanceToUse == null) return -1;
            guid = Guid.NewGuid().ToString();
            var x = new DialogueEntrySceneEvent();
            x.guid = guid;
            sceneInstanceToUse.dialogueEntrySceneEvents.Add(x);
            return sceneInstanceToUse.dialogueEntrySceneEvents.Count - 1;
        }

        public static void RemoveDialogueEntrySceneEvent(string guid, DialogueSystemSceneEvents sceneInstanceToUse = null)
        {
            if (sceneInstanceToUse != null)
            {
                sceneInstanceToUse.dialogueEntrySceneEvents.RemoveAll(x => x.guid == guid);
            }
            else
            {
                foreach (var instance in GameObjectUtility.FindObjectsByType<DialogueSystemSceneEvents>())
                {
                    instance.dialogueEntrySceneEvents.RemoveAll(x => x.guid == guid);
                }
            }
        }

        public static DialogueEntrySceneEvent GetDialogueEntrySceneEvent(string guid)
        {
            if (!Application.isPlaying) return null;
            foreach (var sceneInstance in m_sceneInstances)
            {
                if (sceneInstance == null || sceneInstance.dialogueEntrySceneEvents == null) continue;
                var result = sceneInstance.dialogueEntrySceneEvents.Find(x => x.guid == guid);
                if (result != null) return result;
            }
            return null;
        }

        public static int GetDialogueEntrySceneEventIndex(string guid)
        {
            if (!Application.isPlaying) return -1;
            foreach (var sceneInstance in m_sceneInstances)
            {
                if (sceneInstance == null || sceneInstance.dialogueEntrySceneEvents == null) continue;
                var result = sceneInstance.dialogueEntrySceneEvents.FindIndex(x => x.guid == guid);
                if (result != -1) return result;
            }
            return -1;
        }

        public static int GetDialogueEntrySceneEventIndex(string guid, DialogueSystemSceneEvents instance)
        {
            if (instance == null) return -1;
            return instance.dialogueEntrySceneEvents.FindIndex(x => x.guid == guid);
        }

    }
}
