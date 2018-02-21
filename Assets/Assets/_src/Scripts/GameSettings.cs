using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSettings", menuName = "MurderMystery/Game Settings")]
public class GameSettings : ScriptableObject{
    public string IpAddress = "localhost";
    public int MusicVolume = 10;
    public int SoundFxVolume = 10;
    public int CharacterId = 1;
}