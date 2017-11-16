using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierCurve : MonoBehaviour
{
    public List<Vector3> controlPoints;
    public int steps;

    public void Reset() {
        controlPoints = new List<Vector3>() {
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 2.0f),
            new Vector3(0.0f, 0.0f, 3.0f)
        };
        steps = 10;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(2.5f, 2.5f, 2.5f);
        Gizmos.color = Color.blue;

        if (controlPoints.Count < 2)
        {
            return;
        }
        Gizmos.DrawCube(transform.TransformPoint(controlPoints[0]), size);
        for (int i = 1; i < controlPoints.Count; i++)
        {
            Gizmos.DrawCube(transform.TransformPoint(controlPoints[i]), size);
        }
    }

    /// <summary>
	/// Quadratic interpolation. 
    /// Return points on the curve with control points p1, p2, p3 and parameter t.
    /// </summary>
    /// <returns>The interpolate.</returns>
    /// <param name="p1">P1.</param>
    /// <param name="p2">P2.</param>
    /// <param name="p3">P3.</param>
    /// <param name="t">T.</param>
    public static Vector3 Interpolate(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        return (1.0f - t) * (1.0f - t) * p1 + 2.0f * t * (1.0f - t) * p2 + t * t * p3;
    }

    public static Vector3 FirstDerivative(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 2.0f * (1f - t) * (p2 - p1) + 2.0f * t * (p3 - p2);
    }
}