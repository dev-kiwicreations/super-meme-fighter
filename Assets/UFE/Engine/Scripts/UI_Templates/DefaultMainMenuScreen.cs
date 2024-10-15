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
    public Button StartGame,StartFight;
    public Button HowToPlayButton;
    public Button OptionsButton;
    public GameObject BGImage;
    public GameObject GenericMenu, MemeMenu;
    #region public override methods
    private void Awake()
    {
        if (UFE.Mode == 1) firstSelectableGameObject = StartFight.gameObject;
    }
    public override void OnShow()
    {
        if (UFE.Mode == 1)
        {
            StartFight.gameObject.SetActive(true);
            StartGame.gameObject.SetActive(false);
            GenericMenu.gameObject.SetActive(false);
            MemeMenu.gameObject.SetActive(true);
        }
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
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                if (UFE.Mode == 1) StartGame.Select();
                else StartFight.Select();
            }
           
            if (verticalAxis > 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject || EventSystem.current.currentSelectedGameObject == StartFight.gameObject)
                {
                    OptionsButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    if (UFE.Mode == 1) StartFight.Select();
                    else StartGame.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == OptionsButton.gameObject) 
                {
                    HowToPlayButton.Select();
                }
            }
            else if (verticalAxis < 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject || EventSystem.current.currentSelectedGameObject == StartFight.gameObject)
                {
                    HowToPlayButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    OptionsButton.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == OptionsButton.gameObject) 
                {
                    if (UFE.Mode == 1) StartFight.Select();
                    else StartGame.Select();
                }
            }
        }
        if (confirmButtonDown)
        {

            UFE.PlaySound(cancelSound);
            if (EventSystem.current.currentSelectedGameObject == StartGame.gameObject)
            {
              //  UFE.ChangeModes(1, 1, 1, 1);
                DirectlyStartPlayerVersusCPU();
            }
            else if(EventSystem.current.currentSelectedGameObject == StartFight.gameObject)
            {
                GoToLoadingScreen();
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
