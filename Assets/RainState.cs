using DigitalRuby.RainMaker;
using UnityEngine;

public class RainState : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject.Find("RainPrefab2D").GetComponent<RainScript2D>().enabled = true;
    }

    private void OnDestroy()
    {
        GameObject.Find("RainPrefab2D").GetComponent<RainScript2D>().enabled = false;
    }
}
