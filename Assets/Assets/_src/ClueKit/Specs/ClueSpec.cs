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

    public bool AltPrivateClue1;
    public CharacterSpec AltCharacter1;
    public string AltPrivateDisplayText1;
    public bool AltPrivateClue2;
    public CharacterSpec AltCharacter2;
    public string AltPrivateDisplayText2;
    public bool AltPrivateClue3;
    public CharacterSpec AltCharacter3;
    public string AltPrivateDisplayText3;
    public GameObject ModelPrefab;

}
