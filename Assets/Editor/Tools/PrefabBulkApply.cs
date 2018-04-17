using UnityEngine;
using System.Collections;
using UnityEditor;

public class PrefabBulkApply : MonoBehaviour
{
    [MenuItem("MurderMystery/Bulk Apply Prefabs")]
    static void PrefabsApply() {
        if(Selection.gameObjects.Length < 1) {
            EditorUtility.DisplayDialog("No Prefabs selected", "Select prefabs from the scene and try again.", "Ok");
            return;
        }
        foreach (var obj in Selection.gameObjects)
        {
            Object parent = PrefabUtility.GetPrefabParent(obj);
            if(parent == null){
                continue;
            }
            GameObject go = PrefabUtility.FindValidUploadPrefabInstanceRoot(obj);
            PrefabUtility.ReplacePrefab(go, parent, ReplacePrefabOptions.Default);
        }
    }
}
