using System;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultMainMenuScreen : MainMenuScreen
{
    public PlaySFX playSfx;
    public Button StartGame;
    public Button HowToPlayButton;
    public Button OptionsButton;
    public GameObject BGImage;
    #region public override methods
    public override void OnShow()
    {
        base.OnShow();
        if(VidExp.disableCheck) BGImage.SetActive(false);
    }
    public override void DoFixedUpdate(
        IDictionary<InputReferences, InputEvents> player1PreviousInputs,
        IDictionary<InputReferences, InputEvents> player1CurrentInputs,
        IDictionary<InputReferences, InputEvents> player2PreviousInputs,
        IDictionary<InputReferences, InputEvents> player2CurrentInputs
    )
    {
        this.SpecialNavigationSystem(
            player1PreviousInputs,
            player1CurrentInputs,
            player2PreviousInputs,
            player2CurrentInputs,
            new UFEScreenExtensions.MoveCursorCallback(this.HighlightStage));
        
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
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                StartGame.Select();
            }
            if (verticalAxis > 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject)
                {
                    OptionsButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    StartGame.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == OptionsButton.gameObject) 
                {
                    HowToPlayButton.Select();
                }
            }
            else if (verticalAxis < 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject)
                {
                    HowToPlayButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    OptionsButton.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == OptionsButton.gameObject) 
                {
                    StartGame.Select();
                }
            }
        }
        if (confirmButtonDown)
        {

            UFE.PlaySound(cancelSound);
            if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject)
            {
                DirectlyStartPlayerVersusCPU();
            }
            else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
            {
                GoToControlsScreen();
            }
            else if (EventSystem.current.currentSelectedGameObject == OptionsButton.gameObject)
            {
                GoToOptionsScreen();
            }
            VidExp.disableCheck = true;
        }
    }
    #endregion

    
}
