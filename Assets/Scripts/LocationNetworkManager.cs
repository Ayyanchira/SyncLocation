using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocationNetworkManager : NetworkManager
{

    protected static short messageID = 777;
    public GameObject playerObject;
    static public GameObject[] clientsRawData = new GameObject[8];
    [SerializeField] GameObject WorldMap;

    public class CustomMessage : MessageBase
    {
        public string deviceType, purpose, ipAddress;
        public Vector3 devicePosition;
        public Quaternion deviceRotation;
    }

    public class ClientLocations : MessageBase
    {
        public Vector3 devicePosition1;
        public Quaternion deviceRotation1;
        public Vector3 devicePosition2;
        public Quaternion deviceRotation2;
        public Vector3 devicePosition3;
        public Quaternion deviceRotation3;
        public Vector3 devicePosition4;
        public Quaternion deviceRotation4;
        public Vector3 devicePosition5;
        public Quaternion deviceRotation5;
        public Vector3 devicePosition6;
        public Quaternion deviceRotation6;
        public Vector3 devicePosition7;
        public Quaternion deviceRotation7;
        public Vector3 devicePosition8;
        public Quaternion deviceRotation8;

    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsClientConnected())
        {
            if (playerObject == null)
            {
                playerObject = GameObject.Instantiate(playerPrefab, new Vector3(5, 1, -6), Quaternion.identity);

            }
            else
            {
                updateLocation();
            }
        }
        else
        {
            print("Not connected to server...");
        }
    }

    void updateLocation()
    {
        var msg = new CustomMessage();
        msg.devicePosition = playerObject.transform.position;
        msg.deviceRotation = playerObject.transform.rotation;
        msg.deviceType = "Hololens";
        msg.purpose = "Simulation";
        //#if !UNITY_EDITOR
        //        Debug.Log("Unity Editor");
        //        print("Printing the network address as: ' " + GetLocalIPAddress() + "'. Considering this as local ip address");
        //#endif

        msg.ipAddress = this.networkAddress;
        client.Send(messageID, msg);
    }

    //#if !UNITY_EDITOR
    //    public static string GetLocalIPAddress()
    //    {
    //        var host = Dns.GetHostEntry(Dns.GetHostName());
    //        foreach (var ip in host.AddressList)
    //        {
    //            if (ip.AddressFamily == AddressFamily.InterNetwork)
    //            {
    //                return ip.ToString();
    //            }
    //        }
    //        throw new Exception("No network adapters with an IPv4 address in the system!");
    //    }
    //#endif

    public void ConnectToServer()
    {
        StartClient();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        this.client.RegisterHandler(778, OnReceivedMessage);
        //HideLocalPlayer();
    }

    protected void OnReceivedMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<ClientLocations>();
        print("Message recieved from server");

        if (msg.devicePosition1 != Vector3.zero && msg.deviceRotation1 != Quaternion.identity)
        {
            GameObject localPlayer;
            if (clientsRawData[0] == null)
            {
                localPlayer = (GameObject)Instantiate(playerObject,new GameObject().transform, true);
                clientsRawData[0] = localPlayer;
            }
            else
            {
                localPlayer = clientsRawData[0];
            }

            
            localPlayer.transform.SetParent(WorldMap.transform,true);
            localPlayer.transform.localPosition = msg.devicePosition1;
            localPlayer.transform.localRotation = msg.deviceRotation1;
            localPlayer.transform.localScale = new Vector3(0.4f, 0.8f, 0.4f);
            clientsRawData[0] = localPlayer;
        }
        if (msg.devicePosition2 != Vector3.zero && msg.deviceRotation2 != Quaternion.identity)
        {
            print("Second player is added");
            GameObject localPlayer;
            if (clientsRawData[1] == null)
            {
                localPlayer = (GameObject)Instantiate(playerObject, new GameObject().transform, true);
                clientsRawData[1] = localPlayer;
            }
            else
            {
                localPlayer = clientsRawData[1];
            }
            localPlayer.transform.SetParent(WorldMap.transform, true);
            localPlayer.transform.localPosition = msg.devicePosition2;
            localPlayer.transform.localRotation = msg.deviceRotation2;
            localPlayer.transform.localScale = new Vector3(0.4f, 0.8f, 0.4f);
            clientsRawData[1] = localPlayer;
        }
        if (msg.devicePosition3 != Vector3.zero && msg.deviceRotation3 != Quaternion.identity)
        {
            GameObject localPlayer;
            if (clientsRawData[2] == null)
            {
                localPlayer = (GameObject)Instantiate(playerObject, new GameObject().transform, true);
                clientsRawData[2] = localPlayer;
            }
            else
            {
                localPlayer = clientsRawData[2];
            }
            localPlayer.transform.SetParent(WorldMap.transform, true);
            localPlayer.transform.localPosition = msg.devicePosition3;
            localPlayer.transform.localRotation = msg.deviceRotation3;
            localPlayer.transform.localScale = new Vector3(0.4f, 0.8f, 0.4f);
            clientsRawData[2] = localPlayer;
        }
        if (msg.devicePosition4 != Vector3.zero && msg.deviceRotation4 != Quaternion.identity)
        {
            GameObject localPlayer = clientsRawData[3];
            localPlayer.transform.position = msg.devicePosition4;
            localPlayer.transform.rotation = msg.deviceRotation4;
            clientsRawData[3] = localPlayer;
        }
        if (msg.devicePosition5 != Vector3.zero && msg.deviceRotation5 != Quaternion.identity)
        {
            GameObject localPlayer = clientsRawData[4];
            localPlayer.transform.position = msg.devicePosition5;
            localPlayer.transform.rotation = msg.deviceRotation5;
            clientsRawData[4] = localPlayer;
        }
        if (msg.devicePosition6 != Vector3.zero && msg.deviceRotation6 != Quaternion.identity)
        {
            GameObject localPlayer = clientsRawData[5];
            localPlayer.transform.position = msg.devicePosition6;
            localPlayer.transform.rotation = msg.deviceRotation6;
            clientsRawData[5] = localPlayer;
        }
        if (msg.devicePosition7 != Vector3.zero && msg.deviceRotation7 != Quaternion.identity)
        {
            GameObject localPlayer = clientsRawData[6];
            localPlayer.transform.position = msg.devicePosition7;
            localPlayer.transform.rotation = msg.deviceRotation7;
            clientsRawData[6] = localPlayer;
        }
        if (msg.devicePosition8 != Vector3.zero && msg.deviceRotation8 != Quaternion.identity)
        {
            GameObject localPlayer = clientsRawData[7];
            localPlayer.transform.position = msg.devicePosition8;
            localPlayer.transform.rotation = msg.deviceRotation8;
            clientsRawData[7] = localPlayer;
        }

        //clientsRawData = msg.clients;
    }

    private void HideLocalPlayer()
    {
        if (playerObject.GetComponent<Renderer>().enabled == true)
        {
            playerObject.GetComponent<Renderer>().enabled = false;
        }
    }

}
