using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CluePlaceholder))]
public class CluePlaceholderInspector : Editor
{
    private Vector3 placeholderSize = new Vector3(2,2,2);
	[MenuItem("Tools/MyTool/Do It in C#")]
	static void DoIt()
	{
		EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
	}


    void OnSceneGUI() {

    }
}
