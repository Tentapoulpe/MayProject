using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Script_Save_Manager : MonoBehaviour
{
    public static Script_Save_Manager Instance { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save(string s_e_mail)
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
