using System;
using UFE3D;
using UnityEngine;

public class DefaultMainMenuScreen : MainMenuScreen
{
    public PlaySFX playSfx;
    public GameObject howToPlayScreen;
    
    #region public override methods
    public override void OnShow()
    {
        base.OnShow();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (howToPlayScreen.activeInHierarchy)
            {
                HTP_Screen(false);
            }
        }
    }

    public void HTP_Screen(bool state)
    {
        howToPlayScreen.SetActive(state);
    }
}
