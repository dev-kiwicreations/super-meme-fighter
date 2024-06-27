using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStageSelection : MonoBehaviour
{
    public GameObject portraitP1;
    public GameObject portraitP2;
    public GameObject Stage;
    public GameObject StageButtons;
    public GameObject Title;
    public GameObject StageButtonsNew;
    public GameObject ReadyScreen;
    

    public void OnSelectPress()
    {
        //portraitP1.SetActive(false);
        //portraitP2.SetActive(false);
        StageButtons.SetActive(false);
        Title.SetActive(false);
        Stage.SetActive(false);
        StageButtonsNew.SetActive(true);
    }
    public void OnNewBackPress()
    {
        //portraitP1.SetActive(false);
        //portraitP2.SetActive(false);
        StageButtons.SetActive(true);
        Title.SetActive(true);
        Stage.SetActive(true);
        StageButtonsNew.SetActive(false);
    }
    public void OnYesPress()
    { 

    }
}
