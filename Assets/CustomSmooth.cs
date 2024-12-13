using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class SmoothLineRenderer : MonoBehaviour
{
    public int resolution = 20; // Number of interpolated points between each pair of control points

    private LineRenderer lineRenderer;
    private List<Vector3> controlPoints = new List<Vector3>(); // Stores the original points
    private List<Vector3> smoothedPoints = new List<Vector3>(); // Stores the smoothed points

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Load the control points from the LineRenderer
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            controlPoints.Add(lineRenderer.GetPosition(i));
        }

        // Generate the smoothed curve
        GenerateSmoothCurve();

        // Update the LineRenderer with the smoothed curve
        lineRenderer.positionCount = smoothedPoints.Count;
        lineRenderer.SetPositions(smoothedPoints.ToArray());
    }

    void GenerateSmoothCurve()
    {
        smoothedPoints.Clear();

        // Ensure we have enough points to interpolate
        if (controlPoints.Count < 2)
        {
            Debug.LogError("Not enough points to create a curve.");
            return;
        }

        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Vector3 p0 = i > 0 ? controlPoints[i - 1] : controlPoints[i]; // Previous point or current point
            Vector3 p1 = controlPoints[i];                               // Current point
            Vector3 p2 = controlPoints[i + 1];                           // Next point
            Vector3 p3 = i + 2 < controlPoints.Count ? controlPoints[i + 2] : controlPoints[i + 1]; // Next-next point or repeat next point

            // Interpolate between p1 and p2
            for (int j = 0; j < resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 interpolatedPoint = CatmullRom(t, p0, p1, p2, p3);
                smoothedPoints.Add(interpolatedPoint);
            }
        }

        // Add the final control point to ensure the curve ends at the last point
        smoothedPoints.Add(controlPoints[controlPoints.Count - 1]);
    }

    Vector3 CatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Catmull-Rom spline formula
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            2f * p1 +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }
}
