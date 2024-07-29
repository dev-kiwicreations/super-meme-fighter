using System.Collections.Generic;
using UFE3D;
using UnityEngine;

public class DefaultVersusModeAfterBattleScreen : VersusModeAfterBattleScreen
{
    #region public override methods
    public override void OnShow()
    {
        base.OnShow();
        if (!UFE.IsPlayingMusic())
        {
            UFE.PlayMusic(this.music);
        }
        UFE.canvas.planeDistance = 0.1f;
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
    { }
    #endregion
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RepeatBattle();
        } // Check if the Backspace key is pressed
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GoToMainMenu();
        }
    }
}
