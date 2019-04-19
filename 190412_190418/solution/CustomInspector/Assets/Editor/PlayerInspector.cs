using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor {
    Player player;
    bool showFlag;

    private void OnEnable()
    {
        player = (Player)target;
        showFlag = false;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 30;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.normal.textColor = new Color(0.34242434f, 0.123123f, 0.933434f);

        EditorGUILayout.LabelField("玩家信息", titleStyle);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(true);
        player.id = EditorGUILayout.IntField("玩家ID", player.id);
        EditorGUI.EndDisabledGroup();

        player.playerName = EditorGUILayout.TextField("玩家姓名", player.playerName);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("故事背景");
        EditorGUILayout.TextArea(player.description, GUILayout.MinHeight(100));

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        player.HP = EditorGUILayout.Slider("血量", player.HP, 0, 255);
        if (player.HP < 20)
        {
            GUI.color = Color.red;
        }
        else if (player.HP > 150) {
            GUI.color = Color.green;
        } else
        {
            GUI.color = Color.yellow;
        }

        Rect progressRect = GUILayoutUtility.GetRect(50, 50);
        EditorGUI.ProgressBar(progressRect, player.HP / 255.0f, "血量");

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUI.color = Color.white;

        if (GUILayout.Button("回血")) {
            player.HPRecovered();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        player.damage = EditorGUILayout.Slider("伤害", player.damage, 0, 20);
        if (player.damage < 10)
        {
            EditorGUILayout.HelpBox("武器需要清理", MessageType.Error);
        }
        else if (player.damage > 15) {
            EditorGUILayout.HelpBox("崭新武器", MessageType.Warning);
        } else
        {
            EditorGUILayout.HelpBox("正常武器", MessageType.Info);
        }

        showFlag = EditorGUILayout.Foldout(showFlag, "附加信息");
        if(showFlag)
        {
            player.playerPosition = EditorGUILayout.Vector3Field(
            "玩家位置坐标", player.playerPosition);
            player.playerWeight = EditorGUILayout.FloatField("玩家体重",
                player.playerWeight);
        }


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("名字", GUILayout.MaxWidth(50));
        player.petName = EditorGUILayout.TextField(player.petName);
        EditorGUILayout.LabelField("年龄", GUILayout.MaxWidth(50));
        player.petAge = EditorGUILayout.IntField(player.petAge);
        EditorGUILayout.LabelField("类型", GUILayout.MaxWidth(50));
        player.petType = EditorGUILayout.TextField(player.petType);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
