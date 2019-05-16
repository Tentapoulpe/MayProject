using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Script_Input_Field : MonoBehaviour
{
    public InputField my_input_field;
    public static string s_e_mail;
    public bool b_already_e_mail;

    void Start()
    {
        if (b_already_e_mail == true)
        {
            my_input_field.text = PlayerPrefs.GetString("e_mail");
        }
    }

    public void SaveUsername(string newName)
    {
        Debug.Log("Save mail");
        b_already_e_mail = true;
        PlayerPrefs.SetString("e_mail", newName);
    }
}
