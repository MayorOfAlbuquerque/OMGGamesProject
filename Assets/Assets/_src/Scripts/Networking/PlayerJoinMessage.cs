using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerJoinMessage : MessageBase {
    public static readonly short MESSAGE_TYPE = 3001;

    public Int32 connectionId = 0;
    public Int32 characterId = 0;

    public PlayerJoinMessage() {
        
    }

    public PlayerJoinMessage(int connectionId, int characterId) {
        this.connectionId = connectionId;
        this.characterId = characterId;
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(connectionId);
        writer.Write(characterId);
    }

    public override void Deserialize(NetworkReader reader)
    {
        connectionId = reader.ReadInt32();
        characterId = reader.ReadInt32();
    }
}