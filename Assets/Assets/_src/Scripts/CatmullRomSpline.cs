using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Catmull rom spline, a type of cubic spline, defines a path in 3D.
/// Needs at least 4 control points.
/// </summary>
public class CatmullRomSpline : MonoBehaviour {
    public List<Vector3> controlPoints;
    public int interpolationSteps;
    public bool closedCurve;
    // Use this for initialization
    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(2.5f, 2.5f, 2.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, size);
        Gizmos.color = Color.blue;
        foreach (Vector3 p in controlPoints) 
        {
            Gizmos.DrawCube(p + transform.position, size);
        }

        DrawCurve(this);
    }

    private void DrawCurve(CatmullRomSpline spline)
    {
        int steps = spline.interpolationSteps;
        int segments = spline.GetSegmentCount();

        if (spline.controlPoints.Count < 4 || steps < 1)
        {
            return;
        }

        Vector3 start = spline.GetCurvePoint(0, 0.0f) + spline.transform.position;
        Vector3 end;

        float timestep = 1.0f / (float)steps;
        for (int i = 0; i < segments; i++)
        {
            for (float t = 0.0f + timestep; t <= 1.0f; t += timestep)
            {
                end = spline.GetCurvePoint(i, t) + spline.transform.position;
                Gizmos.DrawLine(start, end);
                start = end;
            }
        }
    }
    public void Reset()
    {
        controlPoints = new List<Vector3>()
        {
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 2.0f),
            new Vector3(0.0f, 0.0f, 3.0f),
            new Vector3(0.0f, 0.0f, 3.0f)
        };
        interpolationSteps = 5;
        closedCurve = false;
    }

    public Vector3 GetCurvePoint(int segment, float t)
    {
        Vector3 p0 = controlPoints[segment];
        Vector3 p1 = controlPoints[segment + 1];
        Vector3 p2 = controlPoints[segment + 2];
        Vector3 p3 = controlPoints[segment + 3];

        Vector3 c0 = 2 * p1;
        Vector3 c1 = -1 * p0 + p2;
        Vector3 c2 = 2 * p0 - 5 * p1 + 4 * p2 - p3;
        Vector3 c3 = -1 * p0 + 3 * p1 - 3 * p2 + p3;

        Vector3 v = 0.5f * (c0 + t * (c1 + t * (c2 + (c3 * t))));
        return v;
    }
    public Vector3 GetTangent(int segment, float t)
    {
        Vector3 p0 = controlPoints[segment];
        Vector3 p1 = controlPoints[segment + 1];
        Vector3 p2 = controlPoints[segment + 2];
        Vector3 p3 = controlPoints[segment + 3];

        Vector3 c0 = 2 * p1;
        Vector3 c1 = -1 * p0 + p2;
        Vector3 c2 = 2 * p0 - 5 * p1 + 4 * p2 - p3;
        Vector3 c3 = -1 * p0 + 3 * p1 - 3 * p2 + p3;

        Vector3 dv = ((3 * t * c3) * t + 2 * t * c2) + c1;
        return dv;
    }

    public int GetSegmentCount()
    {
        return closedCurve ? controlPoints.Count : controlPoints.Count - 3;
    }

}
