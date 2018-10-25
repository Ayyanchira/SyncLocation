using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour {

    GameObject hololensPlayer;

    public GameObject hololensCamera;
    public GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LocationNetworkManager.singleton.IsClientConnected())
        {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");

            }
            else
            {
                print("Updating player coordinates");
                if (hololensCamera != null){
                    player.transform.position = hololensCamera.transform.position;
                    player.transform.rotation = hololensCamera.transform.rotation;
                }
                else
                {
                    print("Camera not found.");
                }
            }
        }
	}
}
