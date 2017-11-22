using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using VoiceChat.Networking;

namespace VoiceChat.Demo.HLAPI
{
    public class VoiceChatNetworkManager : NetworkManager
    {
        //PlayerVoiceChat this_instance;
        public override void OnStartClient(NetworkClient client)
        {
            base.OnStartClient(client);
            VoiceChatNetworkProxy.OnManagerStartClient(client);
            Debug.Log("No problemo with Proxyo");
            //this_instance = new PlayerVoiceChat();

            gameObject.AddComponent<PlayerVoiceChat>(); // Used to be VoiceChatUI i
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            VoiceChatNetworkProxy.OnManagerStopClient();

            if (client != null)
            {
              Destroy(GetComponent<PlayerVoiceChat>());
          //      this_instance = null;
            }

        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);

            VoiceChatNetworkProxy.OnManagerServerDisconnect(conn);
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            VoiceChatNetworkProxy.OnManagerStartServer();

            //gameObject.AddComponent<VoiceChatServerUi>();
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            VoiceChatNetworkProxy.OnManagerStopServer();

            //Destroy(GetComponent<VoiceChatServerUi>());
        }

        public override void OnClientConnect(NetworkConnection connection)
        {
            base.OnClientConnect(connection);
            VoiceChatNetworkProxy.OnManagerClientConnect(connection);
        }
    }
}
