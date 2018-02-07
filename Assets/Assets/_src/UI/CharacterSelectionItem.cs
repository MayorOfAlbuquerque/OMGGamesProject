using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionItem : MonoBehaviour {
    public TextMesh fullName;
    public TextMesh nickname;
    public MeshFilter model;
    public int characterId = -1;
	// Use this for initialization
    public void SetCharacter(CharacterSpec spec) {
        fullName.text = spec.FullName;
        nickname.text = spec.NickName;
        model.mesh = spec.CharacterPrefab.GetComponent<MeshFilter>().sharedMesh;
        characterId = spec.Id;
    }

    public void NotifyCharacterSelected() {
        Debug.Log($"character selected {characterId}");
    }

    private void OnMouseDown()
    {
        NotifyCharacterSelected();
    }
}
