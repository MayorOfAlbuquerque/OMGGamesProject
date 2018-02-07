using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject HomePanel;
    public Button StartButton;
    public Button SettingsButton;
    public Button PickCharacterButton;

    public GameObject SettingsPanel;
    public InputField IpAddress;
    public Button SettingsOkButton;

    public GameObject PickCharacterPanel;
    public Button pickCharacterOkButton;

    public void ShowHomePanel() {
        HomePanel.SetActive(true);
        SettingsPanel.SetActive(false);
        PickCharacterPanel.SetActive(false);
    }

    public void ShowSettingsPanel() {
        HomePanel.SetActive(false);
        SettingsPanel.SetActive(true);
        PickCharacterPanel.SetActive(false);
        FillSettingsFom();
    }

    public void ShowPickCharacterPanel() {
        HomePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PickCharacterPanel.SetActive(true);
    }
	// Use this for initialization
	void Start () {
        ShowHomePanel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FillSettingsFom()
    {
        IpAddress.text = GameSettings.Instance.IpAddress;
    }
    public void SaveSettings() 
    {
        Debug.Log(IpAddress.textComponent.text);
        GameSettings.Instance.IpAddress = IpAddress.textComponent.text ?? "localhost";
        ShowHomePanel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("introScene");
    }
}
