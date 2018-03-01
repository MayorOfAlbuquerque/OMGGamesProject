using System;
using UnityEngine;

[ExecuteInEditMode]
public class CluePlaceholder : MonoBehaviour{
    public ClueSpec Clue;
    public bool previewClue = false;
    private Color placeholderColour = new Color(1.0f, 0.0f, 0.0f, 0.75f);

    private GameObject prefabInstance;

    void Start()
    {
        InstantiateCluePrefab();
    }

    public bool IsCluePrivate()
    {
        return Clue.PrivateClue;
    }

    void InstantiateCluePrefab() {
        RemovePrefabs();
        if (Clue != null && Clue.ModelPrefab != null && prefabInstance == null)
        {
            prefabInstance = Instantiate(Clue.ModelPrefab);
            prefabInstance.transform.position = transform.position;
            prefabInstance.transform.parent = transform;
            prefabInstance.SetActive(false);
        }
    }
    /*void Update() {
        if(previewClue && prefabInstance == null) {
            InstantiateCluePrefab();
            prefabInstance.SetActive(true);
        } else if(previewClue) {
            prefabInstance.SetActive(true);
        } else {
            prefabInstance.SetActive(false);
        }
    }*/
    void OnDrawGizmos() 
    {
        if (!previewClue)
        {
            Gizmos.DrawIcon(transform.position, "question_mark.png", false);
        }
    }

    private void OnDisable()
    {
        RemovePrefabs();
    }

    void RemovePrefabs() {
        foreach (Transform child in transform)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(child.gameObject);

            }
            else
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        RemovePrefabs();
    }
}