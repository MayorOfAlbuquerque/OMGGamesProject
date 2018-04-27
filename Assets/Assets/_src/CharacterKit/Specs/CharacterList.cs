using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "MurderMystery/Character List", order = 3)]
public class CharacterList : ScriptableObject
{
    public List<CharacterSpec> Characters;

    public CharacterSpec GetCharacterById(int id)
    {
        foreach (var spec in Characters)
        {
            if (spec.Id == id)
            {
                return spec;
            }
        }
        return null;
    }

    public static CharacterList Empty {
        get{
            CharacterList list = CreateInstance<CharacterList>();
            list.Characters = new List<CharacterSpec>();
            return list;
        }
    }
}
