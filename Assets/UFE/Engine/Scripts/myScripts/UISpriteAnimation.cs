using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = 0.02f;

    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;

    private void Start()
    {
        Func_PlayUIAnim();
    }

    public void Func_PlayUIAnim()
    {
        IsDone = false;
        if (m_CorotineAnim != null)
        {
            StopCoroutine(m_CorotineAnim);
        }
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        IsDone = true;
        if (m_CorotineAnim != null)
        {
            StopCoroutine(m_CorotineAnim);
            m_CorotineAnim = null;
        }
    }


    IEnumerator Func_PlayAnimUI()
    {
        while (!IsDone)
        {
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite = (m_IndexSprite + 1) % m_SpriteArray.Length;
            yield return new WaitForSeconds(m_Speed);
        }
    }
}
