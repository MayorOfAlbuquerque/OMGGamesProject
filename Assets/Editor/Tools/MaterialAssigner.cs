using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MaterialAssigner : EditorWindow
{
    public Material material;

    public GameObject target;

    [MenuItem("MurderMystery/MaterialAssigner")]
    static void Init() {
        var window = GetWindowWithRect<MaterialAssigner>(new Rect(0, 0, 365, 140));
        window.Show();
    }
    void OnGUI()
    {
        
        EditorGUILayout.LabelField($"Material Assigner");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Choose a material");
        material = (Material) EditorGUILayout.ObjectField(material, typeof(Material), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Choose a GameObject");
        target = (GameObject) EditorGUILayout.ObjectField(target, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Go")) {
            Debug.Log("assigning materialzzzz.");
            if (TraverseAndAssign(target, material))
            {
                Close();
            }
        }
    }

    public bool TraverseAndAssign(GameObject obj, Material mat) {
        if(obj == null || mat == null) {
            return false;
        }

        var renderers = obj.GetComponentsInChildren<MeshRenderer>(true);
        foreach (var renderer in renderers)
        {
            renderer.material = mat;
        }
        return true;
    }
}
