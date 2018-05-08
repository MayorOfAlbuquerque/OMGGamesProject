using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneReference), true)]
public class SceneReferenceEditor : Editor
{
    public override void OnInspectorGUI()
	{
        var reference = target as SceneReference;
        if(reference == null) {
            reference = SceneReference.CreateInstance<SceneReference>();
        }
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(reference.scenePath);

        serializedObject.Update();
        var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;
        var newPath = AssetDatabase.GetAssetPath(newScene);
        reference.sceneName = newScene.name;
        reference.scenePath = newPath;
        EditorUtility.SetDirty(reference);
	}
}
