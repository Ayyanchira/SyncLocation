using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour {

    BuildingAnimationManager buildingAnimationManagerComponent;
    SphereBasedTagalong tagAlongComponent;
    Billboard billboardComponent;
	// Use this for initialization
	void Start () {
        buildingAnimationManagerComponent = gameObject.GetComponent<BuildingAnimationManager>();
        billboardComponent = gameObject.GetComponent<Billboard>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisableMovementComponents()
    {
        //TagAlong can get delayed so having it here instead of in the start 
        tagAlongComponent = gameObject.GetComponent<SphereBasedTagalong>();

        tagAlongComponent.enabled = false;
        billboardComponent.enabled = false;
        buildingAnimationManagerComponent.enabled = false;
    }

    public void EnableMovementComponents()
    {
        //TagAlong can get delayed so having it here instead of in the start 
        tagAlongComponent = gameObject.GetComponent<SphereBasedTagalong>();

        tagAlongComponent.enabled = true;
        billboardComponent.enabled = true;
        buildingAnimationManagerComponent.enabled = true;
    }
}
