using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class FolderCreator
{
    private static string _projectName = "MyNewGame";
    private static bool _includeAudios = true;
    private static bool _includeModels = true;
    private static bool _include2D = true;

    private static bool _includeAnimation = true;
    private static bool _showPreview = true;

    public static void DrawCreateFolder()
    {
        // 標題區
        GUILayout.Label("專案結構範本設定", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        // 設定區
        _projectName = EditorGUILayout.TextField("專案根目錄名稱", _projectName);

        EditorGUILayout.BeginVertical("helpbox");
        GUILayout.Label("模組選擇", EditorStyles.miniBoldLabel);

        _include2D = EditorGUILayout.Toggle("包含2D模組 (2D)", _include2D);
        _includeModels = EditorGUILayout.Toggle("包含3D模組 (Model)", _includeModels);
        _includeAnimation = EditorGUILayout.Toggle("包含動畫模組 (Animations)", _includeAnimation);

        _includeAudios = EditorGUILayout.Toggle("包含音效模組 (Audio)", _includeAudios);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(10);

        // 階層式預覽區
        _showPreview = EditorGUILayout.BeginFoldoutHeaderGroup(_showPreview, "預覽即將建立的階層");
        if (_showPreview)
        {
            DrawPreviewHierarchy();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        GUILayout.FlexibleSpace();

        // 執行按鈕
        if (GUILayout.Button("生成資料夾結構", GUILayout.Height(40)))
        {
            GenerateFolders();
        }
    }

    private static void DrawPreviewHierarchy()
    {
        EditorGUILayout.BeginVertical("box");

        // 根目錄
        GUILayout.Label($"Assets/{_projectName}/", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;

        // 核心階層
        DrawFolderLabel("Arts");
        EditorGUI.indentLevel++;

        // 選配階層 (音效)
        if (_includeAudios)
        {
            DrawFolderLabel("Audios");
            EditorGUI.indentLevel++;
            DrawFolderLabel("BGMs");
            DrawFolderLabel("SFXs");
            EditorGUI.indentLevel--;
        }

        // 選配階層 (3D)
        if (_includeModels)
        {
            DrawFolderLabel("Models");
        }

        // 選配階層 (2D)
        if (_include2D)
        {
            DrawFolderLabel("Sprites");
            EditorGUI.indentLevel++;
            DrawFolderLabel("UIs");
            EditorGUI.indentLevel--;
        }

        // 選配階層 (動畫)
        if (_includeAnimation)
        {
            DrawFolderLabel("Animations");
        }

        EditorGUI.indentLevel--; //end Arts

        DrawFolderLabel("Scripts");
        EditorGUI.indentLevel++;
        DrawFolderLabel("Core");
        DrawFolderLabel("Player");
        DrawFolderLabel("Managers");
        EditorGUI.indentLevel--;

        DrawFolderLabel("Prefabs");
        EditorGUI.indentLevel--; //end for projectName


        DrawFolderLabel("Plugins");
        EditorGUILayout.EndVertical();
    }

    private static void DrawFolderLabel(string name)
    {
        EditorGUILayout.LabelField(EditorGUIUtility.IconContent("Folder Icon"), new GUIContent(name));
    }

    private static void GenerateFolders()
    {
        string root = Path.Combine("Assets", _projectName);
        List<string> paths = new List<string>
        {
            Path.Combine("Assets") + "/Plugins",
            root + "/Prefabs",
            root + "/Scripts/Core",
            root + "/Scripts/Player",
            root + "/Scripts/Managers",
        };

        if (_includeAudios)
        {
            paths.Add(root + "/Arts/Audios/BGMs");
            paths.Add(root + "/Arts/Audios/SFXs");
        }

        if (_includeModels)
        {
            paths.Add(root + "/Arts/Models");
        }

        if (_include2D)
        {
            paths.Add(root + "/Arts/Sprites");
            paths.Add(root + "/Arts/Sprites/UIs");
        }

        if (_includeAnimation)
        {
            paths.Add(root + "/Arts/Animations");
        }

        foreach (var path in paths)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"<color=cyan>{_projectName} 範本建立完成！</color>");
    }
}