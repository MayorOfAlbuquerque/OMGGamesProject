using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "MurderMystery/Character List", order = 3)]
public class CharacterList : ScriptableObject {
    public List<CharacterSpec> Characters;
}
