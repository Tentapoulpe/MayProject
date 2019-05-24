using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Script_UI_Manager : MonoBehaviour
{
    public static Script_UI_Manager Instance { private set; get; }

    [Header("Menu")]

    public List<GameObject> g_menu_list = new List<GameObject>();
    public List<Button> b_button_list = new List<Button>();
    private int i_current_menu_idx;

    [Header("WebViewer")]

    public SampleWebView cs_web_viewer;
    private bool b_web_viewer_is_activate;

    [Header("Quizz")]
    public GameObject g_quizz;
    public Text t_question;
    public List<Scriptable_Quizz> scriptable_quizz_list = new List<Scriptable_Quizz>();
    private List<int> i_quizz_already_asked = new List<int>();
    private int i_current_quizz;
    private bool b_ok_menu_quizz = false;

    [Header("Video")]
    public GameObject g_video;

    [Header("Interactive Video")]
    public VideoPlayer my_video_player;
    public List<GameObject> buttons_video_player = new List<GameObject>();
    public List<Scriptable_Interactive_Video> scriptable_video = new List<Scriptable_Interactive_Video>();
    private int i_current_scriptable_idx=0;
    private bool b_can_show_buttons;

    [Header ("Contacts")]
    public InputField my_input_field;
    public GameObject text_send;
    public float f_delay_text_display;
    private string s_e_mail;

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
        ChangeMenu(0);
    }

    IEnumerator WaitTime(float f_waiting_time)
    {
        yield return new WaitForSeconds(f_waiting_time);
        my_input_field.GetComponentInChildren<Text>().text = "@";
    }

    #region Menu Manager

    public void SelectMenu()
    {
        Transform cTrs = EventSystem.current.currentSelectedGameObject.transform;
        ChangeMenu(cTrs.GetSiblingIndex());
    }

    public void ChangeMenu(int menu_idx)
    {
        DisableCurrentButton(menu_idx);

        g_menu_list[i_current_menu_idx].SetActive(false);
        g_menu_list[menu_idx].SetActive(true);
        i_current_menu_idx = menu_idx;

        if (menu_idx == 0)
        {
            cs_web_viewer.StartWebViewer();
        }
        if (menu_idx != 0)
        {
            cs_web_viewer.StopWebView();
        }
        if (menu_idx == 1)
        {
            DisableInteractiveVideoButton();
            PlayInteractiveVideo(scriptable_video[0].video_to_play[0]);
            i_current_scriptable_idx = 1;
        }
        VerifyScreen();
    }

    public void DisableCurrentButton(int i_menu_idx)
    {
        b_button_list[i_menu_idx].interactable = false;
        b_button_list[i_current_menu_idx].interactable = true;
    }
    #endregion

    #region Interactive Video
    public void PlayInteractiveVideo(VideoClip video)//Jouer une video
    {
        my_video_player.clip = video;
        my_video_player.Play();
        my_video_player.loopPointReached += UpdateButtonVideo;
    }

    public void UpdateButtonVideo(UnityEngine.Video.VideoPlayer vp)//Mettre à jour les boutons correspondant aux videos rensignées dans le scriptable
    {
        for (int i = 0; i < buttons_video_player.Count; i++)
        {
            if (scriptable_video[i_current_scriptable_idx].video_to_play[i] != null)
            {
                buttons_video_player[i].SetActive(true);
            }
            else
            {
                buttons_video_player[i].SetActive(false);
            }
        }
        b_can_show_buttons = true;
        my_video_player.loopPointReached -= UpdateButtonVideo;
    }

    public void SelectVideo()//Mettre à jour le current scriptable et envoyer la vidéo correspondante au bouton selectionné
    {
        if(b_can_show_buttons)
        {
            Transform cTrs = EventSystem.current.currentSelectedGameObject.transform;
            if (cTrs.GetSiblingIndex() == 0)//Button Top
            {
                PlayInteractiveVideo(scriptable_video[i_current_scriptable_idx].video_to_play[cTrs.GetSiblingIndex()]);
                i_current_scriptable_idx = scriptable_video[i_current_scriptable_idx].i_top_button_idx;
            }

            if (cTrs.GetSiblingIndex() == 1)//Button Bot
            {
                PlayInteractiveVideo(scriptable_video[i_current_scriptable_idx].video_to_play[cTrs.GetSiblingIndex()]);
                i_current_scriptable_idx = scriptable_video[i_current_scriptable_idx].i_bot_button_idx;
            }

            if (cTrs.GetSiblingIndex() == 2)//Button Left
            {
                PlayInteractiveVideo(scriptable_video[i_current_scriptable_idx].video_to_play[cTrs.GetSiblingIndex()]);
                i_current_scriptable_idx = scriptable_video[i_current_scriptable_idx].i_left_button_idx;
            }

            if (cTrs.GetSiblingIndex() == 3)//Button Right
            {
                PlayInteractiveVideo(scriptable_video[i_current_scriptable_idx].video_to_play[cTrs.GetSiblingIndex()]);
                i_current_scriptable_idx = scriptable_video[i_current_scriptable_idx].i_right_button_idx;
            }
            b_can_show_buttons = false;
            DisableInteractiveVideoButton();
        }
    }

    public void DisableInteractiveVideoButton()
    {
        foreach (GameObject interactivebutton in buttons_video_player)
        {
            interactivebutton.SetActive(false);
        }
    }
    #endregion

    #region Contacts
    public void SaveEmail()
    {
        s_e_mail = my_input_field.text;
        Debug.Log(s_e_mail);
        Script_Save_Manager.Instance.Save(s_e_mail);
        my_input_field.text = "";
        my_input_field.GetComponentInChildren<Text>().text = "Envoyé";
        StartCoroutine(WaitTime(f_delay_text_display));
    }
    #endregion

    #region Video
    public void LaunchVideo()
    {
        if(g_menu_list[0].activeSelf)
        {
            cs_web_viewer.StopWebView();
        }
        Script_Game_Manager.Instance.DisablePopUpQuizz();
        DisplayVideo();
    }

    public void DisplayVideo()
    {
        g_video.SetActive(true);
    }

    public void HideVideo()
    {
        g_video.SetActive(false);
        Script_Game_Manager.Instance.ResetTimerInactivity();
        ChangeMenu(0);
    }
    #endregion

    #region Quizz
    public void VerifyScreen()// si on est pas sur l'écran de WEB ou Interactive video
    {
        if (i_current_menu_idx == 0 || i_current_scriptable_idx == 1)
        {
            b_ok_menu_quizz = false;
        }
        else
        {
            b_ok_menu_quizz = true;
        }
    }

    public bool ReturnBoolQuizz()
    {
        return b_ok_menu_quizz;
    }

    public void PopulateQuizz()//Renseigné dans les boutons / text le string correspondant au quizz actuel
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
        else if (i_quizz_already_asked.Count == scriptable_quizz_list.Count)
        {
            i_quizz_already_asked.Clear();
            PopulateQuizz();
        }
    }

    public void OnPickAnswer()//Selectionner une réponse
    {
        Transform cTrs = EventSystem.current.currentSelectedGameObject.transform;
        if (cTrs.GetSiblingIndex() == scriptable_quizz_list[i_current_quizz].i_idx_answer)
        {
            HideQuizz();
            ChangeMenu(0);
        }
        else if (cTrs.GetSiblingIndex() != scriptable_quizz_list[i_current_quizz].i_idx_answer)
        {
            PopulateQuizz();
        }
    }

    public void DisplayQuizz()
    {
        g_quizz.SetActive(true);
    }

    public void HideQuizz()
    {
        g_quizz.SetActive(false);
    }
    #endregion
}
