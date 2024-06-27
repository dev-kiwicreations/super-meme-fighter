using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UFE3D.BattleGUI;

namespace UFE3D
{
    public class MyBattleEnd : MonoBehaviour
    {
        public static MyBattleEnd instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public CharacterInfo winnerPlayerInfo = null;
        public Image mapBG;
        public Image winnerPortrait;
        public Text winnerText;

        // Start is called before the first frame update
        void Start()
        {
            if (UFE.config.selectedStage != null && UFE.config.selectedStage.screenshot != null)
            {
                Texture2D texture = UFE.config.selectedStage.screenshot;

                // Create a Sprite from the Texture2D
                Sprite sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );

                // Assign the Sprite to the Image component
                mapBG.sprite = sprite;
            }
            else
            {
                Debug.Log("Selected stage or screenshot is null.");
            }


            if (UFE.GetBattleGUI().publicWinner != null)
            {
                winnerPlayerInfo = UFE.GetBattleGUI().publicWinner;
            }
            else
            {
                Debug.Log("Winner is null.");
                string storedWinnerName = PlayerPrefs.GetString("winnerName");
                if (storedWinnerName == UFE.config.player1Character.name)
                {
                    winnerPlayerInfo = UFE.config.player1Character;
                }
                else if (storedWinnerName == UFE.config.player2Character.name)
                {
                    winnerPlayerInfo = UFE.config.player2Character;
                }
                else
                {
                    Debug.Log("The stored winner does not match player 1 or player 2.");
                }
                if (storedWinnerName.Contains("T"))
                {
                    winnerPortrait.transform.localScale = new Vector3(
                        -winnerPortrait.transform.localScale.x, 
                        winnerPortrait.transform.localScale.y, 
                        winnerPortrait.transform.localScale.z
                        );
                }
            }
            SetWinnerPlayer();
        }

        public void SetWinnerPlayer()
        {


            Texture2D texture = winnerPlayerInfo.profilePictureBig;

            // Create a Sprite from the Texture2D
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            // Assign the Sprite to the Image component
            winnerPortrait.sprite = sprite;
            
            //----------------------------------

            winnerText.text = (winnerPlayerInfo.characterName + " WINS!").ToUpper();
        }
    }
}