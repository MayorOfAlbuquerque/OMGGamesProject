using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CatmullRomSpline))]
public class SplineInspector : Editor
{
    private void OnSceneGUI()
    {
        CatmullRomSpline curve = target as CatmullRomSpline;
        Transform handleTransform = curve.transform;
        Quaternion rotation = handleTransform.rotation;
        Vector3[] pointsInWorld = new Vector3[curve.controlPoints.Count];

        for (int i = 0; i < curve.controlPoints.Count; i++)
        {
            pointsInWorld[i] =
                handleTransform.TransformPoint(curve.controlPoints[i]);
            CheckForCurvePointChanges(
                curve,
                i,
                ref pointsInWorld[i]
            );
        }

        DrawCurve(curve);
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Reverse Direction"))
        {
            (target as CatmullRomSpline)?.controlPoints.Reverse();
        }
    }
    private void CheckForCurvePointChanges(CatmullRomSpline curve,
                                           int curvePointIndex,
                                           ref Vector3 transformedControlPoint)
    {
        EditorGUI.BeginChangeCheck();

        Vector3 repositioned = Handles.DoPositionHandle(
            transformedControlPoint,
            curve.transform.rotation
        );

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curve, "Move Control Point");
            curve.controlPoints[curvePointIndex]
                 = curve.transform.InverseTransformPoint(repositioned);
            EditorUtility.SetDirty(curve);
        }
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
                Handles.DrawLine(start, end);
                start = end;
            }
        }
    }
}
