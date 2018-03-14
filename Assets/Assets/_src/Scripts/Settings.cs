using UnityEngine;
using System.Collections;


public class Settings : MonoBehaviour
{
    [Tooltip("Choose which GameSettings to Use")]

    public GameSettings _settings;// Use this for initialization
    [SerializeField]
    private static GameSettings s;
    private static Settings instance;

    public static Settings Instance
    {
        get
        {
            return Instance;
        }
    }

    public static GameSettings gameSettings {
        get {
            return s;
        }
    }
	private void Awake()
	{
        DontDestroyOnLoad(gameObject);
        if(Settings.instance == null) {
            Settings.instance = this;
        } else {
            Debug.LogWarning("A previous awakened Settings Monobehaviour exists.");
        }
        if(Settings.s == null) {
            Settings.s = _settings;
        }
	}
}
