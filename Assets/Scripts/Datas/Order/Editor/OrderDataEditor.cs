// OrderDataEditor.cs
using UnityEngine;
using UnityEditor;
using Data;

[CustomEditor(typeof(OrderData))]
public class OrderDataEditor : Editor
{
    private SerializedProperty requirementType;
    // 直接指名用
    private SerializedProperty weaponData;
    // スペック要求用
    private SerializedProperty weaponRequirements;
    // レア度指定用
    private SerializedProperty requiredRarity;

    private void OnEnable()
    {
        requirementType = serializedObject.FindProperty("RequirementType");
        weaponData = serializedObject.FindProperty("weaponData");
        weaponRequirements = serializedObject.FindProperty("specRequirements");
        requiredRarity = serializedObject.FindProperty("RequiredRarity");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 基本プロパティの描画
        DrawPropertiesExcluding(serializedObject, new string[] { 
            "weaponData", 
            "specRequirements", 
            "RequiredRarity" 
        });

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("依頼要求の詳細設定", EditorStyles.boldLabel);

        // 選択された要求タイプに基づいて対応するUIを表示
        switch ((Requirements)requirementType.enumValueIndex)
        {
            case Requirements.ByData:
                DrawDirectNameRequirement();
                break;

            case Requirements.SpecSpecifications:
                DrawSpecificationsRequirement();
                break;

            case Requirements.Rarity:
                DrawRarityRequirement();
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    // 直接指名の設定UI
    private void DrawDirectNameRequirement()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.HelpBox("武器の名前を直接指定してください", MessageType.Info);
        
        EditorGUILayout.PropertyField(weaponData, new GUIContent("武器の名前"));
        
        EditorGUILayout.EndVertical();
    }

    // スペック要求の設定UI
    private void DrawSpecificationsRequirement()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.HelpBox("チェックを入れた項目について、指定値より大きい必要があります", MessageType.Info);

        // 長さの要求
        var checkLength = weaponRequirements.FindPropertyRelative("checkLength");
        EditorGUILayout.BeginHorizontal();
        checkLength.boolValue = EditorGUILayout.ToggleLeft("長さ", checkLength.boolValue, GUILayout.Width(100));
        if (checkLength.boolValue)
        {
            var requiredLength = weaponRequirements.FindPropertyRelative("requiredLength");
            EditorGUILayout.PropertyField(requiredLength, GUIContent.none);
        }
        EditorGUILayout.EndHorizontal();

        // 重さの要求
        var checkWeight = weaponRequirements.FindPropertyRelative("checkWeight");
        EditorGUILayout.BeginHorizontal();
        checkWeight.boolValue = EditorGUILayout.ToggleLeft("重さ", checkWeight.boolValue, GUILayout.Width(100));
        if (checkWeight.boolValue)
        {
            var requiredWeight = weaponRequirements.FindPropertyRelative("requiredWeight");
            EditorGUILayout.PropertyField(requiredWeight, GUIContent.none);
        }
        EditorGUILayout.EndHorizontal();

        // 鋭さの要求
        var checkSharpness = weaponRequirements.FindPropertyRelative("checkSharpness");
        EditorGUILayout.BeginHorizontal();
        checkSharpness.boolValue = EditorGUILayout.ToggleLeft("鋭さ", checkSharpness.boolValue, GUILayout.Width(100));
        if (checkSharpness.boolValue)
        {
            var requiredSharpness = weaponRequirements.FindPropertyRelative("requiredSharpness");
            EditorGUILayout.PropertyField(requiredSharpness, GUIContent.none);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    // レア度要求の設定UI
    private void DrawRarityRequirement()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.HelpBox("必要なレア度を指定してください", MessageType.Info);
        
        EditorGUILayout.PropertyField(requiredRarity, new GUIContent("必要レア度"));
        
        EditorGUILayout.EndVertical();
    }
}