using System;
using UFE3D;
using UnityEngine;

public class DefaultMainMenuScreen : MainMenuScreen
{
    
    #region public override methods
    public override void OnShow()
    {
        base.OnShow();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DirectlyStartPlayerVersusCPU();
        } // Check if the Backspace key is pressed
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //GoToOptionsScreen();
        }
    }
}
