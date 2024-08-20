using UnityEngine;
using System.Collections.Generic;
using FPLibrary;
using UFE3D;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultPauseScreen : PauseScreen
{
    #region public instance fields
    public UFEScreen backToMenuConfirmationDialog;
    public UFEScreen[] screens;
    public Button ContinueButton;
    public Button HowToPlayButton;
    public Button MainMenuButton;
    public Button BackButtonOnHowToPlay;
    public GameObject ShowControls;
    #endregion

    #region protected instance fields
    protected int currentScreen = -1;
    protected bool confirmationDialogVisible = false;
    #endregion

    #region public instance methods
    public virtual void HideBackToMenuConfirmationDialog()
    {
        this.HideBackToMenuConfirmationDialog(true);
    }

    public virtual void HideBackToMenuConfirmationDialog(bool triggerOnShowScreenEvent)
    {
        if (this.backToMenuConfirmationDialog != null)
        {
            for (int i = 0; i < this.screens.Length; ++i)
            {
                if (this.screens[i] != null)
                {
                    CanvasGroup canvasGroup = this.screens[i].GetComponent<CanvasGroup>();

                    if (canvasGroup != null)
                    {
                        canvasGroup.interactable = true;
                    }
                }
            }

            this.HideScreen(this.backToMenuConfirmationDialog);
            this.confirmationDialogVisible = false;

            if (triggerOnShowScreenEvent && currentScreen >= 0)
            {
                this.ShowScreen(this.screens[this.currentScreen]);
            }
        }
    }

    public virtual void GoToScreen(int index)
    {
        for (int i = 0; i < this.screens.Length; ++i)
        {
            if (i != index)
            {
                this.HideScreen(this.screens[i]);
            }
            else
            {
                this.ShowScreen(this.screens[i]);
            }
        }

        this.currentScreen = index;
    }

    public virtual void ShowBackToMenuConfirmationDialog()
    {
        if (this.backToMenuConfirmationDialog != null)
        {
            for (int i = 0; i < this.screens.Length; ++i)
            {
                if (this.screens[i] != null)
                {
                    CanvasGroup canvasGroup = this.screens[i].GetComponent<CanvasGroup>();

                    if (canvasGroup != null)
                    {
                        canvasGroup.interactable = false;
                    }
                    else
                    {
                        this.HideScreen(this.screens[i]);
                    }
                }
            }

            this.ShowScreen(this.backToMenuConfirmationDialog);
            this.confirmationDialogVisible = true;
        }
    }
    #endregion

    #region public override methods
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
            if (verticalAxis < 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == ContinueButton.gameObject)
                {
                    MainMenuButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    ContinueButton.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == MainMenuButton.gameObject) 
                {
                    HowToPlayButton.Select();
                }
            }
            else if (verticalAxis > 0)
            {
                UFE.PlaySound(moveCursorSound);
                if (EventSystem.current.currentSelectedGameObject == ContinueButton.gameObject)
                {
                    HowToPlayButton.Select();
                }
                else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
                {
                    MainMenuButton.Select();
                }
                else if(EventSystem.current.currentSelectedGameObject == MainMenuButton.gameObject) 
                {
                    ContinueButton.Select();
                }
            }
        }
        if (confirmButtonDown)
        {
            if (EventSystem.current.currentSelectedGameObject == ContinueButton.gameObject)
            {
                ResumeGame();
            }
            else if (EventSystem.current.currentSelectedGameObject == HowToPlayButton.gameObject)
            {
                ShowControls.gameObject.SetActive(true);
                BackButtonOnHowToPlay.Select();
            }
            else if (EventSystem.current.currentSelectedGameObject == MainMenuButton.gameObject)
            {
                GoToMainMenu();
            }
        }
    }

    public override void OnShow()
    {
        base.OnShow();

        this.confirmationDialogVisible = false;
        this.HideBackToMenuConfirmationDialog(false);
        if (this.screens.Length > 0)
        {
            this.GoToScreen(0);
        }
    }

    public override void OnHide()
    {
        base.OnHide();

        this.confirmationDialogVisible = false;
        this.HideBackToMenuConfirmationDialog(false);
        if (this.currentScreen >= 0 && this.currentScreen < this.screens.Length)
        {
            this.HideScreen(this.screens[this.currentScreen]);
        }
    }

    public override void SelectOption(int option, int player)
    {
        if (this.currentScreen >= 0 && this.currentScreen < this.screens.Length && this.screens[this.currentScreen] != null)
        {
            this.screens[this.currentScreen].SelectOption(option, player);
        }
    }
    #endregion

    #region protected instance methods
    protected virtual void HideScreen(UFEScreen screen)
    {
        if (screen != null)
        {
            screen.OnHide();
            screen.gameObject.SetActive(false);
        }
        currentScreen = -1;
    }

    protected virtual bool IsVisible(UFEScreen screen)
    {
        return screen != null && screen.IsVisible();
    }

    protected virtual void ShowScreen(UFEScreen screen)
    {
        if (screen != null)
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                return;
            }
            screen.gameObject.SetActive(true);
            screen.OnShow();
        }
    }
    #endregion
    
}
