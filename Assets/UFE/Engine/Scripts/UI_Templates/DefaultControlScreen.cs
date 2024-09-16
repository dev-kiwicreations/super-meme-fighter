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
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.EventSystems;

public class DefaultControlScreen : ControlScreen
{
    
    public PlaySFX playSfx;
    public Sprite[] ControlTextures;
    public UnityEngine.UI.Image LayoutImage;
    int i = 0;
    public GameObject NextBtn, PrevBtn;
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
                playSfx.PlaySfx(playSfx.clickSound);
                GoToMainMenuScreen();
            }));

    }

    #endregion

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playSfx.PlaySfx(playSfx.clickSound);
            GoToMainMenuScreen();
        }*/
        if (Input.GetAxis("P1JoystickHorizontal") > 0 || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("P1JoystickHorizontalDpad") > 0)
        {
            ChangeControlLayout(1);
            NextBtn.SetActive(false);
            PrevBtn.SetActive(true);
        }
        if (Input.GetAxis("P1JoystickHorizontal") < 0 || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("P1JoystickHorizontalDpad") <0)
        {
            ChangeControlLayout(0);
            PrevBtn.SetActive(false);
            NextBtn.SetActive(true);
        }
    }

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
