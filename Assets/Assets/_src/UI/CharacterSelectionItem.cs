using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelectionItem : MonoBehaviour {
    public TextMesh fullName;
    public TextMesh nickname;
    public MeshFilter model;
    public int characterId = -1;
    // Use this for initialization
    public CharacterSelectEvent characterSelected;
    void SetupEvent()
    {
        if (characterSelected == null)
        {
            characterSelected = new CharacterSelectEvent();
        }
    }
    public void SetCharacter(CharacterSpec spec) {
        fullName.text = spec.FullName;
        nickname.text = spec.NickName;
        model.mesh = spec.CharacterPrefab.GetComponent<MeshFilter>().sharedMesh;
        characterId = spec.Id;
    }

    public void NotifyCharacterSelected() {
        Debug.Log($"character selected {characterId}");
        characterSelected.Invoke(characterId);
    }

    private void OnMouseDown()
    {
        NotifyCharacterSelected();
    }
}
