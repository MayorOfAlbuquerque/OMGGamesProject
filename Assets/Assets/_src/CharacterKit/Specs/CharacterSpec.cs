using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pirate", menuName = "MurderMystery/Character", order = 1)]
public class CharacterSpec : ScriptableObject
{
    public string FullName = "Captain Hook";
    public string NickName = "Hook";
    public int age = 35;
    public string Bio = "Attack!!!";
    public GameObject CharacterPrefab = null;
}
