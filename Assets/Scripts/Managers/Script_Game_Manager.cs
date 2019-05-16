﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Script_Game_Manager : MonoBehaviour
{
    public static Script_Game_Manager Instance { private set; get; }

    [Header("EcranVeille")]
    public bool b_can_block = true;
    public float f_set_timer;
    private float f_current_timer;

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

    private void Start()
    {
        ResetTimerInactivity();
        Screen.fullScreen = true;
    }

    public void ResetTimerInactivity()
    {
        Debug.Log("ResetQuizzTimer");
        f_current_timer = f_set_timer;
        b_can_block = true;
    }

    void Update()
    {
        if(b_can_block)
        {
            f_current_timer -= Time.deltaTime;
            if(f_current_timer <= 0)
            {
                b_can_block = false;
                f_current_timer = f_set_timer;
                Script_UI_Manager.Instance.VerifyScreen();
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Back");
            }
        }

        if(Input.anyKey && b_can_block)
        {
            ResetTimerInactivity();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndVideo()
    {
        Script_UI_Manager.Instance.HideVideo();
        b_can_block = true;
    }
}
