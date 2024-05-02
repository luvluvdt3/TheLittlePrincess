// Copyright (c) Pixel Crushers. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace PixelCrushers
{

    /// <summary>
    /// Custom editor window for mass exporting text tables to CSV.
    /// </summary>
    public class TextTableMassExportImportWindow : EditorWindow
    {

        #region Menu Item

        [MenuItem("Tools/Pixel Crushers/Common/Text Table Mass Export")]
        public static void ShowWindow()
        {
            GetWindow<TextTableMassExportImportWindow>();
        }

        #endregion

        private const string PrefsKey = "PixelCrushers.TextTableMassExport";

        [Serializable]
        public class Prefs
        {
            public List<string> textTableGuids = new List<string>();
            public string csvFilename;
            public EncodingType encodingType = EncodingType.UTF8;
        }

        private Prefs prefs;
        private List<TextTable> textTables = new List<TextTable>();
        private ReorderableList textTablesList;
        private Vector2 scrollPosition = Vector2.zero;
        private string folderPath;

        private void OnEnable()
        {
            if (EditorPrefs.HasKey(PrefsKey))
            {
                prefs = JsonUtility.FromJson<Prefs>(EditorPrefs.GetString(PrefsKey));
            }
            if (prefs == null) prefs = new Prefs();

            textTables.Clear();
            foreach (var textTableGuid in prefs.textTableGuids)
            {
                if (!string.IsNullOrEmpty(textTableGuid))
                {
                    var textTable = AssetDatabase.LoadAssetAtPath<TextTable>(AssetDatabase.GUIDToAssetPath(textTableGuid));
                    if (textTable != null)
                    {
                        textTables.Add(textTable);
                    }
                }
            }
        }

        private void OnDisable()
        {
            prefs.textTableGuids.Clear();
            foreach (var textTable in textTables)
            {
                prefs.textTableGuids.Add((textTable != null) ? AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(textTable)) : string.Empty);
            }
            EditorPrefs.SetString(PrefsKey, JsonUtility.ToJson(prefs));
        }

        private void OnGUI()
        {
            try
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                if (textTablesList == null)
                {
                    textTablesList = new ReorderableList(textTables, typeof(TextTable), true, true, true, true);
                    textTablesList.drawHeaderCallback += OnDrawTextTablesListHeader;
                    textTablesList.drawElementCallback += OnDrawTextTablesListElement;
                    textTablesList.onAddCallback += OnAddTextTable;
                }
                textTablesList.DoLayoutList();
                if (GUILayout.Button("Add Folder..."))
                {
                    AddFolder();
                }
                prefs.encodingType = (EncodingType)EditorGUILayout.EnumPopup("Encoding Type", prefs.encodingType);
                EditorGUI.BeginDisabledGroup(!HasAnyTextTables());
                if (GUILayout.Button("Export to CSV..."))
                {
                    ExportToCSV();
                }
                if (GUILayout.Button("Import from CSV File..."))
                {
                    ImportFromCSVFile();
                }
                if (GUILayout.Button("Import from CSV Folder..."))
                {
                    ImportFromCSVFolder();
                }
                EditorGUI.EndDisabledGroup();
            }
            finally
            {
                EditorGUILayout.EndScrollView();
            }
        }

        private bool HasAnyTextTables()
        {
            return textTables.Find(x => x != null) != null;
        }

        private void OnDrawTextTablesListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Text Tables");
        }

        private void OnDrawTextTablesListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (!(0 <= index && index < textTables.Count)) return;
            textTables[index] = EditorGUI.ObjectField(rect, textTables[index], typeof(TextTable), true) as TextTable;
        }

        private void OnAddTextTable(ReorderableList list)
        {
            textTables.Add(null);
        }

        private void AddFolder()
        {
            var newPath = EditorUtility.OpenFolderPanel("Add Text Tables", folderPath, folderPath);
            if (!string.IsNullOrEmpty(newPath))
            {
                folderPath = newPath;
                var filenames = Directory.GetFiles(folderPath, "*.asset", SearchOption.AllDirectories);
                foreach (var filename in filenames)
                {
                    string assetPath = filename.Replace("\\", "/");
                    assetPath = "Assets/" + assetPath.Substring(Application.dataPath.Length);
                    var textTable = AssetDatabase.LoadAssetAtPath<TextTable>(assetPath);
                    if (textTable != null && !textTables.Contains(textTable))
                    {
                        textTables.Add(textTable);
                    }
                }
                if (Application.platform == RuntimePlatform.WindowsEditor) folderPath = folderPath.Replace("/", "\\");
            }
        }

        private void ExportToCSV()
        {
            string newFilename = EditorUtility.SaveFilePanel("Export to CSV", GetPath(prefs.csvFilename), prefs.csvFilename, "csv");
            if (string.IsNullOrEmpty(newFilename)) return;
            prefs.csvFilename = newFilename;
            if (Application.platform == RuntimePlatform.WindowsEditor) prefs.csvFilename = prefs.csvFilename.Replace("/", "\\");
            switch (EditorUtility.DisplayDialogComplex("Export CSV", "Export languages as columns in one file or as separate files?", "One", "Cancel", "Separate"))
            {
                case 0:
                    ExportCSV(prefs.csvFilename, false);
                    break;
                case 2:
                    ExportCSV(prefs.csvFilename, true);
                    break;
                default:
                    return;
            }
            EditorUtility.DisplayDialog("Export Complete", "The text table was exported to CSV (comma-separated values) format. ", "OK");
        }

        private void ImportFromCSVFile()
        {
            if (!EditorUtility.DisplayDialog("Import CSV?", "Importing from CSV will overwrite any existing languages or fields with the same name in the current contents. Are you sure?", "Import", "Cancel")) return;
            string newFilename = EditorUtility.OpenFilePanel("Import from CSV", GetPath(prefs.csvFilename), "csv");
            if (string.IsNullOrEmpty(newFilename)) return;
            if (!File.Exists(newFilename))
            {
                EditorUtility.DisplayDialog("Import CSV", "Can't find the file " + newFilename + ".", "OK");
                return;
            }
            try
            {
                EditorUtility.DisplayProgressBar("Importing CSV File", newFilename, 0);
                prefs.csvFilename = newFilename;
                if (Application.platform == RuntimePlatform.WindowsEditor) prefs.csvFilename = prefs.csvFilename.Replace("/", "\\");
                ImportCSVFile(prefs.csvFilename);
                if (TextTableEditorWindow.instance != null)
                {
                    var selection = Selection.activeObject;
                    Selection.activeObject = null;
                    Selection.activeObject = selection;
                }
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Import Complete", "The text tables have been updated from CSV. ", "OK");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private void ImportFromCSVFolder()
        {
            if (!EditorUtility.DisplayDialog("Import CSV?", "Importing from CSV will overwrite any existing languages or fields with the same name in the current contents. Are you sure?", "Import", "Cancel")) return;
            string newFolder = EditorUtility.OpenFolderPanel("Import from CSV Folder", GetPath(prefs.csvFilename), "csv");
            if (string.IsNullOrEmpty(newFolder)) return;
            try
            {
                EditorUtility.DisplayProgressBar("Importing CSV Files", newFolder, 0);
                var filenames = Directory.GetFiles(newFolder);
                foreach (var filename in filenames)
                {
                    if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) continue;
                    Debug.Log($"Importing {filename}");
                    ImportCSVFile(filename);
                }
                if (TextTableEditorWindow.instance != null)
                {
                    var selection = Selection.activeObject;
                    Selection.activeObject = null;
                    Selection.activeObject = selection;
                }
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Import Complete", "The text tables have been updated from CSV. ", "OK");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private string GetPath(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return string.Empty;
            try
            {
                return Path.GetDirectoryName(filename);
            }
            catch (System.ArgumentException)
            {
                return string.Empty;
            }
        }

        private List<string> GetLanguages()
        {
            var hashSet = new HashSet<string>();
            foreach (var textTable in textTables)
            {
                if (textTable == null) continue;
                foreach (var language in textTable.languages.Keys)
                {
                    hashSet.Add(language);
                }
            }
            return new List<string>(hashSet);
        }

        private void ExportCSV(string csvFilename, bool separateFiles)
        {
            var languages = GetLanguages();
            if (separateFiles)
            {
                foreach (var language in languages)
                {
                    var content = new List<List<string>>();
                    var row = new List<string>();
                    row.Add("Language");
                    row.Add(language);
                    content.Add(row);
                    foreach (var textTable in textTables)
                    {
                        if (textTable == null) continue;
                        var hasLanguage = textTable.HasLanguage(language);
                        int languageID = textTable.GetLanguageID(language);
                        foreach (var fieldKvp in textTable.fields)
                        {
                            var field = fieldKvp.Value;
                            row = new List<string>();
                            row.Add(field.fieldName);
                            var text = hasLanguage ? field.GetTextForLanguage(languageID) : "";
                            row.Add(text);
                            content.Add(row);
                        }
                    }
                    var languageFilename = csvFilename.Substring(0, csvFilename.Length - 4) + "_" + language + ".csv";
                    CSVUtility.WriteCSVFile(content, languageFilename, prefs.encodingType);
                }
            }
            else
            {
                // All in one file:
                var content = new List<List<string>>();

                // Heading rows:
                var row = new List<string>();
                content.Add(row);
                row.Add("Field");
                foreach (var language in languages)
                { 
                    row.Add(language);
                }
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;
                    // One row per field:
                    foreach (var kvp in textTable.fields)
                    {
                        var field = kvp.Value;
                        row = new List<string>();
                        content.Add(row);
                        row.Add(field.fieldName);
                        foreach (var language in languages)
                        {
                            if (textTable.HasLanguage(language))
                            {
                                var languageID = textTable.GetLanguageID(language);
                                var value = field.HasTextForLanguage(languageID) ? field.GetTextForLanguage(languageID) : "";
                                row.Add(value);
                            }
                            else
                            {
                                row.Add("");
                            }
                        }
                    }
                }
                CSVUtility.WriteCSVFile(content, csvFilename, prefs.encodingType);
            }
        }

        private void ImportCSVFile(string csvFilename)
        {
            var content = CSVUtility.ReadCSVFile(csvFilename, prefs.encodingType);
            if (content == null || content.Count < 1 || content[0].Count < 2) return;
            var fieldList = new List<string>();
            var firstCell = content[0][0];
            if (string.Equals(firstCell, "Language"))
            {
                // Single language file:
                var language = content[0][1];
                if (!string.IsNullOrEmpty(language))
                {
                    foreach (var textTable in textTables)
                    {
                        if (textTable == null) continue;
                        if (!textTable.HasLanguage(language)) textTable.AddLanguage(language);
                        for (int y = 1; y < content.Count; y++)
                        {
                            var field = content[y][0];
                            if (string.IsNullOrEmpty(field)) continue;
                            fieldList.Add(field);
                            if (textTable.HasField(field))
                            {
                                for (int x = 1; x < content[y].Count; x++)
                                {
                                    textTable.SetFieldTextForLanguage(field, language, content[y][x]);
                                }
                            }
                        }
                        textTable.ReorderFields(fieldList);
                        textTable.OnBeforeSerialize();
                        EditorUtility.SetDirty(textTable);
                    }
                }
            }
            else
            {
                // All-in-one file:
                foreach (var textTable in textTables)
                {
                    if (textTable == null) continue;
                    for (int x = 1; x < content[0].Count; x++)
                    {
                        var language = content[0][x];
                        if (string.IsNullOrEmpty(language)) continue;
                        if (!textTable.HasLanguage(language)) textTable.AddLanguage(language);
                        for (int y = 1; y < content.Count; y++)
                        {
                            var field = content[y][0];
                            if (string.IsNullOrEmpty(field)) continue;
                            if (x == 1) fieldList.Add(field);
                            if (textTable.HasField(field))
                            {
                                if ((0 <= y && y < content.Count) && (0 <= x && x < content[y].Count))
                                {
                                    textTable.SetFieldTextForLanguage(field, language, content[y][x]);
                                }
                            }
                        }
                    }
                    textTable.ReorderFields(fieldList);
                    textTable.OnBeforeSerialize();
                    EditorUtility.SetDirty(textTable);
                }
            }
        }

    }
}
