using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject HomePanel;
    public Button StartButton;
    public Button SettingsButton;


    public GameObject SettingsPanel;
    public InputField IpAddress;
    public Button SettingsOkButton;


    public void ShowHomePanel() {
        HomePanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void ShowSettingsPanel() {
        HomePanel.SetActive(false);
        SettingsPanel.SetActive(true);
        FillSettingsFom();
    }
	// Use this for initialization
	void Start () {
        HomePanel.SetActive(true);
        SettingsPanel.SetActive(false);
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
