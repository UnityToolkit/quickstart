using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
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

        if (GUILayout.Button("下載忽略檔 (.gitignore)"))
        {
            DownloadGitIgnore();
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
    
    
    private void DownloadGitIgnore()
    {
        string url = "https://raw.githubusercontent.com/github/gitignore/refs/heads/main/Unity.gitignore";
        // 取得專案根目錄 (Assets 的上一層)
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string savePath = Path.Combine(projectRoot, ".gitignore");

        // 顯示進度條，增加使用者體驗，同時防止重複點擊
        EditorUtility.DisplayProgressBar("下載中", "正在從 GitHub 取得 Unity.gitignore...", 0.5f);

        try
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                // 強制等待下載完成 (在 Editor 下這是安全的)
                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    // 這裡留空，等待下載
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllText(savePath, request.downloadHandler.text);
                    AssetDatabase.Refresh();
                    Debug.Log($"<color=cyan> .gitignore 已成功存至: {savePath} </color>");
                
                    // 只有成功才跳一次視窗
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("下載成功", ".gitignore 已儲存至專案根目錄", "確定");
                }
                else
                {
                    Debug.LogError($"下載失敗: {request.error}");
                    EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("下載失敗", "請檢查網路連線或網址是否正確", "確定");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"執行錯誤: {e.Message}");
        }
        finally
        {
            // 確保進度條一定會關閉
            EditorUtility.ClearProgressBar();
        }
    }
}