using UFE3D;
using UnityEngine;

public class DefaultControlGMScreen : ControlGMScreen
{
    protected UFEScreen pause = null;
    protected bool hiding = false;
    
    #region public override methods
    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnShow()
    {
        base.OnShow();
    }
    
    /*public override void OnGamePaused(bool isPaused)
    {
        base.OnGamePaused(isPaused);
        if (UFE.config.gameGUI.pauseScreen != null)
        {
            if (isPaused)
            {
                this.pause = GameObject.Instantiate(UFE.config.gameGUI.pauseScreen);
                this.pause.transform.SetParent(UFE.canvas != null ? UFE.canvas.transform : null, false);
                this.pause.OnShow();
            }
            else if (this.pause != null)
            {
                if (!this.hiding)
                {
                    UFE.PlayMusic(UFE.config.selectedStage.music);
                }
                this.pause.OnHide();
                GameObject.Destroy(this.pause.gameObject);
            }
        }
    }*/
    #endregion
    
}
