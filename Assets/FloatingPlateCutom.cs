using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlate : MonoBehaviour
{
    public float scalingSpeed = 2f;      // Speed of scaling (growing/shrinking)
    public float scalingAmount = 10f;  // Maximum amount to scale
    public float rotationSpeed = 50f;   // Speed of spinning around Y-axis
    public float bobbingSpeed = 1f;     // Speed of slight bobbing motion
    public float bobbingHeight = 0.02f; // Small height for subtle bobbing

    private Vector3 initialScale;       // Original size of the plate (and children)
    private Vector3 initialPosition;    // Starting position of the plate

    void Start()
    {
        // Save the initial position and scale
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 1. Slight bobbing motion
        float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.position = initialPosition + new Vector3(0, bobbingOffset, 0);

        // 2. Pulsating motion (growing and shrinking)
        float scaleOffset = Mathf.Sin(Time.time * scalingSpeed) * scalingAmount;
        transform.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);

        // 3. Circular spinning motion (rotating around Y-axis)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
