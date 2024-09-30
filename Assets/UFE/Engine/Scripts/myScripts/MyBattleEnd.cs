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
                
                string storedWinnerName = PlayerPrefs.GetString("winnerName");
                Debug.Log("Winner is null and name is: |" + storedWinnerName
                    + "| comparing with |" + UFE.config.player1Character.characterName
                    + "| and |" + UFE.config.player2Character.characterName);
                bool winCondition = false;
                if (storedWinnerName == (UFE.config.player1Character.characterName))
                {
                    winnerPlayerInfo = UFE.config.player1Character;
                    winCondition = true;
                }
                else if (storedWinnerName == (UFE.config.player2Character.characterName))
                {
                    winnerPlayerInfo = UFE.config.player2Character;
                }
                else
                {
                    Debug.Log("The stored winner does not match player 1 or player 2.");
                }
                APIReader.Instance.PutAPIRequest(winCondition);
                if (storedWinnerName == "Toshi" || storedWinnerName == "Trump")
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
            PortraitAdjust();
            //----------------------------------
            if(winnerPlayerInfo.characterName == "Brett") winnerText.text = (winnerPlayerInfo.characterName + "   WINS!").ToUpper();
            else if (winnerPlayerInfo.characterName == "Popcat") winnerText.text = (winnerPlayerInfo.characterName + "   WINS!").ToUpper();
            else winnerText.text = (winnerPlayerInfo.characterName + " WINS!").ToUpper();
        }
        public void PortraitAdjust()
        {
            if (winnerPlayerInfo.characterName == "Doge")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(48f, -587, 0);
                    rectTransform.sizeDelta = new Vector2(1912.059f, 1889.804f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Toshi")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(134, -238, 0);
                    rectTransform.sizeDelta = new Vector2(1412.2f, 1395.788f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Slerf")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(120, -279, 0);
                    rectTransform.sizeDelta = new Vector2(1348.922f, 1333.247f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Trump")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(-133f, -406, 0);
                    rectTransform.sizeDelta = new Vector2(1617.501f, 1598.675f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Mew")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(63.514f, -338.45f, 0);
                    rectTransform.sizeDelta = new Vector2(1552.973f, 1534.897f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Shiba")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(-195f, -333f, 0);
                    rectTransform.sizeDelta = new Vector2(1339.731f, 1469.614f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Pepe")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(95f, -1289f, 0);
                    rectTransform.sizeDelta = new Vector2(2090f, 3312.942f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Boden")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(192f, -734, 0);
                    rectTransform.sizeDelta = new Vector2(1375.53f, 2204.293f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Wif")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(74, -276.5f, 0);
                    rectTransform.sizeDelta = new Vector2(1594.765f, 1411.008f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Brett")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(80, -235, 0);
                    rectTransform.sizeDelta = new Vector2(1289.885f, 1297.487f);
                }
            }
            else if (winnerPlayerInfo.characterName == "Popcat")
            {
                RectTransform rectTransform = winnerPortrait.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector3(0, -235, 0);
                    rectTransform.sizeDelta = new Vector2(1083f, 1201f);
                }
            }
        }
    }

}