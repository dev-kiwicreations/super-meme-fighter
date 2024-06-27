using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownText : MonoBehaviour
{
    // Reference to the Text component
    public Text countdownText;

    void Start()
    {
        // Disable the Text GameObject
        countdownText.gameObject.SetActive(false);
        // Start the countdown coroutine
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(0.1f);

        // Enable the Text GameObject
        countdownText.gameObject.SetActive(true);

        // Countdown from 3 to 1
        for (int i = 3; i > 0; i--)
        {
            // Update the text
            countdownText.text = i.ToString();
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }

        // Wait for an additional second after the countdown
        yield return new WaitForSeconds(1f);

        // Disable the Text GameObject
        countdownText.gameObject.SetActive(false);
    }
}
