// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.DialogueEditor
{

    /// <summary>
    /// Draw additional information in actor, item, and location inspectors.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="asset">Asset to draw.</param>
    public delegate void DrawAssetInspectorDelegate(DialogueDatabase database, Asset asset);

    /// <summary>
    /// Draw additional information in dialogue entry inspector.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="entry">Dialogue entry to draw.</param>
    public delegate void DrawDialogueEntryInspectorDelegate(DialogueDatabase database, DialogueEntry entry);

    /// <summary>
    /// Draw additional information on a dialogue entry node in the node editor.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="entry">Dialogue entry to draw.</param>
    /// <param name="boxRect">Node boundaries.</param>
    public delegate void DrawDialogueEntryNodeDelegate(DialogueDatabase database, DialogueEntry entry, Rect boxRect);

    /// <summary>
    /// Add menu items to the Dialogue Editor's Conversation section Menu dropdown.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="menu">Menu to add items to.</param>
    public delegate void SetupGenericDialogueEditorMenuDelegate(DialogueDatabase database, GenericMenu menu);

    /// <summary>
    /// Perform additional global search.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="conversationTitle">Conversation to search. If blank, search all conversations.</param>
    /// <param name="searchText">Text to search for.</param>
    /// <param name="result">Append search results to this string.</param>
    public delegate void GlobalSearchDelegate(DialogueDatabase database, string conversationTitle, string searchText, ref string result);

    /// <summary>
    /// Perform additional global search & replace.
    /// </summary>
    /// <param name="database">Dialogue database.</param>
    /// <param name="conversationTitle">Conversation to search. If blank, search all conversations.</param>
    /// <param name="searchText">Text to search for.</param>
    /// <param name="replaceText">Replace matches with this text.</param>
    public delegate void GlobalSearchAndReplaceDelegate(DialogueDatabase database, string conversationTitle, string searchText, string replaceText);

    /// <summary>
    /// This part of the Dialogue Editor window handles the main code for 
    /// the conversation node editor.
    /// </summary>
    public partial class DialogueEditorWindow
    {

        /// <summary>
        /// Assign handler(s) to perform extra drawing in Actor, Item, and Location inspector views.
        /// </summary>
        public static event DrawAssetInspectorDelegate customDrawAssetInspector = null;

        /// <summary>
        /// Assign handler(s) to perform extra drawing in the dialogue entry inspector view.
        /// </summary>
        public static event DrawDialogueEntryInspectorDelegate customDrawDialogueEntryInspector = null;

        /// <summary>
        /// Assign handler(s) to perform extra drawing on nodes in the node editor.
        /// </summary>
        public static event DrawDialogueEntryNodeDelegate customDrawDialogueEntryNode = null;

        /// <summary>
        /// Assign handler(s) to add extra menu items to the node editor menu.
        /// </summary>
        public static event SetupGenericDialogueEditorMenuDelegate customNodeMenuSetup = null;

        /// <summary>
        /// Assign handler(s) to perform extra global search.
        /// If conversationTitle is blank, search all conversations.
        /// </summary>
        public static event GlobalSearchDelegate customGlobalSearch = null;

        /// <summary>
        /// Assign handler(s) to perform extra global search & replace.
        /// If conversationTitle is blank, search all conversations.
        /// </summary>
        public static event GlobalSearchAndReplaceDelegate customGlobalSearchAndReplace = null;

    }
}
