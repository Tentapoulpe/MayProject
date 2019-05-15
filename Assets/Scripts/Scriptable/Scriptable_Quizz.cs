using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quizz", menuName = "Quizz", order = 1)]
public class Scriptable_Quizz : ScriptableObject
{
    public string s_question;
    [Range(0,1)]
    public int i_idx_answer; 
}
