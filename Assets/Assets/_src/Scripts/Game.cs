using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    [SerializeField]
    public string entryScene;

	private void Awake()
	{
        DontDestroyOnLoad(this);
	}
	// Use this for initialization
	void Start () {
        SceneManager.LoadSceneAsync(entryScene);
	}
}
