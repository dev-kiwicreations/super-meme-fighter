using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAutoFill : MonoBehaviour
{
    // Reference to the Slider component
    private Slider slider;

    // Duration over which the slider should increment
    public float duration = 2.0f;

    void Start()
    {
        // Get the Slider component attached to this GameObject
        slider = GetComponent<Slider>();

        // Start the coroutine to increment the slider
        if (slider != null)
        {
            StartCoroutine(IncrementSlider());
        }
        else
        {
            Debug.LogWarning("Slider component not found.");
        }
    }

    IEnumerator IncrementSlider()
    {
        float elapsedTime = 0f;
        float startValue = slider.value;
        float endValue = 1f; // Assuming the slider max value is 1

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }

        // Ensure the slider is set to the end value after the loop
        slider.value = endValue;
    }

    // Update is called once per frame

}
