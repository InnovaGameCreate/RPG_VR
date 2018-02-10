using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEdit : Editor
{

    public override void OnInspectorGUI()
    {
        Quest obj = target as Quest;

        obj.questType = (Quest.QuestType)EditorGUILayout.EnumPopup("QuestType", obj.questType);

        //共通の注記 

        EditorGUI.indentLevel++;    //インデントを入れる

        switch (obj.questType)
        {
            case Quest.QuestType.FIGHT:
                EditorGUILayout.HelpBox("討伐クエストの場合、対象モンスターの討伐数\n対象モンスター", MessageType.Info, true);
                obj.ClearNum = EditorGUILayout.IntField("討伐数", obj.ClearNum);
                obj.target = (GameObject)EditorGUILayout.ObjectField("討伐モンスター", obj.target, typeof(GameObject), false);
                break;

            case Quest.QuestType.COLLECT:
                EditorGUILayout.HelpBox("採集クエストの場合、対象アイテムの採集数\n対象アイテム", MessageType.Info, true);
                obj.ClearNum = EditorGUILayout.IntField("採集数", obj.ClearNum);
                obj.target = (GameObject)EditorGUILayout.ObjectField("採集アイテム", obj.target, typeof(GameObject), false);

                break;
        }

        EditorGUI.indentLevel--;    //インデントを戻す

        //obj.iconImage = EditorGUILayout.TextField("Icon Image", obj.iconImage); //アイコン画像

        EditorUtility.SetDirty(target);
    }
}