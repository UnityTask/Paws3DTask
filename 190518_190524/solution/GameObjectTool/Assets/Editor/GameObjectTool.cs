using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameObjectTool : EditorWindow {
    static GameObjectTool toolWindow;

    private string addContent;
    private string removeContent;
    private string oldNameContent;
    private string newNameContent;
    private string checkContent;

    private List<string> gameObjects = new List<string>();

    [MenuItem("Window/游戏对象编辑工具")]
    static void OpenWindow() {
        toolWindow = (GameObjectTool)EditorWindow.GetWindow(typeof(GameObjectTool), false, "游戏对象编辑工具", true);
        toolWindow.minSize = new Vector2(800, 600);
        toolWindow.Show();
    }

    private void OnGUI() {
        EditorGUILayout.BeginVertical();

        Space(6);

        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 30;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = new Color(0.3443241f, 0.1342f, 0.9333f);

        EditorGUILayout.LabelField("游戏对象编辑工具", titleStyle);

        Space(6);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名称：", GUILayout.MaxWidth(30));
        addContent = EditorGUILayout.TextField(addContent, GUILayout.MaxWidth(200));
        if (GUILayout.Button("添加游戏对象", GUILayout.MaxWidth(100))) {
            if (addContent?.Length > 0) {
                new GameObject(addContent);
            }
        }
        EditorGUILayout.EndHorizontal();

        Space(3);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名称：", GUILayout.MaxWidth(30));
        removeContent = EditorGUILayout.TextField(removeContent, GUILayout.MaxWidth(200));
        if (GUILayout.Button("删除游戏对象", GUILayout.MaxWidth(100))) {
            if (removeContent?.Length > 0) {
                GameObject g = GameObject.Find(removeContent);
                if (g != null) {
                    DestroyImmediate(g);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        Space(3);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("修改前名称：", GUILayout.MaxWidth(65));
        oldNameContent = EditorGUILayout.TextField(oldNameContent, GUILayout.MaxWidth(200));
        EditorGUILayout.LabelField("修改后名称：", GUILayout.MaxWidth(65));
        newNameContent = EditorGUILayout.TextField(newNameContent, GUILayout.MaxWidth(200));
        if (GUILayout.Button("重命名游戏对象", GUILayout.MaxWidth(100))) {
            if (oldNameContent?.Length > 0 && newNameContent?.Length > 0) {
                GameObject g = GameObject.Find(oldNameContent);
                if (g != null) {
                    g.transform.name = newNameContent;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        Space(3);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名称：", GUILayout.MaxWidth(30));
        checkContent = EditorGUILayout.TextField(checkContent, GUILayout.MaxWidth(200));
        if (GUILayout.Button("查询游戏对象", GUILayout.MaxWidth(100))) {
            gameObjects.Clear();
            if (checkContent?.Length > 0) {
                Transform[] items = GameObject.FindObjectsOfType<Transform>();
                foreach (Transform temp in items) {
                    if (temp.name.IndexOf(checkContent) != -1) {
                        gameObjects.Add(temp.name);
                    }
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("筛选内容：");
        foreach (var g in gameObjects) {
            EditorGUILayout.LabelField(g);
        }

        EditorGUILayout.EndHorizontal();
    }

    private void Space(int count) {
        while (count > 0) {
            EditorGUILayout.Space();
            count--;
        }
    }
}
