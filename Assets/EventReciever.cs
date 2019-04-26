using HoloToolkit.Unity.Receivers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class EventReciever : InteractionReceiver
{

  

    protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData)
    {
        Debug.Log(obj.name + " : FocusEnter");
    }

    protected override void FocusExit(GameObject obj, PointerSpecificEventData eventData)
    {
        Debug.Log(obj.name + " : FocusExit");
    }

    protected override void InputDown(GameObject obj, InputEventData eventData)
    {
        Debug.Log(obj.name + " : InputDown");
        switch (obj.name)
        {
            case "Navigation":
                Debug.Log("Navigation Related Tasks");
                NodeNavigation.startNode = "4041";
                NodeNavigation.destination = "435F";
                NodeNavigation.instance.GetAllPaths();
                break;

            case "Connection":
                Debug.Log("Connect to the server");
                break;

            default:
                Debug.Log("Default statement executed");
                break;
        }
    }

    protected override void InputUp(GameObject obj, InputEventData eventData)
    {
        Debug.Log(obj.name + " : InputUp");
    }
}
