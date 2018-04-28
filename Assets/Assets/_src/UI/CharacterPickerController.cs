using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickerController : MonoBehaviour {

    public GameObject CharacterPickerPanel;
    public CharacterList PlayableCharacters;
    public GameObject CharacterModelView;
    public GameObject PickerModelPrefab;
    private GameObject[] models;
    private GameSettings settings;
    public float distanceBetweenItems = 4.0f;
	// Use this for initialization
	void Start () {

	}

    private void RenderModels() {
        Debug.Log(Settings.gameSettings);
        int selectCharacterId = Settings.gameSettings.CharacterId;
        models = new GameObject[PlayableCharacters.Characters.Count];
        List<CharacterSpec> chars = PlayableCharacters.Characters;
        Vector3 textOffset = new Vector3(-3.0f, 0.3f, 0.0f);
        float totalWidth = (models.Length - 1) * distanceBetweenItems;
        for (int i = 0; i < models.Length; i++)
        {
            models[i] = Instantiate(PickerModelPrefab);
            models[i].transform.position = new Vector3(i * 4.0f - totalWidth * 0.5f, -1.5f, 2.0f);
            var item = models[i].GetComponent<CharacterSelectionItem>();
            item?.SetCharacter(chars[i], chars[i].Id == selectCharacterId);
            item.characterSelected.AddListener(UpdateItems);
        }
    }
    private void DestroyModels() {
        for (int i = 0; i < models.Length; i++)
        {
            if(models[i] != null){
                Destroy(models[i]);
                models[i] = null;
            }
        }
    }
    public void UpdateItems(int newId) {
        
        List<CharacterSpec> chars = PlayableCharacters.Characters;
        Debug.Log($"update items: {models.Length}");
        for (int i = 0; i < models.Length; i++) {
            
            Debug.Log($"{i}: {chars[i].Id == newId}");
            models[i]
                .GetComponent<CharacterSelectionItem>()
                .UpdateSelected(chars[i].Id == newId);
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
        try
        {
            Debug.Log("picker disabled");
            DestroyModels();
            CharacterModelView?.SetActive(false);
        }catch(MissingReferenceException e) {
            
        }
    }
}
