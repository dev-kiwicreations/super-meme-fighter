using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemeMenuUI : MonoBehaviour
{
    [SerializeField] Image P1Portrait;
    [SerializeField] Image P2Portrait;
    [SerializeField] Image StageBG;



    private void Awake()
    {
        if (this.P1Portrait != null)
        {
            this.P1Portrait.sprite = Sprite.Create(
                UFE.config.player1Character.profilePictureBig,
                new Rect(0f, 0f, UFE.config.player1Character.profilePictureBig.width, UFE.config.player1Character.profilePictureBig.height),
                new Vector2(0.5f * UFE.config.player1Character.profilePictureBig.width, 0.5f * UFE.config.player1Character.profilePictureBig.height)
            );
        }
        if (this.P2Portrait != null)
        {
            this.P2Portrait.sprite = Sprite.Create(
                UFE.config.player2Character.profilePictureBig,
                new Rect(0f, 0f, UFE.config.player2Character.profilePictureBig.width, UFE.config.player2Character.profilePictureBig.height),
                new Vector2(0.5f * UFE.config.player2Character.profilePictureBig.width, 0.5f * UFE.config.player2Character.profilePictureBig.height)
            );
        }
        if (this.StageBG != null)
        {
            this.StageBG.sprite = Sprite.Create(
                UFE.config.selectedStage.screenshot,
                new Rect(0f, 0f, UFE.config.selectedStage.screenshot.width, UFE.config.selectedStage.screenshot.height),
                new Vector2(0.5f * UFE.config.selectedStage.screenshot.width, 0.5f * UFE.config.selectedStage.screenshot.height)
            );
        }
        LeftPortraitAdjust();
        RightPortraitAdjust();
    }

    public void LeftPortraitAdjust()
    {
        if (UFE.config.player1Character.characterName == "Doge")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-623f, -224.0916f, 0);
                rectTransform.sizeDelta = new Vector2(1625.743f, 1681.393f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Toshi")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-695.65f, -87, 0);
                rectTransform.sizeDelta = new Vector2(1391.3f, 1438.966f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Slerf")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-525, -133, 0);
                rectTransform.sizeDelta = new Vector2(1299.007f, 1343.511f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Trump")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-733.6379f, -110, 0);
                rectTransform.sizeDelta = new Vector2(1686.124f, 1630.717f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x * 0.75f, rectTransform.localScale.y * 0.75f, rectTransform.localScale.z * 0.75f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Mew")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-687, -121, 0);
                rectTransform.sizeDelta = new Vector2(1548.3f, 1497.416f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Shiba")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-773.6f, -168, 0);
                rectTransform.sizeDelta = new Vector2(1262.452f, 1476.077f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Pepe")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-626, -174.8076f, 0);
                rectTransform.sizeDelta = new Vector2(2314.849f, 3333.085f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Popcat")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-624.27f, -105, 0);
                rectTransform.sizeDelta = new Vector2(1424.8f, 1415.1f);
                rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Wif")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-675, -89, 0);
                rectTransform.sizeDelta = new Vector2(1527.438f, 1438.484f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player1Character.characterName == "Brett")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-550, -142, 0);
                rectTransform.sizeDelta = new Vector2(1261.419f, 1268.854f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Mog")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-489, -63, 0);
                rectTransform.sizeDelta = new Vector2(1161.56f, 1211.763f);
            }
        }
        else if (UFE.config.player1Character.characterName == "Floki")
        {
            RectTransform rectTransform = P1Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-528.1129f, -178.9518f, 0);
                rectTransform.sizeDelta = new Vector2(1123.334f, 1253.811f);
            }
        }
    }
    public void RightPortraitAdjust()
    {
        if (UFE.config.player2Character.characterName == "Doge")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-623f, -224.0916f, 0);
                rectTransform.sizeDelta = new Vector2(1625.743f, 1681.393f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Toshi")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-695.65f, -87, 0);
                rectTransform.sizeDelta = new Vector2(1391.3f, 1438.966f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Slerf")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-525, -133, 0);
                rectTransform.sizeDelta = new Vector2(1299.007f, 1343.511f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Trump")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-733.6379f, -110, 0);
                rectTransform.sizeDelta = new Vector2(1686.124f, 1630.717f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x * 0.75f, rectTransform.localScale.y * 0.75f, rectTransform.localScale.z * 0.75f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Mew")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-687, -121, 0);
                rectTransform.sizeDelta = new Vector2(1548.3f, 1497.416f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Shiba")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-773.6f, -168, 0);
                rectTransform.sizeDelta = new Vector2(1262.452f, 1476.077f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Pepe")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-626, -174.8076f, 0);
                rectTransform.sizeDelta = new Vector2(2314.849f, 3333.085f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Popcat")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-624.27f, -105, 0);
                rectTransform.sizeDelta = new Vector2(1424.8f, 1415.1f);
                rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Wif")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-675, -89, 0);
                rectTransform.sizeDelta = new Vector2(1527.438f, 1438.484f);
                rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
        else if (UFE.config.player2Character.characterName == "Brett")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-550, -142, 0);
                rectTransform.sizeDelta = new Vector2(1261.419f, 1268.854f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Mog")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-489, -63, 0);
                rectTransform.sizeDelta = new Vector2(1161.56f, 1211.763f);
            }
        }
        else if (UFE.config.player2Character.characterName == "Floki")
        {
            RectTransform rectTransform = P2Portrait.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector3(-528.1129f, -178.9518f, 0);
                rectTransform.sizeDelta = new Vector2(1123.334f, 1253.811f);
            }
        }
    }
}
