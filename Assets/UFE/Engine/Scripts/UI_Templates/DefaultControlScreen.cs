using System;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;
using Button = UnityEngine.UI.Button;
using System.Collections;

public class DefaultControlScreen : ControlScreen
{
    
    public PlaySFX playSfx;
    
    #region public override methods
    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnShow()
    {
        base.OnShow();
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playSfx.PlaySfx(playSfx.clickSound);
            GoToMainMenuScreen();
        }
    }
}
