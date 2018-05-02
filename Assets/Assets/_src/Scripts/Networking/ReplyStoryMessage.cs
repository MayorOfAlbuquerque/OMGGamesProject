using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ReplyStoryMessage : MessageBase
{
    public static readonly short MESSAGE_TYPE = 3003;
    public Int32 storyId = -1;

    public ReplyStoryMessage() {}
    public ReplyStoryMessage(int storyId) { this.storyId = storyId; }

	public override void Deserialize(NetworkReader reader)
	{
        base.Deserialize(reader);
        storyId = reader.ReadInt32();
	}
	public override void Serialize(NetworkWriter writer)
	{
        writer.Write(storyId);
        base.Serialize(writer);
	}
}
