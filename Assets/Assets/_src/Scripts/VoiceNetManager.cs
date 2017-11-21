using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;
using VoiceChat;
using VoiceChat.Networking.Legacy;

public class NetManager : NetworkManager {

    VoiceChatNetworkProxy proxy; // for voice stuff

    void OnConnectedToServer()
    {
        proxy = VoiceChatNetworkUtils.CreateProxy(); // For voice stuff

    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        GameObject.Destroy(proxy.gameObject); // for voice stuff
        /* PS Sorry for this confusion, we're not sure where any of this goes*/
    }

    public NetManager()
    {
        
    }

    public override void OnStartServer()
    {
        
        base.OnStartServer();
    }

    

    //Called whenever a client joins the server
    public override void OnServerConnect(NetworkConnection conn)
    {
        
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log(conn.playerControllers);


        NetworkServer.DestroyPlayersForConnection(conn);
        base.OnServerDisconnect(conn);
       
    }
}