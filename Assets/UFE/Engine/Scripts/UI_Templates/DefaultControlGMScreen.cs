using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using UnityEngine.EventSystems;

public class DefaultControlGMScreen : ControlGMScreen
{
    public DefaultPauseScreen PauseScreen;
    public Button BackToPause;
    public PlaySFX playSfx;
    
    #region public override methods
    
    public override void DoFixedUpdate(
        IDictionary<InputReferences, InputEvents> player1PreviousInputs,
        IDictionary<InputReferences, InputEvents> player1CurrentInputs,
        IDictionary<InputReferences, InputEvents> player2PreviousInputs,
        IDictionary<InputReferences, InputEvents> player2CurrentInputs
    )
    {
        Debug.Log("pause menu gm controls");
        this.SpecialNavigationSystem(
            player1PreviousInputs,
            player1CurrentInputs,
            player2PreviousInputs,
            player2CurrentInputs,
            new UFEScreenExtensions.MoveCursorCallback(this.HighlightStage),null, new UFEScreenExtensions.ActionCallback(delegate (AudioClip sound)
            {
                // this.TryDeselectCharacter(1);
               PauseScreen.GoBackToPause();
            }));
    }

    protected virtual void HighlightStage(
        Fix64 horizontalAxis,
        Fix64 verticalAxis,
        bool horizontalAxisDown,
        bool verticalAxisDown,
        bool confirmButtonDown,
        bool cancelButtonDown,
        AudioClip sound
    )
    {
        if (verticalAxisDown)
        {
            if (verticalAxis < 0)
            {
                UFE.PlaySound(moveCursorSound);
                BackToPause.Select();
            }
            else if (verticalAxis > 0)
            {
                UFE.PlaySound(moveCursorSound);
                BackToPause.Select();
            }
        }
        if (confirmButtonDown)
        {
            if (EventSystem.current.currentSelectedGameObject == BackToPause.gameObject)
            {
                UFE.PlaySound(selectSound);
            }
        }

        if (cancelButtonDown)
        {
            PauseScreen.GoBackToPause();
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp("P1Pause"))
        {
            PauseScreen.GoBackToPause();
        }
    }

    #endregion
    
}
