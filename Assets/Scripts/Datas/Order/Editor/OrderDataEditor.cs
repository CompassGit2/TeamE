using UnityEngine;
using UnityEditor;
using Data;

[CustomEditor(typeof(OrderData))]
public class OrderDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update(); // 必要な更新 

        // 基本的なプロパティを描画
        DrawDefaultInspector();

        OrderData orderData = (OrderData)target;

        // RequirementTypeのプロパティを描画(既に描画済み)
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("RequirementType"));

        // 選択されたRequirementTypeに応じて、異なるプロパティを描画
        switch (orderData.RequirementType)
        {
            case requirements.ByName:
                // Type1の場合に表示するプロパティを描画
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PropertyForByName"));
                break;
            case requirements.SpecSpecifications:
                // Type2の場合に表示するプロパティを描画
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PropertyForSpecSpecifications"));
                break;
            case requirements.Rarity:
                // Type3の場合に表示するプロパティを描画
                EditorGUILayout.PropertyField(serializedObject.FindProperty("PropertyForRarity"));
                break;
            // ... 他のケースも同様
            default:
                break;
        }
        serializedObject.ApplyModifiedProperties(); // 変更を適用
    }
}