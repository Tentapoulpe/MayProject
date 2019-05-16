using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [Header("Quizz")]
    public GameObject g_quizz;
    public Text t_question;
    public List<Scriptable_Quizz> scriptable_quizz_list = new List<Scriptable_Quizz>();
    private List<int> i_quizz_already_asked = new List<int>();
    private int i_current_quizz;


    [Header("Video")]
    public GameObject g_video;

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

    public void ChangeMenu()
    {
        Transform cTrs = EventSystem.current.currentSelectedGameObject.transform;
        DisableCurrentButton(cTrs.GetSiblingIndex());

        g_menu_list[i_current_menu_idx].SetActive(false);
        g_menu_list[cTrs.GetSiblingIndex()].SetActive(true);
        i_current_menu_idx = cTrs.GetSiblingIndex();

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

    //QUIZZ

    public void DisplayQuizz()
    {
        g_quizz.SetActive(true);
    }

    public void HideQuizz()
    {
        g_quizz.SetActive(false);
    }

    public void PopulateQuizz()
    {
        if (i_quizz_already_asked.Count != scriptable_quizz_list.Count)
        {
            i_current_quizz = UnityEngine.Random.Range(0, scriptable_quizz_list.Count);

            while (i_quizz_already_asked.Contains(i_current_quizz))
            {
                i_current_quizz = UnityEngine.Random.Range(0, scriptable_quizz_list.Count);
            }

            i_quizz_already_asked.Add(i_current_quizz);

            t_question.text = scriptable_quizz_list[i_current_quizz].s_question;
        }
        else if(i_quizz_already_asked.Count == scriptable_quizz_list.Count)
        {
            Debug.Log("Restart");
            i_quizz_already_asked.Clear();
            PopulateQuizz();
        }
    }

    public void OnPickAnswer()
    {
        Transform cTrs = EventSystem.current.currentSelectedGameObject.transform;
        if (cTrs.GetSiblingIndex() == scriptable_quizz_list[i_current_quizz].i_idx_answer)
        {
            HideQuizz();
            LaunchVideo();
        }
        else if (cTrs.GetSiblingIndex() != scriptable_quizz_list[i_current_quizz].i_idx_answer)
        {
            PopulateQuizz();
        }
    }

    public void LaunchVideo()
    {
        Debug.Log("Vidéo");
        Script_Game_Manager.Instance.ResetTimerInactivity();
        DisplayVideo();
    }

    //VIDEO

    public void DisplayVideo()
    {
        g_video.SetActive(true);
    }

    public void HideVideo()
    {
        g_video.SetActive(false);
    }
}
