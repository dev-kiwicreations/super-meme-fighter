using UnityEngine;

namespace UFE3D
{
    public class ControlGMScreen : UFEScreen
    {
        public virtual void GoToPauseScreen()
        {
            UFE.StartPauseScreen();
        }
    }
}
