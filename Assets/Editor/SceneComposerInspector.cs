using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SceneComposer))]
public class SceneComposerInspector : Editor {

    private ReorderableList reorderableList;

    private SceneComposer SceneComposer {
        get {
            return target as SceneComposer;
        }
    }

	private void OnEnable()
	{
        reorderableList = new ReorderableList(SceneComposer.scenes, typeof(SceneReference), true, true, true, true);
        reorderableList.drawHeaderCallback += DrawHeader;
        reorderableList.drawElementCallback += DrawElement;
        reorderableList.onAddCallback += OnAddItem;
        reorderableList.onRemoveCallback += OnRemoveItem;
        reorderableList.onReorderCallback += OnReorder;
	}
	private void OnDisable()
	{
        reorderableList.drawHeaderCallback -= DrawHeader;
        reorderableList.drawElementCallback -= DrawElement;
        reorderableList.onAddCallback -= OnAddItem;
        reorderableList.onRemoveCallback -= OnRemoveItem;
        reorderableList.onReorderCallback -= OnReorder;
	}
    private void OnReorder(ReorderableList list) {
        
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Parts of Multi-scene Game:");
    }
    private void DrawElement(Rect rect, int index, bool active, bool focused) {
        var sceneRef = SceneComposer.scenes[index];

        EditorGUI.BeginChangeCheck();
        sceneRef.name = EditorGUI.TextField(new Rect(rect.x + 18, rect.y, rect.width - 18, rect.height), sceneRef.name);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }
    private void OnAddItem(ReorderableList list) {
        SceneComposer.scenes.Add(new SceneReference());
    }
    private void OnRemoveItem(ReorderableList list) {

        SceneComposer.scenes.RemoveAt(list.index);
        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Actually draw the list in the inspector
        reorderableList.DoLayoutList();
    }
}
