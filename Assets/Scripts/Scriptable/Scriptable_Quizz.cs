using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Quizz", menuName = "Quizz", order = 1)]
public class Scriptable_Quizz : ScriptableObject
{
    public string s_question;
    [Range(0,1)]
    [Tooltip("0 for true and 1 for false")]
    public int i_idx_answer;
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Scriptable_Quizz))]
public class Scriptable_Quizz_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("0 for true and 1 for false", MessageType.Info);
        serializedObject.ApplyModifiedProperties();
    }
    
}
#endif
