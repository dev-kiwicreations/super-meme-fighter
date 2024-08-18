using UnityEngine;

namespace UFE3D
{
    public class ControlScreen : UFEScreen
    {
        public virtual void GoToMainMenuScreen()
        {
            UFE.StartMainMenuScreen();
        }
    }
}
