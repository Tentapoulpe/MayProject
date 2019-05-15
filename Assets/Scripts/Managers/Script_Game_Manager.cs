using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Game_Manager : MonoBehaviour
{
    public static Script_Game_Manager Instance { private set; get; }

    public AudioSource s_audio_source;
    public AudioClip s_audio_test;

    private void Start()
    {
        Screen.fullScreen = true;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                s_audio_source.PlayOneShot(s_audio_test);
            }
        }
    }

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

    public void Quit()
    {
        Application.Quit();
    }

}
