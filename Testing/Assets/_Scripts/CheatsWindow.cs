using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheatsWindow : EditorWindow {
    bool useCustomFeatures;
    BoundsInt bounds;

    [MenuItem("My Game/Cheats")]
    public static void ShowWindow()
    {
        GetWindow<CheatsWindow>(false, "Cheats", true);
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.cyan;
        Cheats.MuteAllSounds = EditorGUILayout.Toggle("Mute", Cheats.MuteAllSounds);
        EditorGUILayout.IntField("PlayerLives", 3);
        EditorGUILayout.TextField("P2 name", "John");


        useCustomFeatures = EditorGUILayout.BeginToggleGroup("Use custom features?", useCustomFeatures);
        EditorGUILayout.Toggle("I am a Toggle",false);
        bounds = EditorGUILayout.BoundsIntField("Bounds int",bounds,null);
        EditorGUILayout.DelayedIntField("Delayed Int", 0);
        EditorGUILayout.Foldout(true, "FUCK YOUUU");
        EditorGUILayout.EndToggleGroup();

        GUI.backgroundColor = Color.green;

        GUILayout.FlexibleSpace(); // vague and weird
        //EditorGUILayout.BeginFadeGroup(.5f);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Fuck You",GUILayout.Width(200),GUILayout.Height(20)))
        {
            Debug.Log("FUCK YOU");
        }
       // EditorGUILayout.EndFadeGroup();
    }
}

public class Cheats {
    public static bool MuteAllSounds
    {
        get { return EditorPrefs.GetBool("Mute", false); }
        set { EditorPrefs.SetBool("Mute", value); }
    }
}

