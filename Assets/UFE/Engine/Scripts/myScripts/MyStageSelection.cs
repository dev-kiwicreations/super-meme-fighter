using UnityEngine;
using UnityEngine.UI;

public class MyStageSelection : MonoBehaviour
{
    public GameObject Stage;
    public GameObject StageButtons;
    public GameObject Title;
    public GameObject StageButtonsNew;
    public Button ButtonSelectYesStage;

    public Text mapName;
    public GameObject area51png;
    

    public void OnSelectPress(AudioClip sound)
    {
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
        ButtonSelectYesStage.Select();
    }
    public void OnNewBackPress(AudioClip sound)
    {
        StageButtons.SetActive(true);
        Title.SetActive(true);
        Stage.SetActive(true);
        StageButtonsNew.SetActive(false);
    }
}
