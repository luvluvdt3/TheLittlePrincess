////////////////////////////////////////////////////////////////
////  || |\\  ||  ////                                      ////
////  || ||\\ ||  ////       Created by IntenseNation       ////
////  || || \\||  ////       ========================       ////
////  || ||  \||  ////                                      ////
////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

namespace IntenseNation.EditorAutoSave
{
    public class EditorAutoSave : EditorWindow
    {
        public string[] debugOptions = new string[] { "Full", "Necessary", "None" };

        private static int _selectedDebugOptions = 1;
        private static bool _autoSave = true;
        private static bool _backUp;
        private static bool _savePrompt;
        private static bool _countDown = true;
        private static bool _saveNotification = true;
        private static bool _versionControl;
        private static bool _versionControlLimitState;

        private static float _saveTime = 5;
        private static int _countDownTime = 5;
        private static int _versionControlLimit = 5;

        private static float _remainingSaveTime;
        private static float _frameTime;

        private static EditorCoroutine _coroutine;

        [MenuItem("Tools/Editor Auto Save")]
        public static void OpenWindow()
        {
            GetWindow<EditorAutoSave>("Editor Auto Save");
        }

        private void OnEnable()
        {
            Initialize();
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            _saveTime = EditorPrefs.GetFloat("SaveTime", _saveTime);
            _countDownTime = EditorPrefs.GetInt("CountDownTime", _countDownTime);
            _versionControlLimit = EditorPrefs.GetInt("VersionControlLimit", _versionControlLimit);
            _selectedDebugOptions = EditorPrefs.GetInt("SelectedDebugOptions", _selectedDebugOptions);
            _autoSave = EditorPrefs.GetBool("Autosave", _autoSave);
            _backUp = EditorPrefs.GetBool("BackUp", _backUp);
            _versionControl = EditorPrefs.GetBool("VersionControl", _versionControl);
            _savePrompt = EditorPrefs.GetBool("SavePrompt", _savePrompt);
            _countDown = EditorPrefs.GetBool("Countdown", _countDown);
            _saveNotification = EditorPrefs.GetBool("SaveNotification", _saveNotification);
            _versionControlLimitState = EditorPrefs.GetBool("VersionControlLimitState", _versionControlLimitState);

            _coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(AutoSaveWait());

            if (_selectedDebugOptions == 0)
            {
                Debug.Log("Loaded Editor Auto Save values successfully");
            }
        }

        private void OnGUI()
        {
            //Title
            GUILayout.Label("Editor Auto Save", EditorStyles.boldLabel);
            EditorGUILayout.Space(8);

            //Main
            GUILayout.Label("Main", EditorStyles.boldLabel);
            _autoSave = EditorGUILayout.Toggle("Enable Auto Save", _autoSave);
            _saveTime = EditorGUILayout.Slider("Save Time (In Minutes)", _saveTime, 0.1f, 120);
            EditorGUILayout.Space(10);

            //Properties
            GUILayout.Label("Properties", EditorStyles.boldLabel);
            _savePrompt = EditorGUILayout.Toggle("Display Save Prompt", _savePrompt);
            EditorGUILayout.Space(10);

            //Version Control
            GUILayout.Label("Version Control", EditorStyles.boldLabel);
            _backUp = EditorGUILayout.Toggle("Backup Before Saving", _backUp);

            if (_backUp)
            {
                _versionControl = EditorGUILayout.Toggle("Enable Version Control", _versionControl);
            }

            if (_versionControl)
            {
                _versionControlLimitState =
                    EditorGUILayout.Toggle("Enable Version Control Limit", _versionControlLimitState);
                if (_versionControlLimitState)
                {
                    _versionControlLimit =
                        EditorGUILayout.IntSlider("Version Control Limit", _versionControlLimit, 2, 100);
                }
            }

            EditorGUILayout.Space(10);

            //Notifications
            GUILayout.Label("Notifications", EditorStyles.boldLabel);
            _countDown = EditorGUILayout.Toggle("Countdown", _countDown);

            if (_countDown)
            {
                _countDownTime = EditorGUILayout.IntSlider("Countdown Time (In Seconds)", _countDownTime, 3, 20);
            }

            _saveNotification = EditorGUILayout.Toggle("Save Notification", _saveNotification);
            EditorGUILayout.Space(10);

            //Debugging
            GUILayout.Label("Debugging", EditorStyles.boldLabel);
            _selectedDebugOptions = EditorGUILayout.Popup("Debug Messages", _selectedDebugOptions, debugOptions);
            EditorGUILayout.Space(15);

            //Apply Button
            if (!GUILayout.Button("Apply")) return;
            EditorPrefs.SetFloat("SaveTime", _saveTime);
            EditorPrefs.SetInt("CountDownTime", _countDownTime);
            EditorPrefs.SetInt("SelectedDebugOptions", _selectedDebugOptions);
            EditorPrefs.SetInt("VersionControlLimit", _versionControlLimit);
            EditorPrefs.SetBool("Autosave", _autoSave);
            EditorPrefs.SetBool("BackUp", _backUp);
            EditorPrefs.SetBool("VersionControl", _versionControl);
            EditorPrefs.SetBool("SavePrompt", _savePrompt);
            EditorPrefs.SetBool("CountDown", _countDown);
            EditorPrefs.SetBool("SaveNotification", _saveNotification);
            EditorPrefs.SetBool("VersionControlLimitState", _versionControlLimitState);

            EditorCoroutineUtility.StopCoroutine(_coroutine);
            _coroutine = EditorCoroutineUtility.StartCoroutineOwnerless(AutoSaveWait());

            ShowNotification(new GUIContent("Applied new values successfully!"));

            if (_selectedDebugOptions <= 1)
            {
                Debug.Log("Applied new values successfully!");
            }
        }

        private static IEnumerator AutoSaveWait()
        {
            if (!_autoSave) yield break;
            while (true)
            {
                yield return new WaitForSecondsRealtime(1);
                _frameTime++;
                _remainingSaveTime = _saveTime * 60 - _frameTime;

                if (_remainingSaveTime <= 0)
                {
                    _frameTime = 0;
                    Save();
                }
                else if (_remainingSaveTime <= _countDownTime && _remainingSaveTime > 0 && _countDown)
                {
                    foreach (SceneView scene in SceneView.sceneViews)
                    {
                        scene.ShowNotification(new GUIContent("Auto Save in " + (int)(_remainingSaveTime)));
                    }
                }
            }
        }

        private static void Save()
        {
            if (EditorApplication.isPlaying) return;
            var saveState = true;

            if (_backUp)
            {
                var activePath = EditorSceneManager.GetActiveScene().path;
                var path = activePath.Split(char.Parse("/"));
                path[path.Length - 1] = "Backup/AutoSaveBackup_" + path[path.Length - 1];
                var folderPath = "";

                for (int i = 0; i < path.Length - 1; i++)
                {
                    folderPath += path[i];

                    if (i < path.Length - 2)
                        folderPath += "/";
                }

                if (!AssetDatabase.IsValidFolder(folderPath + "/Backup"))
                {
                    AssetDatabase.CreateFolder(folderPath, "Backup");

                    if (_selectedDebugOptions == 0)
                    {
                        Debug.Log("Backup folder didn't exist, created a new one");
                    }
                    else if (_selectedDebugOptions == 1)
                    {
                        Debug.Log(folderPath);
                    }
                }

                if (_versionControl)
                {
                    string[] locatedFiles = Directory.GetFiles(folderPath + "/Backup");
                    List<string> savedScenes = new List<string>();
                    List<int> savedScenesNumbers = new List<int>();
                    string sceneName = "";

                    for (int i = 0; i < locatedFiles.Length; i++)
                    {
                        string[] filePaths = activePath.Split(char.Parse("/"));
                        sceneName = filePaths[filePaths.Length - 1].Replace("Backup/", "").Replace(".unity", "");

                        if (!locatedFiles[i].Contains(".meta") && locatedFiles[i].Contains(sceneName))
                        {
                            savedScenes.Add(locatedFiles[i]);
                        }
                    }

                    for (int i = 0; i < savedScenes.Count; i++)
                    {
                        string[] fileNumbers = savedScenes[i].Split(char.Parse("_"));
                        string fileNumber = fileNumbers[fileNumbers.Length - 1].Replace(".unity", "");
                        savedScenesNumbers.Add(fileNumber == sceneName && i == 0 ? 0 : int.Parse(fileNumber));
                    }

                    savedScenesNumbers.Sort();
                    int scenesNumber = savedScenesNumbers.Count > 0
                        ? savedScenesNumbers[savedScenesNumbers.Count - 1]
                        : 0;

                    if (_versionControlLimitState)
                    {
                        if (scenesNumber > savedScenes.Count || scenesNumber >= _versionControlLimit)
                        {
                            if (scenesNumber > 1)
                            {
                                for (int i = 0; i < savedScenes.Count; i++)
                                {
                                    string[] scenePath = savedScenes[i].Split(char.Parse(@"\"));
                                    scenePath[scenePath.Length - 1] = scenePath[scenePath.Length - 1]
                                        .Replace(savedScenesNumbers[i].ToString(), (i + 1).ToString());
                                    AssetDatabase.RenameAsset(savedScenes[i], scenePath[scenePath.Length - 1]);
                                }

                                scenesNumber = savedScenes.Count;
                            }

                        }

                        if (savedScenes.Count > _versionControlLimit)
                        {
                            for (int i = 0; i < savedScenes.Count - _versionControlLimit; i++)
                            {
                                AssetDatabase.DeleteAsset(savedScenes[i]);
                                savedScenes.Remove(savedScenes[i]);
                            }
                        }
                    }

                    if (savedScenes.Count > 0)
                    {
                        path[path.Length - 1] = "Backup/AutoSaveBackup_" +
                                                activePath.Split(char.Parse("/"))[path.Length - 1]
                                                    .Replace(".unity", "_") +
                                                (scenesNumber + (!_versionControlLimitState ? 1 :
                                                    savedScenes.Count >= _versionControlLimit ? 0 : 1)) + ".unity";
                    }
                }

                AssetDatabase.CopyAsset(activePath, string.Join("/", path));

                if (_savePrompt)
                {
                    saveState = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }

                if (saveState)
                {
                    EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), activePath);
                }
            }
            else
            {
                EditorSceneManager.SaveOpenScenes();
            }

            if (saveState && _saveNotification)
            {
                foreach (SceneView scene in SceneView.sceneViews)
                {
                    scene.ShowNotification(new GUIContent("Scene Saved Successfully!"));
                }
            }

            if (_selectedDebugOptions == 0)
            {
                Debug.Log("Saved Open Scene");
            }
        }
    }
}