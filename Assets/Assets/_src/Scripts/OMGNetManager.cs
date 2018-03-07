using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OMGNetManager : NetworkManager
{
    public CharacterList PlayableCharacters;
    private Dictionary<int, CharacterSpec> characterSpecs = new Dictionary<int, CharacterSpec>();

    int numOfConnectedPlayers;
    int[] listOfPlayersVotes = new int[10];

    int numberOfPlayersVoted;

    public override void OnStartServer()
    {
        
        if(PlayableCharacters != null)
        {
            LoadCharacterList(PlayableCharacters);
        }
    }

    private void LoadCharacterList(CharacterList list)
    {
        foreach (var character in list.Characters)
        {
            characterSpecs[character.Id] = character;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if(extraMessageReader == null) {
            return;
        }
        PlayerJoinMessage message = extraMessageReader.ReadMessage<PlayerJoinMessage>();
        if(message == null) {
            return;
        }
        //CharacterSpec spec = characterSpecs[(uint)message.characterId];

    }

    
}

