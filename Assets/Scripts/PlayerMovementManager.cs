using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour {

    GameObject hololensPlayer;

    public GameObject hololensCamera;
    public GameObject player;
    public static string lastImageTracked = "";
    public static bool vuforiaTargetDetected = false;
    [SerializeField]GameObject imageTarget;

    [SerializeField]GameObject AllPlaces;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LocationNetworkManager.singleton.IsClientConnected())
        {

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {

                GetImageTargetObject();
                UpdatePlayerCoordinates();
            }
        }
    }

    private void UpdatePlayerCoordinates()
    {   
        //TODO: Instead of directly assigning Hololens Camera transform, we now hae to create a gameobject which represents self and then send assign it to player
        if (hololensCamera != null)
        {
            GeneratePlayerCoordinates();
        }
        else
        {
            print("Camera not found.");
        }
    }

    private void GeneratePlayerCoordinates()
    {
        if (lastImageTracked == "")
        {
            //Just assigning camera's location to player as it is.
            player.transform.position = hololensCamera.transform.position;
            player.transform.rotation = hololensCamera.transform.rotation;
        }
        else
        {
            RepositionPlayer();
        }

    }

    private void GetImageTargetObject()
    {
        if(vuforiaTargetDetected)
        {
            vuforiaTargetDetected = false;
            foreach (GameObject child in AllPlaces.GetComponents<GameObject>())
            {
                if (child.tag == lastImageTracked)
                {
                    print("the Image target name stored is " + child.tag + "And what we get from vuforia is " + lastImageTracked);
                    imageTarget = child;
                }
            }
        }
        
    }

    private void RepositionPlayer()
    {
        //TODO: TRS function should have first parameter the difference of user's location and the image target that he found. Currently only passing the hololens transform position.
        var m = Matrix4x4.TRS(hololensCamera.transform.position, imageTarget.transform.rotation, Vector3.one);
        m = m.inverse;
        var position = MatrixUtils.ExtractTranslationFromMatrix(ref m);
        player.transform.SetParent(imageTarget.transform, false);
        player.transform.localPosition = position;
    }
}
