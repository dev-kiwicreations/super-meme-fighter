using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UFE3D;

public class DefaultLoadingBattleScreen : LoadingBattleScreen
{
    public float delayBeforePreload = 1.5f;
    public float delayAfterPreload = 2.0f;
    public Text namePlayer1;
    public Text namePlayer2;
    public Text nameStage;
    public Image portraitPlayer1;
    public Image portraitPlayer2;
    public Image screenshotStage;

    #region public override methods
    public override void OnShow()
    {
        Debug.Log("Stage Selection: OnShow");
        base.OnShow();

        if (UFE.config.player1Character != null)
        {
            if (this.portraitPlayer1 != null)
            {
                this.portraitPlayer1.sprite = Sprite.Create(
                    UFE.config.player1Character.profilePictureBig,
                    new Rect(0f, 0f, UFE.config.player1Character.profilePictureBig.width, UFE.config.player1Character.profilePictureBig.height),
                    new Vector2(0.5f * UFE.config.player1Character.profilePictureBig.width, 0.5f * UFE.config.player1Character.profilePictureBig.height)
                );
            }

            if (this.namePlayer1 != null)
            {
                this.namePlayer1.text = UFE.config.player1Character.characterName.ToUpper();
            }
        }

        if (UFE.config.player2Character != null)
        {
            if (this.portraitPlayer2 != null)
            {
                this.portraitPlayer2.sprite = Sprite.Create(
                    UFE.config.player2Character.profilePictureBig,
                    new Rect(0f, 0f, UFE.config.player2Character.profilePictureBig.width, UFE.config.player2Character.profilePictureBig.height),
                    new Vector2(0.5f * UFE.config.player2Character.profilePictureBig.width, 0.5f * UFE.config.player2Character.profilePictureBig.height)
                );
            }

            if (this.namePlayer2 != null)
            {
                this.namePlayer2.text = UFE.config.player2Character.characterName.ToUpper();
            }
        }
        LeftPortraitAdjust();
        RightPortraitAdjust();
        if (UFE.config.selectedStage != null)
        {
            if (this.screenshotStage != null)
            {
                this.screenshotStage.sprite = Sprite.Create(
                    UFE.config.selectedStage.screenshot,
                    new Rect(0f, 0f, UFE.config.selectedStage.screenshot.width, UFE.config.selectedStage.screenshot.height),
                    new Vector2(0.5f * UFE.config.selectedStage.screenshot.width, 0.5f * UFE.config.selectedStage.screenshot.height)
                );

                //Animator anim = this.screenshotStage.GetComponent<Animator>();
                //if (anim != null)
                //{
                    //anim.enabled = UFE.gameMode != GameMode.StoryMode;
                //}
            }

            /*if (this.nameStage != null){
				this.nameStage.text = UFE.config.selectedStage.stageName;
			}*/
        }
        Debug.Log("Loading Screen done. trying to Load Battle");
        UFE.DelayLocalAction(UFE.PreloadBattle, this.delayBeforePreload);
        UFE.DelayLocalAction(this.StartBattle, (this.delayAfterPreload));
        Debug.Log("Loaded Battle");
    }
    #endregion
    public void LeftPortraitAdjust()
    {
        if(UFE.config.player1Character.characterName == "Doge")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-611, -188, 0);
                rectTransform.sizeDelta = new Vector2(1700f, 1982.41f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Toshi")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-708, -35, 0);
                rectTransform.sizeDelta = new Vector2(1372.928f, 1605.248f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Slerf")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-534, -168, 0);
                rectTransform.sizeDelta = new Vector2(1232.051f, 1440.532f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Trump")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-750, -146, 0);
                rectTransform.sizeDelta = new Vector2(2285.71f, 2059.42f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x * 0.75f, rectTransform.localScale.y * 0.75f, rectTransform.localScale.z * 0.75f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Mew")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-683, -101, 0);
                rectTransform.sizeDelta = new Vector2(1537.87f, 1528.859f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Shiba")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-773.6f, -168, 0);
                rectTransform.sizeDelta = new Vector2(1262.452f, 1476.077f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Pepe")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-648, -172, 0);
                rectTransform.sizeDelta = new Vector2(2085.638f, 3306.028f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Boden")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-678, -169, 0);
                rectTransform.sizeDelta = new Vector2(1412.971f, 2264.292f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Wif")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-682, -107, 0);
                rectTransform.sizeDelta = new Vector2(1546.961f, 1368.698f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Brett")
        {
            RectTransform rectTransform = portraitPlayer1.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-550, -142, 0);
                rectTransform.sizeDelta = new Vector2(1261.419f, 1268.854f);
            }
        }
    }
    public void RightPortraitAdjust()
    {
        if (UFE.config.player2Character.characterName == "Doge")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-611, -188, 0);
                rectTransform.sizeDelta = new Vector2(1656.197f, 1982.41f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Toshi")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-708, -35, 0);
                rectTransform.sizeDelta = new Vector2(1372.928f, 1605.248f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Slerf")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-534, -168, 0);
                rectTransform.sizeDelta = new Vector2(1232.051f, 1440.532f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Trump")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-750, -146, 0);
                rectTransform.sizeDelta = new Vector2(2285.71f, 2059.4f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x * 0.75f, rectTransform.localScale.y * 0.75f, rectTransform.localScale.z * 0.75f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Mew")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-683, -101, 0);
                rectTransform.sizeDelta = new Vector2(1537.87f, 1528.859f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Shiba")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-773.6f, -168, 0);
                rectTransform.sizeDelta = new Vector2(1262.452f, 1476.077f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Pepe")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-648, -172, 0);
                rectTransform.sizeDelta = new Vector2(2085.638f, 3306.028f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Boden")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-678, -169, 0);
                rectTransform.sizeDelta = new Vector2(1412.971f, 2264.292f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Wif")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-682, -107, 0);
                rectTransform.sizeDelta = new Vector2(1546.961f, 1368.698f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Brett")
        {
            RectTransform rectTransform = portraitPlayer2.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-550, -142, 0);
                rectTransform.sizeDelta = new Vector2(1261.419f, 1268.854f);
            }
        }
    }
}
