using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickerController : MonoBehaviour {

    public GameObject CharacterPickerPanel;
    public CharacterList PlayableCharacters;
    public GameObject CharacterModelView;
    public GameObject PickerModelPrefab;
    private GameObject[] models;

	// Use this for initialization
	void Start () {
        
	}

    private void RenderModels() {
        models = new GameObject[PlayableCharacters.Characters.Count];
        List<CharacterSpec> chars = PlayableCharacters.Characters;
        Vector3 textOffset = new Vector3(-3.0f, 0.3f, 0.0f);
        for (int i = 0; i < models.Length; i++)
        {
            models[i] = Instantiate(PickerModelPrefab);
            models[i].transform.position = new Vector3(i * 4, 0, 2);
            var item = models[i].GetComponent<CharacterSelectionItem>();
            item?.SetCharacter(chars[i]);
        }
    }
    private void DestroyModels() {
        if(models!=null) {
            for (int i = 0; i < models.Length; i++)
            {
                Destroy(models[i]);
                models[i] = null;
            }
        }
    }
    private void OnEnable()
    {
        // draw character screen
        List<CharacterSpec> chars = PlayableCharacters.Characters;
        for (int i = 0; i < chars.Count; i++)
        {
            Debug.Log(chars[i].FullName);
        }
        RenderModels();
        foreach(GameObject obj in models) {
            obj.transform.parent = CharacterModelView.transform;
            obj.transform.position += CharacterModelView.transform.position;
        }
        CharacterModelView?.SetActive(true);
    }

    private void OnDisable()
    {
        DestroyModels();
        CharacterModelView?.SetActive(false);
    }
}
