using UnityEngine;

namespace UFE3D
{
    public class PauseScreen : UFEScreen
    {
        public int BackToMenuFrameDelay = 4;

        public virtual void GoToMainMenu()
        {
            // Delay is necessary to avoid double input through the menu
            UFE.DelayLocalAction(GoToMainMenuDelayed, BackToMenuFrameDelay);
        }

        private void GoToMainMenuDelayed()
        {
            Debug.Log("GoToMainMenuDelayed");
            UFE.EndGame();
            //TODO StopMusic is being called manually. 
            //This means that whenever a new screen or gamemode is designed, we will have to call StopMusic again
            //Design a better way to handle this so that the music automatically transitions between stage music and main menu or other screen music.
            UFE.StopMusic();
            UFE.StartMainMenuScreen();
            UFE.PauseGame(false);
        }

        public virtual void ResumeGame()
        {
            UFE.PauseGame(false);
        }
    }
}