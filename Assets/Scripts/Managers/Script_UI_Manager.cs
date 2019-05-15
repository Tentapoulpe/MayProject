using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { private set; get; }

    [Header("Menu")]

    public List<GameObject> g_menu_list = new List<GameObject>();
    public List<Button> b_button_list = new List<Button>();
    private int i_current_menu_idx = 0;

    [Header("WebViewer")]

    public SampleWebView cs_web_viewer;
    private bool b_web_viewer_is_activate;

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
        
    }

    public void ChangeMenu(int i_menu_idx)
    {

        //if (g_menu_to_display != null)
        //{
        //    g_previous_menu = g_menu_to_display;
        //    g_previous_menu.SetActive(false);
        //    g_new_menu.SetActive(true);
        //    g_menu_to_display = g_new_menu;
        //}

        DisableCurrentButton(i_menu_idx);

        g_menu_list[i_current_menu_idx].SetActive(false);
        g_menu_list[i_menu_idx].SetActive(true);
        i_current_menu_idx = i_menu_idx;

        if(i_current_menu_idx == 1)
        {
            cs_web_viewer.StartWebViewer();
        }
        else if (i_current_menu_idx != 1)
        {
            cs_web_viewer.StopWebView();
        }
    }

    public void DisableCurrentButton(int i_menu_idx)
    {
        b_button_list[i_menu_idx].interactable = false;
        b_button_list[i_current_menu_idx].interactable = true;
    }

}
