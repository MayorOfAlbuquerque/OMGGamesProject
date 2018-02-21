using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelectionItem : MonoBehaviour {
    public float modelScale = 3.0f;
    public TextMesh fullName;
    public GameObject model;
    public int characterId = -1;
    public bool selected = false;
    // Use this for initialization
    public CharacterSelectEvent characterSelected;
    private LayerMask uiLayerMask = 5;
    private GameObject characterModel = null;
    void SetupEvent()
    {
        if (characterSelected == null)
        {
            characterSelected = new CharacterSelectEvent();
        }
    }
    public void SetCharacter(CharacterSpec spec, bool selected = false) {
        fullName.text = spec.FullName;
        this.selected = selected;
        if (characterModel == null)
        {
            characterModel = Instantiate(spec.CharacterPrefab);
            characterId = spec.Id;
            characterModel.transform.parent = model.transform;
            characterModel.transform.position = model.transform.position;
            characterModel.transform.localScale *= modelScale;
            characterModel.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        UpdateSelected(selected);
    }

    public void UpdateSelected(bool selected) {
        Debug.Log("updating character name text colour");
        if (selected)
        {
            fullName.color = Color.white;
        }
        else
        {
            fullName.color = Color.gray;
        }
    }
    public void NotifyCharacterSelected() {
        Debug.Log($"character selected {characterId}");
        characterSelected.Invoke(characterId);
    }

    void Update()
    {
        
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
#else
        if(Input.GetMouseButtonDown(0)) {
#endif
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(worldPosition, Vector3.forward, 100, uiLayerMask))
            {
                NotifyCharacterSelected();
            }
        }
    }

    void OnMouseDown()
    {
        NotifyCharacterSelected();
    }
}
