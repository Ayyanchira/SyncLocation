using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocationNetworkManager : NetworkManager
{

    protected static short messageID = 777;
    public GameObject playerObject;

    public class CustomMessage : MessageBase
    {
        public string deviceType, purpose, ipAddress;
        public Vector3 devicePosition;
        public Quaternion deviceRotation;
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


}
