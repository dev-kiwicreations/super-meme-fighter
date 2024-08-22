using System;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;
using Button = UnityEngine.UI.Button;
using System.Collections;
using System.Collections.Generic;

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

    public override void DoFixedUpdate(IDictionary<InputReferences, InputEvents> player1PreviousInputs, IDictionary<InputReferences, InputEvents> player1CurrentInputs,
        IDictionary<InputReferences, InputEvents> player2PreviousInputs, IDictionary<InputReferences, InputEvents> player2CurrentInputs)
    {
        this.SpecialNavigationSystem(
            player1PreviousInputs,
            player1CurrentInputs,
            player2PreviousInputs,
            player2CurrentInputs,
            null,null,new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
            {
                // this.TryDeselectCharacter(1);
                GoToMainMenuScreen();
            }));
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
