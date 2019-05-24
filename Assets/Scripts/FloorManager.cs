using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is dedicated for building gameobect.
/// The primary task if this script will be to manage the location of floors models within the building gameobject.
/// It will also handle hiding and showing the floor models based on the scenarios and user request. 
/// It can also animate the one floor for example - a floor sliding out of entire building for highlighting purposes.
/// The animation of entire building should not be written here. Attaching to player's camera will be taken care by tagalong scripts. 
/// The movement of keeping aside and bring to front will also be taken care by another deidcated script
/// 
/// </summary>
public class FloorManager : MonoBehaviour {

    [SerializeField] GameObject firstfloor, secondfloor, thirdfloor, fourthfloor;
    [SerializeField] GameObject firsFloorModel, secondFloorModel, thirdFloorModel, fourthFloorModel;
    [SerializeField] Material material1, material2, material3, material4;
    [SerializeField] Material transparentMaterial;
    [SerializeField] bool isInFront = false;
    [SerializeField] GameObject headPosition;
    bool isIsometricView = false;
    [SerializeField] GameObject RotatingObject;

    Renderer Renderer1, Renderer2, Renderer3, Renderer4;



    // Use this for initialization
    void Start() {

        validation();
        LoadRenderers();

    }

    private void LoadRenderers()
    {
        Renderer1 = firsFloorModel.GetComponentInChildren<Renderer>();
        Renderer2 = secondFloorModel.GetComponentInChildren<Renderer>();
        Renderer3 = thirdFloorModel.GetComponentInChildren<Renderer>();
        Renderer4 = fourthFloorModel.GetComponentInChildren<Renderer>();
    }

    private void validation()
    {
        if (headPosition == null)
        {
            Debug.Log("Add Holotoolkit camera/ARCamera gameobject representing the player location in headPosition");
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isIsometricView)
        {
            TakeSphereRotation();
        }
        
    }

    private void TakeSphereRotation()
    {
        firstfloor.transform.rotation = RotatingObject.transform.rotation;
        secondfloor.transform.rotation = RotatingObject.transform.rotation;
        thirdfloor.transform.rotation = RotatingObject.transform.rotation;
        fourthfloor.transform.rotation = RotatingObject.transform.rotation;
    }

    public void keepAllToLeft()
    {
        //Check if the models are displayed in front of the user. only then, move the whole building aside.

        firstfloor.transform.localPosition = new Vector3(-2f, firstfloor.transform.localPosition.y, firstfloor.transform.localPosition.z);
        secondfloor.transform.localPosition = new Vector3(-2f, secondfloor.transform.localPosition.y, secondfloor.transform.localPosition.z);
        thirdfloor.transform.localPosition = new Vector3(-2f, thirdfloor.transform.localPosition.y, thirdfloor.transform.localPosition.z);
        fourthfloor.transform.localPosition = new Vector3(-2f, fourthfloor.transform.localPosition.y, fourthfloor.transform.localPosition.z);


        firstfloor.transform.localRotation = Quaternion.Euler(0, 0, 0);
        secondfloor.transform.localRotation = Quaternion.Euler(0, 0, 0);
        thirdfloor.transform.localRotation = Quaternion.Euler(0, 0, 0);
        fourthfloor.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }

    public void HideAll()
    {
        if (Renderer1 == null || Renderer2 == null || Renderer3 == null || Renderer4 == null)
            return;

        //Renderer1.material.Lerp(material1, transparentMaterial, 1);
        //Renderer2.material.Lerp(material2, transparentMaterial, 1);
        //Renderer3.material.Lerp(material3, transparentMaterial, 1);
        //Renderer4.material.Lerp(material4, transparentMaterial, 1);

        StartCoroutine(FadeTo(0.0f, 1.0f, Renderer1));
        StartCoroutine(FadeTo(0.0f, 1.0f, Renderer2));
        StartCoroutine(FadeTo(0.0f, 1.0f, Renderer3));
        StartCoroutine(FadeTo(0.0f, 1.0f, Renderer4));
    }

    public void ShowAll()
    {
        if (Renderer1 == null || Renderer2 == null || Renderer3 == null || Renderer4 == null)
            return;

        Renderer1.material.Lerp(transparentMaterial, material1, 1);
        Renderer2.material.Lerp(transparentMaterial, material2, 1);
        Renderer3.material.Lerp(transparentMaterial, material3, 1);
        Renderer4.material.Lerp(transparentMaterial, material4, 1);
    }

    IEnumerator FadeTo(float aValue, float aTime, Renderer modelRenderer)
    {
        float alpha = modelRenderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            modelRenderer.material.color = newColor;
            yield return null;
        }
    }


    public void bringToFront()
    {
        //Check if the models are displayed in front of the user. only then, move the whole building aside.
        firstfloor.transform.localPosition = new Vector3(0, firstfloor.transform.localPosition.y, firstfloor.transform.localPosition.z);
        secondfloor.transform.localPosition = new Vector3(0, secondfloor.transform.localPosition.y, secondfloor.transform.localPosition.z);
        thirdfloor.transform.localPosition = new Vector3(0, thirdfloor.transform.localPosition.y, thirdfloor.transform.localPosition.z);
        fourthfloor.transform.localPosition = new Vector3(0, fourthfloor.transform.localPosition.y, fourthfloor.transform.localPosition.z);
    }

    public void showFirstFloor()
    {
        keepAllToLeft();
        firstfloor.transform.localPosition = new Vector3(0, 0.8f , firstfloor.transform.localPosition.z);
        firstfloor.transform.localRotation = Quaternion.Euler(-40, 0, 0);
    }

    public void showSecondFloor()
    {
        keepAllToLeft();
        secondfloor.transform.localPosition = new Vector3(0, secondfloor.transform.localPosition.y, secondfloor.transform.localPosition.z);
    }

    public void showThirdFloor()
    {
        keepAllToLeft();
        thirdfloor.transform.localPosition = new Vector3(0, thirdfloor.transform.localPosition.y, thirdfloor.transform.localPosition.z);
    }

    public void showFourthFloor()
    {
        keepAllToLeft();
        Vector3 newLocation = new Vector3(0, fourthfloor.transform.localPosition.y, fourthfloor.transform.localPosition.z);
        fourthfloor.transform.localPosition = Vector3.Lerp(fourthfloor.transform.localPosition, newLocation, 2f);
        //fourthfloor.transform.localPosition = new Vector3(0, fourthfloor.transform.localPosition.y, fourthfloor.transform.localPosition.z);
    }

    public void showStairCase()
    {
        isIsometricView = false;
        fourthfloor.transform.localPosition = new Vector3(0, 0.3f, 0.6f);
        thirdfloor.transform.localPosition = new Vector3(0, 0.2f, 0.3f);
        secondfloor.transform.localPosition = new Vector3(0, 0.1f, 0);
        firstfloor.transform.localPosition = new Vector3(0, 0f, -0.3f);

        fourthfloor.transform.localRotation = Quaternion.identity;
        thirdfloor.transform.localRotation = Quaternion.identity;
        secondfloor.transform.localRotation = Quaternion.identity;
        firstfloor.transform.localRotation = Quaternion.identity;

    }

    public void showIsometric()
    {
        isIsometricView = true;
        fourthfloor.transform.localPosition = new Vector3(0, 0.3f, 0);
        thirdfloor.transform.localPosition = new Vector3(0, 0.2f, 0);
        secondfloor.transform.localPosition = new Vector3(0, 0.1f, 0);
        firstfloor.transform.localPosition = new Vector3(0, 0.0f, 0);

        fourthfloor.transform.localRotation = Quaternion.identity;
        thirdfloor.transform.localRotation = Quaternion.identity;
        secondfloor.transform.localRotation = Quaternion.identity;
        firstfloor.transform.localRotation = Quaternion.identity;
    }

    public void showOrthogonal()
    {
        isIsometricView = false;
        fourthfloor.transform.localPosition = new Vector3(0, 0, 0);
        thirdfloor.transform.localPosition = new Vector3(1, 0, 0);
        secondfloor.transform.localPosition = new Vector3(2, 0, 0);
        firstfloor.transform.localPosition = new Vector3(3, 0, 0);

        //TODO: Rotate the y and z to 90 90. It should work. Dont know why its not rotating!
        fourthfloor.transform.localRotation = new Quaternion(0, 123, 120, 2);
        //fourthfloor.transform.localEulerAngles.Set(0, 90f, 90f);
        thirdfloor.transform.localEulerAngles.Set(0, 90f, 90f);
        secondfloor.transform.localEulerAngles.Set(0, 90f, 90f);
        firstfloor.transform.localEulerAngles.Set(0, 90f, 90f);

        print("The rotation is : "+firstfloor.transform.localRotation.y);
    }


}

public class BuildingFloors
{
    public Vector3 firstFloorLocation, secondFloorLocation, thirdFloorLocation, fourthFloorLocation;
}

