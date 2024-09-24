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
    int i = 0;
    public Sprite[] ControlTextures;
    public UnityEngine.UI.Image LayoutImage;
    public GameObject NextBtn, PrevBtn;
    bool NextPressed, PrevPressed;
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

        if (!NextPressed && (Input.GetAxis("P1JoystickHorizontal") == 1 || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("P1JoystickHorizontalDpad") == 1))
        {
            playSfx.PlaySfx(playSfx.clickSound);

            ChangeControlLayout(1);
            NextBtn.SetActive(false);
            PrevBtn.SetActive(true);
            NextPressed = !NextPressed;
            if (PrevPressed == true) PrevPressed = false;
        }
        if (!PrevPressed && (Input.GetAxis("P1JoystickHorizontal") < 0 || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("P1JoystickHorizontalDpad") < 0))
        {
            playSfx.PlaySfx(playSfx.clickSound);

            ChangeControlLayout(0);
            PrevBtn.SetActive(false);
            NextBtn.SetActive(true);
            if (NextPressed == true) NextPressed = false;
            PrevPressed = !PrevPressed;

        }
    }

    #endregion

    public void NextLayoutBtn()
    {
        ChangeControlLayout(1);

        NextBtn.SetActive(false);
        PrevBtn.SetActive(true);
    }
    public void PrevLayoutBtn()
    {
        ChangeControlLayout(0);
        PrevBtn.SetActive(false);
        NextBtn.SetActive(true);
    }
    public void ChangeControlLayout(int index)
    {
        LayoutImage.sprite = ControlTextures[index];
    }
}
