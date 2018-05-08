using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour {

    public GameObject HomePanel;
    public Button StartButton;
    public Button SettingsButton;
    public Button PickCharacterButton;

    public GameObject SettingsPanel;
    public Toggle SkipTutorialToggle;
    public InputField IpAddress;
    public Dropdown IpAddressDropDown;
    public Button SettingsOkButton;

    public GameObject PickCharacterPanel;
    public Button pickCharacterOkButton;

    private GameSettings settings;

    [SerializeField]
    private string gameSceneName;

    public void ShowHomePanel() {
        HomePanel.SetActive(true);
        SettingsPanel.SetActive(false);
        PickCharacterPanel.SetActive(false);
    }

    public void ShowSettingsPanel() {
        HomePanel.SetActive(false);
        SettingsPanel.SetActive(true);
        PickCharacterPanel.SetActive(false);
        FillSettingsForm();
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

	private void OnEnable()
	{
        if (Settings.gameSettings != null)
        {
            settings = Settings.gameSettings;
        }
        else
        {
            settings = ScriptableObject.CreateInstance<GameSettings>();
        }
	}
	public void handleDropdownSelection(int optionId) {
        if(IpAddressDropDown == null) {
            return;
        }

        Dropdown.OptionData data = IpAddressDropDown.options[optionId];
        IpAddress.text = data.text;
        IpAddress.textComponent.text = data.text;
    }

    public void handleCharacterSelection(int id) {
        Debug.Log("saving character choice:" + id);
        Debug.Log(settings);
        try
        {
            Settings.gameSettings.CharacterId = id;
        }catch(Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
    public void FillSettingsForm()
    {
        IpAddress.text = settings.IpAddress;
        SkipTutorialToggle.isOn = settings.skipTutorial;
    }
    public void SaveSettings() 
    {
        Debug.Log(IpAddress.textComponent.text);
        settings.IpAddress = IpAddress.textComponent.text ?? "localhost";
        settings.skipTutorial = SkipTutorialToggle.isOn;
        Debug.Log("Setting ip to: " + settings.IpAddress);
        ShowHomePanel();
        Debug.Log(settings);
    }

    public void StartGameFromTutorial()
    {
        SceneManager.LoadScene(gameSceneName ?? "introScene");
    }
    public void StartGame() {
        if(settings.skipTutorial) {
            StartGameOnMultiplayer();
        } else {
            StartGameFromTutorial();
        }
    }
    public void StartGameOnMultiplayer() {
        NetworkManager.singleton.networkAddress = settings.IpAddress;
        NetworkManager.singleton?.StartClient();
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartServer();
    }
}
