using System;
using UnityEngine;

[ExecuteInEditMode]
public class CluePlaceholder : MonoBehaviour{
    public ClueSpec Clue;
    public bool showClueModel = false;
    private Color placeholderColour = new Color(1.0f, 0.0f, 0.0f, 0.75f);
    void OnDrawGizmos() 
    {
        if (showClueModel && Clue != null)
        {
            Mesh clueMesh = Clue
                .ModelPrefab
                .GetComponent<MeshFilter>()
                .sharedMesh;
            Gizmos.DrawMesh(clueMesh, transform.position, transform.rotation, transform.localScale);
        }
        else
        {
            Gizmos.color = placeholderColour;
            Gizmos.DrawIcon(transform.position, "question_mark.png", false);
        }
    }
}