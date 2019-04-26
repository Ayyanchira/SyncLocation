using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimationManager : MonoBehaviour {

    [SerializeField] Transform headPosition;
	// Use this for initialization
	void Start () {
        //Initialize the position from the camera to 3 meters in front
        gameObject.transform.position = new Vector3(headPosition.position.x, headPosition.position.y - 1f, headPosition.position.z + 3f);
        var tagAlong = gameObject.GetComponent<SphereBasedTagalong>();
        tagAlong.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        //Look at camera is taken care by billboard script
        //LookAtCamera();

        stayDown();
    }

    private void stayDown()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, headPosition.transform.position.y - 1f, transform.position.z), 0.1f);
    }

    //Alternative to billboardscript
    private void LookAtCamera()
    {
        gameObject.transform.LookAt(headPosition);
        transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
    }

    void bringToFront()
    {

    }
}
