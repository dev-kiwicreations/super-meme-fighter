using System;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefaultVersusModeAfterBattleScreen : VersusModeAfterBattleScreen
{
    public Button PlayAgain;
    public Button BackToMainMenu;
    public Button TweetBtn;
    public Button ExitBtn;
    
    #region public override methods
    public override void OnShow()
    {
        Debug.Log("SHOWING ENDPANEL");
        base.OnShow();
        if (!UFE.IsPlayingMusic())
        {
            UFE.PlayMusic(this.music);
        }
        if (UFE.Mode == 1)
        {
            PlayAgain.gameObject.SetActive(false);
            BackToMainMenu.gameObject.SetActive(false);
            ExitBtn.gameObject.SetActive(true);
            TweetBtn.gameObject.SetActive(true);
        }
        UFE.canvas.planeDistance = 20f;
        UFE.canvas.sortingOrder = 500;
        UFE.canvas.worldCamera = Camera.main;
        UFE.canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

    // Override constructor and don't call base
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

    [Obsolete]
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
            if (verticalAxis > 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (UFE.Mode == 1) TweetBtn.Select();
                else
                {
                    PlayAgain.Select();
                }
            }
            else if (verticalAxis < 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (UFE.Mode == 1) ExitBtn.Select();
                else BackToMainMenu.Select();

            }
        }
        if (confirmButtonDown)
        {

            if (EventSystem.current.currentSelectedGameObject == PlayAgain.gameObject)
            {
                Debug.Log(">>>> Button RepeatBattle Selected");
                UFE.RestartMatch();
            }
            else if (EventSystem.current.currentSelectedGameObject == BackToMainMenu.gameObject)
            {
                Debug.Log("Main Called");
                UFE.PlaySound(selectSound);
                GoToMainMenu();

            }
            else if (EventSystem.current.currentSelectedGameObject == TweetBtn.gameObject)
            {
                UFE.PlaySound(selectSound);
                TweetOnClick();
               // Functionality for Tweet Not Yet Finalized.
            }
            else if(EventSystem.current.currentSelectedGameObject == ExitBtn.gameObject)
            {
                Debug.Log("ExitBtn Called");
                RedirectToDashboard("https://staging.supermemefighter.xyz/dashboard");
            }
        }
        if (cancelButtonDown)
        {
            Debug.Log(">>>> Cancel MainMenu Selected");
            //GoToStageSelectionScreen();
        }
    }
    
    #endregion
    
    private void Update()
    {
       /*if (Input.GetKeyDown(KeyCode.Return))
       {
            if (EventSystem.current.currentSelectedGameObject == PlayAgain.gameObject)
            {
                UFE.PlaySound(selectSound);
                RepeatBattle();
            }
            else if (EventSystem.current.currentSelectedGameObject == BackToMainMenu.gameObject)
            {
                Debug.Log("Main Called");
                UFE.PlaySound(selectSound);
                GoToMainMenu();
            }
       }*/
        
       /*  // Check if the Backspace key is pressed
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GetComponent<PlaySFX>().PlaySfx(GetComponent<PlaySFX>().clickSound);
            GoToMainMenu();
        }*/
    }

    private void OnDisable()
    {
        UFE.canvas.sortingOrder = 0;
        UFE.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        UFE.canvas.worldCamera = null;
    }
}
