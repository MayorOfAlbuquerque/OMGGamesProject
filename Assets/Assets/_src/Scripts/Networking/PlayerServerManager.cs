using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerServerManager : MonoBehaviour
{
    public CharacterList list;
    private Dictionary<int, CharacterSpec> charactersPlaying =
        new Dictionary<int, CharacterSpec>();


    public void RegisterHandlers()
    {
        NetworkServer.RegisterHandler(PlayerJoinMessage.MESSAGE_TYPE, OnPlayerJoin);
    }

    public void RegisterPlayerPrefabs()
    {
        foreach (var spec in list.Characters)
        {
            if (spec.PlayablePrefab != null)
            {
                ClientScene.RegisterPrefab(spec.PlayablePrefab);
            }
        }
    }


    public void OnPlayerJoin(NetworkMessage message)
    {
        PlayerJoinMessage joinMessage = message.ReadMessage<PlayerJoinMessage>();
        if (joinMessage == null)
        {
            return;
        }

        AddCharacter((int)joinMessage.characterId);
    }
    public void RemoveCharacter()
    {

    }
    public void AddCharacter(int id)
    {
        CharacterSpec spec = list.GetCharacterById(id);
        if (spec == null)
        {
            return;
        }
        charactersPlaying[id] = spec;
    }

    public void RemoveCharacter(int id)
    {
        if (charactersPlaying.ContainsKey(id))
        {
            charactersPlaying.Remove(id);
        }
    }

    public bool IsPlayerJoined(int id) {
        return charactersPlaying.ContainsKey(id);
    }
    public Dictionary<int, CharacterSpec> CharactersPlaying() {
        return charactersPlaying;
    }

    public CharacterSpec FindCharacterSpecById(int id) {
        return list.GetCharacterById(id);
    }

    public GameObject InstantiateCharacter(CharacterSpec spec, GameObject backupPrefab, Transform t) {

        GameObject prefab = spec.PlayablePrefab ?? backupPrefab;
        GameObject temp =  Instantiate(prefab);
        temp.transform.position = t.position; 
        return temp;
    }
}
