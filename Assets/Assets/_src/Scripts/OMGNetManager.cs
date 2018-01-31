using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using VoiceChat.Networking;


namespace VoiceChat.Demo.HLAPI
{
    public class OMGNetManager : NetworkManager
    {
        //PlayerVoiceChat this_instance;
        public override void OnStartClient(NetworkClient client)
        {
            VoiceChatNetworkProxy.OnManagerStartClient(client);
            base.OnStartClient(client);
        }

        public override void OnStopClient()
        {
            //base.OnStopClient();
            VoiceChatNetworkProxy.OnManagerStopClient();

            if (client != null)
            {
              Destroy(GetComponent<PlayerVoiceChat>());
            }

        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            base.OnServerDisconnect(conn);
            VoiceChatNetworkProxy.OnManagerServerDisconnect(conn);
        }

		/* Called from NetworkManager.StartServer()
		*  This is also the implementation for a Host
		*/
        public override void OnStartServer()
        {
		//	base.OnStartServer(); //Default implementation
			VoiceChatNetworkProxy.OnManagerStartServer(); //Voice chat implementation
        }

        public override void OnStopServer()
        {
            //base.OnStopServer();
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
