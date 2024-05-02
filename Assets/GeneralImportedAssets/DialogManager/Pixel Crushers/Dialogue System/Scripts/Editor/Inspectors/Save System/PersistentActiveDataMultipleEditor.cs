// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem
{
    [CustomEditor(typeof(PersistentActiveDataMultiple), true)]
    public class PersistentActiveDataMultipleEditor : Editor
    {
        public void OnEnable()
        {
            EditorTools.SetInitialDatabaseIfNull();
        }

        public override void OnInspectorGUI()
        {
            
            serializedObject.Update();
            EditorTools.DrawReferenceDatabase();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("checkOnStart"), true);
            EditorGUILayout.Space();
            

            SerializedProperty targetsAndConditions = serializedObject.FindProperty("targetsAndConditions");

            for (int i = 0; i < targetsAndConditions.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                SerializedProperty targetConditionPair = targetsAndConditions.GetArrayElementAtIndex(i);
                SerializedProperty target = targetConditionPair.FindPropertyRelative("target");
                SerializedProperty condition = targetConditionPair.FindPropertyRelative("condition");

                EditorGUILayout.PropertyField(target, true);
                EditorGUILayout.PropertyField(condition, true);

                EditorGUILayout.Space();

                if (GUILayout.Button("Remove Target"))
                {
                    targetsAndConditions.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Target"))
            {
                PersistentActiveDataMultiple script = (PersistentActiveDataMultiple)target;
                script.targetsAndConditions.Add(new PersistentActiveDataMultiple.TargetConditionPair());
            }

            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
