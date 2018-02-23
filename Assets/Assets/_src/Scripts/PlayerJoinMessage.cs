using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerJoinMessage : MessageBase {
    public const ushort messageType = 1001;

    public uint connectionId;
    public uint characterId;

    public override void Serialize(NetworkWriter writer)
    {
        writer.WritePackedUInt32(connectionId);
        writer.WritePackedUInt32(characterId);
    }

    public override void Deserialize(NetworkReader reader)
    {
        connectionId = reader.ReadUInt32();
        characterId = reader.ReadUInt32();
    }
}