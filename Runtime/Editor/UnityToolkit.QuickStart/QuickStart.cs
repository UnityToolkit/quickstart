using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickStart : EditorWindow
{
    private int _selectedIndex = -1;
    private Vector2 _scrollPos;


    [MenuItem("Assets/Unity Toolkit/Quick Start Panel")]
    private static void SetUpFolders()
    {
        QuickStart windows = ScriptableObject.CreateInstance<QuickStart>();
        windows.position = new Rect(Screen.width / 2, Screen.height / 2, 800, 400);
        windows.titleContent = new GUIContent("Unity Toolkit - Quick Start");
        windows.Show();
    }

    private void OnGUI()
    {
        // 使用水平佈局將視窗分為左右兩半
        EditorGUILayout.BeginHorizontal();

        // --- 左側：Master (清單) ---
        DrawMasterSide();

        // 畫一條垂直線作為分隔
        Handles.color = Color.gray;
        Handles.DrawLine(new Vector2(205, 0), new Vector2(205, position.height));
        GUILayout.Space(10);

        // --- 右側：Detail (內容) ---
        DrawDetailSide();

        EditorGUILayout.EndHorizontal();
    }

    private void DrawMasterSide()
    {
        // 固定寬度 200
        EditorGUILayout.BeginVertical(GUILayout.Width(200));

        if (GUILayout.Button("專案設定 (Project Settings)"))
        {
            _selectedIndex = 1;
        }

        if (GUILayout.Button("場景設定 (Scene Settings)"))
        {
            _selectedIndex = 2;
        }

        if (GUILayout.Button("取消 (Cancel / Esc)"))
        {
            this.Close();
        }

        /*
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        EditorGUILayout.EndScrollView();
        */

        EditorGUILayout.EndVertical();
    }

    private void DrawDetailSide()
    {
        EditorGUILayout.BeginVertical();

        switch (_selectedIndex)
        {
            case 1:
                FolderCreator.DrawCreateFolder();

                break;
            case 2:
                SceneCreator.DrawSceneSettings();
                break;
        }


        EditorGUILayout.EndVertical();
    }
}