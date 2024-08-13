using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip selectSound;
    public AudioClip yesSound;
    public AudioClip characterSound;
    
    public void PlaySfx(AudioClip clip)
    {
        UFE.PlaySound(clip);
    }
}
