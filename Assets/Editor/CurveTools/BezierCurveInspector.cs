using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{

    private void OnSceneGUI()
    {
        BezierCurve curve = target as BezierCurve;
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

        DrawCurve(curve.steps, pointsInWorld);
    }


    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if(GUILayout.Button("Reverse Direction")){
            (target as BezierCurve).controlPoints.Reverse();
        }
    }
    private void CheckForCurvePointChanges(BezierCurve curve, 
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

    private void DrawCurve(int steps, Vector3[] points)
    {
        if (points.Length < 2 || steps < 1)
        {
            return;
        }
        Debug.Log("steps: " + steps);
        float timestep = 1.0f / (float) steps;
        Vector3 start = BezierCurve.Interpolate(points[0], points[1], points[2], 0.0f);
        Vector3 end;
        for (float t = 0.0f + timestep; t <= 1.0f; t+= timestep)
        {
            end = BezierCurve.Interpolate(points[0], points[1], points[2], t);
            Handles.DrawLine(start, end);
            start = end;
        }
    }
}
