using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PixelCrushers.DialogueSystem.DialogueEditor;

namespace PixelCrushers.DialogueSystem
{


    public class EditModePlayerWindow : EditorWindow
    {
        public static void Open(DialogueDatabase database, int conversationID, int startingEntryID = 0)
        {
            var window = GetWindow<EditModePlayerWindow>(false, "Conversation");
            window.minSize = new Vector2(340, 128);
            window.StartConversation(database, conversationID, startingEntryID);
        }

        private DialogueDatabase database;
        private List<int> playerActorIDs;
        private Dictionary<int, string> actorNames;
        private DialogueEntry currentEntry = null;
        private string speaker;
        private string subtitleText;
        private string userScript;
        private string sequence;
        private List<DialogueEntry> linkedEntries;
        private List<string> linkedEntryButtonTexts;
        private Vector2 scrollPosition = Vector2.zero;

        private static GUIContent EditLabel = new GUIContent("Edit", "Edit this dialogue entry.");

        private void StartConversation(DialogueDatabase database, int conversationID, int startingEntryID)
        {
            if (database == null)
            {
                Debug.LogError("Dialogue System Edit Mode Player: No database specified.");
                Close();
                return;
            }
            this.database = database;
            playerActorIDs = new List<int>();
            actorNames = new Dictionary<int, string>();
            foreach (var actor in database.actors)
            {
                if (actor.IsPlayer) playerActorIDs.Add(actor.id);
                actorNames[actor.id] = actor.Name;
            }
            var entry = database.GetDialogueEntry(conversationID, startingEntryID);
            if (entry == null)
            {
                Debug.LogError($"Dialogue System Edit Mode Player: Database {database} conversation [{conversationID}] doesn't have a dialogue entry [{startingEntryID}].", database);
                Close();
                return;
            }
            GotoEntry(entry);
        }

        private void GotoEntry(DialogueEntry entry)
        {
            currentEntry = entry;
            var actor = database.GetActor(entry.ActorID);
            speaker = (entry.id == 0) ? "START" : ((actor != null) ? actor.Name : "(No Actor)");
            subtitleText = entry.subtitleText;
            if (string.IsNullOrEmpty(subtitleText)) subtitleText = entry.Title;
            userScript = entry.userScript;
            sequence = entry.Sequence;
            linkedEntries = new List<DialogueEntry>();
            linkedEntryButtonTexts = new List<string>();
            foreach (var link in entry.outgoingLinks)
            {
                var linkedEntry = database.GetDialogueEntry(link);
                linkedEntries.Add(linkedEntry);
                var isPlayerLine = playerActorIDs.Contains(linkedEntry.ActorID);
                var buttonText = actorNames[linkedEntry.ActorID] + ": " + (isPlayerLine ? linkedEntry.responseButtonText : linkedEntry.subtitleText);
                var tooltip = linkedEntry.conditionsString;
                if (!string.IsNullOrEmpty(tooltip)) buttonText += $"\n[{linkedEntry.conditionsString}]";
                linkedEntryButtonTexts.Add(buttonText);
            }
        }

        void OnGUI()
        {
            if (currentEntry == null)
            {
                GUILayout.Label("Not playing a conversation.");
                return;
            }
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            try
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(speaker + ":");
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(EditLabel))
                {
                    DialogueEditorWindow.OpenDialogueEntry(database, currentEntry.conversationID, currentEntry.id);
                }
                GUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextArea(subtitleText);
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.LabelField("Sequence:");
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextArea(string.IsNullOrEmpty(sequence) ? "Default Sequence" : sequence);
                EditorGUI.EndDisabledGroup();
                if (!string.IsNullOrEmpty(userScript))
                {
                    EditorGUILayout.LabelField("Script:");
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.TextArea(userScript);
                    EditorGUI.EndDisabledGroup();
                }
                for (int i = 0; i < linkedEntries.Count; i++)
                {
                    if (GUILayout.Button(linkedEntryButtonTexts[i]))
                    {
                        GotoEntry(linkedEntries[i]);
                    }
                }
                if (linkedEntries.Count == 0)
                {
                    if (GUILayout.Button("[END]"))
                    {
                        Close();
                    }
                }
            }
            finally
            {
                EditorGUILayout.EndScrollView();
            }
        }

    }
}
