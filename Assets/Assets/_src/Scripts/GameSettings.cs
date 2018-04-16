using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "DefaultSettings", menuName = "MurderMystery/Game Settings")]
public class GameSettings : ScriptableObject
{
    public string IpAddress = "localhost";
    public int MusicVolume = 10;
    public int SoundFxVolume = 10;
    public int CharacterId = 1;
    public bool skipTutorial = false;
    public override string ToString()
    {
        return base.ToString() + $"[IpAddress={IpAddress}, MusicVolume={MusicVolume}, SoundFxVolume={SoundFxVolume}, CharacterId={CharacterId}]";
	}
}