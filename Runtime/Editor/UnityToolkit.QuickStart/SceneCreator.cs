using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneCreator
{
    // --- 新增：場景設定變數 ---
    private static string _sceneName = "S0";
    private static bool _addCamera = true;
    private static bool _addLight = true;
    private static bool _addUI = true;

    // --- 新增：場景設定 UI 區塊 ---
    public static void DrawSceneSettings()
    {
        GUILayout.Label("自動化場景建置", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        _sceneName = EditorGUILayout.TextField("場景名稱", _sceneName);

        EditorGUILayout.BeginVertical("helpbox");
        GUILayout.Label("初始物件配置", EditorStyles.miniBoldLabel);
        _addCamera = EditorGUILayout.Toggle("包含 Main Camera", _addCamera);
        _addLight = EditorGUILayout.Toggle("包含 Directional Light", _addLight);
        _addUI = EditorGUILayout.Toggle("包含 UI Canvas", _addUI);
        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("執行：建立並儲存場景", GUILayout.Height(40)))
        {
            // 【核心修正】：使用 delayCall 避開 Layout 渲染過程中的場景切換
            EditorApplication.delayCall += () => { CreateAndSaveScene(); };
        }

        EditorGUILayout.Space(10);
    }

    // --- 核心邏輯：建立場景 ---
    private static void CreateAndSaveScene()
    {
        // 1. 建立空場景
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // 2. 根據勾選建立物件
        if (_addCamera)
        {
            GameObject cam = new GameObject("Main Camera");
            cam.AddComponent<Camera>();
            cam.tag = "MainCamera";
            cam.transform.position = new Vector3(0, 1, -10);
        }

        if (_addLight)
        {
            GameObject lightObj = new GameObject("Directional Light");
            Light l = lightObj.AddComponent<Light>();
            l.type = LightType.Directional;
            lightObj.transform.rotation = Quaternion.Euler(50, -30, 0);
        }

        if (_addUI)
        {
            // 更穩定的 UI 建立方式（取代 ExecuteMenuItem）
            GameObject canvasObj = new GameObject("Canvas");
            canvasObj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        // 3. 儲存場景
        string path = $"Assets/Scenes/{_sceneName}.unity";
        if (EditorSceneManager.SaveScene(newScene, path))
        {
            AssetDatabase.Refresh();
            //  EditorUtility.DisplayDialog("成功", $"場景已建立並儲存於: {path}", "確定");
            Debug.Log($"<color=cyan>場景【{_sceneName}】已建立並儲存於: {path} 範本建立完成！</color>");
        }
    }
}