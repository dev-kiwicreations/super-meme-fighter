using DigitalRuby.RainMaker;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    [SerializeField] private RainScript2D RainScript2D;

    public static RainManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DeactivateRain();
    }
    public void ActivateRain()
    {
        RainScript2D.gameObject.SetActive(true);
        RainScript2D.RainIntensity = 0.5f;
        RainScript2D.RainFallParticleSystem.Play();
        RainScript2D.RainExplosionParticleSystem.Play();
        RainScript2D.RainMistParticleSystem.Play();
    }
    public void DeactivateRain()
    {
        RainScript2D.RainIntensity = 0f;
        RainScript2D.gameObject.SetActive(false);
        RainScript2D.RainFallParticleSystem.Stop();
        RainScript2D.RainExplosionParticleSystem.Stop();
        RainScript2D.RainMistParticleSystem.Stop();
    }
}
