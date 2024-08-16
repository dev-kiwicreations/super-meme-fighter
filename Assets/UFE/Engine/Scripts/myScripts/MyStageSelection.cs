using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyStageSelection : MonoBehaviour
{
    public GameObject portraitP1;
    public GameObject portraitP2;
    public GameObject Stage;
    public GameObject StageButtons;
    public GameObject Title;
    public GameObject StageButtonsNew;
    public GameObject ReadyScreen;
    public DefaultStageSelectionScreen DefaultStageSelectionScreen;
    public Button ButtonSelectYesStage;

    public Text mapName;
    public GameObject area51png;
    

    public void OnSelectPress(AudioClip sound)
    {
        //portraitP1.SetActive(false);
        //portraitP2.SetActive(false);
        StageButtons.SetActive(false);
        Title.SetActive(false);
        Stage.SetActive(false);
        StageButtonsNew.SetActive(true);
        if(mapName.text.Contains("rea"))
        {
            mapName.gameObject.SetActive(false);
            area51png.SetActive(true);
        }
        else
        {
            mapName.gameObject.SetActive(true);
            area51png.SetActive(false);
        }
        // DefaultStageSelectionScreen.firstSelectableGameObject = ButtonSelectYesStage.gameObject;
        ButtonSelectYesStage.Select();
    }
    public void OnNewBackPress(AudioClip sound)
    {
        //portraitP1.SetActive(false);
        //portraitP2.SetActive(false);
        StageButtons.SetActive(true);
        Title.SetActive(true);
        Stage.SetActive(true);
        StageButtonsNew.SetActive(false);
    }
}
