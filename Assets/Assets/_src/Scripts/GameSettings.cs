using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings{
    private static GameSettings instance;

    public string IpAddress = "localhost";
    public int MusicVolume = 10;
    public int SoundFxVolume = 10;

    public static GameSettings Instance
    {
        get 
        {
            if(instance == null)
            {
                instance = new GameSettings();
            }
            return instance;
        }
    }
}