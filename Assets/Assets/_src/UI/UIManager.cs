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
        Settings.gameSettings.CharacterId = id;
    }
    public void FillSettingsFom()
    {
        IpAddress.text = settings.IpAddress;
    }
    public void SaveSettings() 
    {
        Debug.Log(IpAddress.textComponent.text);
        settings.IpAddress = IpAddress.textComponent.text ?? "localhost";
        Debug.Log("Setting ip to: " + settings.IpAddress);
        ShowHomePanel();
        Debug.Log(settings);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName ?? "introScene");
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartServer();
    }
}
