using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyInfo))]
public class EnemyInsEditor : Editor
{
    private SerializedObject enemyInfo;//序列化
    //default
    private SerializedProperty enemyType;
    private SerializedProperty enemyName;
    private SerializedProperty images;
    private SerializedProperty maxHealth;
    private SerializedProperty basicIntentions;
    private SerializedProperty specialFunctions;
    private SerializedProperty defaultSkill;
    //adaptive
    private SerializedProperty minionType;
    private SerializedProperty eliteType;
    private SerializedProperty bossType;

    void OnEnable()
    {
        enemyInfo = new SerializedObject(target);

        enemyType = enemyInfo.FindProperty("enemyType");
        enemyName = enemyInfo.FindProperty("enemyName");
        images = enemyInfo.FindProperty("images");
        maxHealth = enemyInfo.FindProperty("maxHealth");
        basicIntentions = enemyInfo.FindProperty("basicIntentions");
        specialFunctions = enemyInfo.FindProperty("specialFunctions");
        defaultSkill = enemyInfo.FindProperty("defaultSkill");

        minionType = enemyInfo.FindProperty("minionType");
        eliteType = enemyInfo.FindProperty("eliteType");
        bossType = enemyInfo.FindProperty("bossType");
    }
    public override void OnInspectorGUI()
    {
        enemyInfo.Update();//更新test
        EditorGUILayout.PropertyField(enemyName);
        EditorGUILayout.PropertyField(images);
        EditorGUILayout.PropertyField(maxHealth);
        EditorGUILayout.PropertyField(defaultSkill);

        EditorGUILayout.PropertyField(enemyType);
        switch (enemyType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(minionType);
                break;
            case 1:
                EditorGUILayout.PropertyField(eliteType);
                break;
            case 2:
                EditorGUILayout.PropertyField(bossType);
                break;
        }

        EditorGUILayout.PropertyField(basicIntentions);
        EditorGUILayout.PropertyField(specialFunctions);

        enemyInfo.ApplyModifiedProperties();//应用
    }
}
