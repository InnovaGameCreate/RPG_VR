using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEdit : Editor
{
    SerializedProperty itemsProp;
    int currentIndex;

    private void OnEnable()
    {
        itemsProp = serializedObject.FindProperty("reward");
    }


    public override void OnInspectorGUI()
    { 

        Quest obj = target as Quest;

        obj.questType = (Quest.QuestType)EditorGUILayout.EnumPopup("QuestType", obj.questType);
        obj.questName = EditorGUILayout.TextField("クエスト名" , obj.questName);
        //共通の注記 

        EditorGUI.indentLevel++;    //インデントを入れる

        switch (obj.questType)
        {
            case Quest.QuestType.FIGHT:
                EditorGUILayout.HelpBox("対象モンスターにはプレハブから\nドラッグアンドドロップで当てはめる\n討伐クエストの場合、対象モンスターの討伐数", MessageType.Info, true);
  
                obj.target = (GameObject)EditorGUILayout.ObjectField("討伐モンスター", obj.target, typeof(GameObject), false);
                obj.CLEARNUM = EditorGUILayout.IntField("討伐数", obj.CLEARNUM);

                break;

            case Quest.QuestType.COLLECT:
                EditorGUILayout.HelpBox("対象アイテムにはプレハブから\nドラッグアンドドロップで当てはめる\n採集クエストの場合、対象アイテムの採集数", MessageType.Info, true);

                obj.target = (GameObject)EditorGUILayout.ObjectField("採集アイテム", obj.target, typeof(GameObject), false);
                obj.CLEARNUM = EditorGUILayout.IntField("採集数", obj.CLEARNUM);

                break;
        }
        // obj.target = EditorGUILayout.PropertyField(prop, new GUIContent( “array1” ), true);

        EditorGUI.indentLevel--;    //インデントを戻す

        //obj.iconImage = EditorGUILayout.TextField("Icon Image", obj.iconImage); //アイコン画像

        EditorUtility.SetDirty(target);


        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField("");
        EditorGUILayout.HelpBox("Size＝報酬アイテムの種類数\nItem＝報酬アイテムはプレハブから\nドラッグアンドドロップで当てはめる\nItem Num＝アイテムの個数", MessageType.Info, true);
        EditorGUILayout.PropertyField(itemsProp, new GUIContent("報酬"), true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}