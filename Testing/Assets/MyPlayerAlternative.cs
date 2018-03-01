using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyPlayerAlternative : MonoBehaviour {
    [SerializeField] int damage;
    [SerializeField] int armor;
    [SerializeField] GameObject gun;
    public static bool MuteAllSounds
    {
        get { return EditorPrefs.GetBool("Mute", false); }
        set { EditorPrefs.SetBool("Mute", value); }
    }

}

[CustomEditor(typeof(MyPlayerAlternative))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor
{
    SerializedProperty damageProp;
    SerializedProperty armorProp;
    SerializedProperty gunProp;

    private void OnEnable()
    {
        damageProp = serializedObject.FindProperty("damage");
        armorProp = serializedObject.FindProperty("armor");
        gunProp = serializedObject.FindProperty("gun");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // do this at the beginning

        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));

        if (!damageProp.hasMultipleDifferentValues)
            ProgressBar(damageProp.intValue / 100.0f, "Damage");

        EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));

        if (!armorProp.hasMultipleDifferentValues)
            ProgressBar(armorProp.intValue / 100.0f, "Armor");

        EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun"));

        serializedObject.ApplyModifiedProperties(); // emd wit hthis

    }

    void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}