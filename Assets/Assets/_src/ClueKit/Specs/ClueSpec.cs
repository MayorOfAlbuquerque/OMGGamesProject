using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "A Clue", menuName = "MurderMystery/Clue", order = 2)]
public class ClueSpec : ScriptableObject {
    public string Name;
    public string Description;
    public string GeneralDisplayText;
    public bool PrivateClue;
    public CharacterSpec Character;
    public string PrivateDisplayText;
    public GameObject ModelPrefab;
}
