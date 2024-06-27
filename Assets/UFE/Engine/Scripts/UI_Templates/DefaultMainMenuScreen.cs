using UFE3D;

public class DefaultMainMenuScreen : MainMenuScreen
{
    private void Start()
    {
        if (!UFE.IsPlayingMusic())
        {
            UFE.PlayMusic();
        }
    }
    #region public override methods
    public override void OnShow()
    {
        base.OnShow();
    }
    #endregion
}
