using UnityEngine;
using System.Collections;


public class LightFlicker : MonoBehaviour
{
    public Light spotlight; // Assign your spotlight in the Inspector
    public float minFlickerDelay = 0.1f; // Minimum delay between flickers
    public float maxFlickerDelay = 0.5f; // Maximum delay between flickers

    private bool isFlickering = false;

    void Start()
    {
        if (spotlight == null)
        {
            spotlight = GetComponent<Light>();
        }

        if (spotlight == null)
        {
            Debug.LogError("No Light component found on the GameObject.");
        }
    }

    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight()
    {
        isFlickering = true;

        // Turn the light off
        spotlight.enabled = false;

        // Wait for a random delay
        yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

        // Turn the light on
        spotlight.enabled = true;

        // Wait for another random delay
        yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

        isFlickering = false;
    }
}
