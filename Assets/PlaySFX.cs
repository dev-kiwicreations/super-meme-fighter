using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public void PlayHoverSFX(AudioClip clip)
    {
        UFE.PlaySound(clip);
    }
    
    public void PlayClickSFX(AudioClip clip)
    {
        UFE.PlaySound(clip);
    }
}
