using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Script_Input_Field : MonoBehaviour
{
    public InputField my_input_field;
    private string s_e_mail;

    public void SaveEmail(string mail_adress)
    {
        Debug.Log("Save mail");
        s_e_mail = mail_adress;
        Debug.Log(s_e_mail);
        Save();
        //PlayerPrefs.SetString("e_mail", mail_adress);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.e_mail = s_e_mail;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]

class PlayerData
{
    public string e_mail;
}
   
