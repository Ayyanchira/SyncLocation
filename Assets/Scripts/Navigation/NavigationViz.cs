﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationViz
{


    ArrayList allPaths = new ArrayList();
    //TODO: Make use of this gameobject
    public GameObject lineRenderingNode = new GameObject();
    public int shortTestPathIndex;
    public bool isShortestPathCalculated;

    public void clearLineRenderer()
    {
        LineRenderer line = lineRenderingNode.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = lineRenderingNode.AddComponent<LineRenderer>();
        }
        line.positionCount = 0;
    }

    public void pathCalculationCompleled()
    {
        Debug.Log("Paths have been calculated");
        allPaths = NodeNavigation.possiblePaths;
        if (allPaths.Count == 0)
        {
            clearLineRenderer();
        }
        else
        {
            if (!isShortestPathCalculated)
            {
                scanPaths();
            }
            
            drawPathForPathAtIndex(shortTestPathIndex);
        }

    }

    public void drawPathForPathAtIndex(int index)
    {
        ArrayList pathSelected = new ArrayList();
        pathSelected = allPaths[index] as ArrayList;
        LineRenderer line = null;

        int i = 0;

        //TODO: Create a node just for line renderer so that we can reuse that and take that to disable the paths whenever necessary

        line = lineRenderingNode.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = lineRenderingNode.AddComponent<LineRenderer>();
        }
        // Set the width of the Line Renderer
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        // Set the number of vertex fo the Line Renderer
        //line.SetVertexCount(pathSelected.Count);
        line.positionCount = pathSelected.Count;

        foreach (string node in pathSelected)
        {
            GameObject nodeObject = GameObject.Find(node);
            if (nodeObject == null)
            {
                Debug.Log("GameObject named " + node + "is not found.. Please check");
            }
            //if(i == 0){
            //    line = nodeObject.GetComponent<LineRenderer>();
            //    if(line == null){
            //        line = nodeObject.AddComponent<LineRenderer>();
            //    }
            //    // Set the width of the Line Renderer
            //    line.startWidth = 1f;
            //    line.endWidth = 1f;
            //    // Set the number of vertex fo the Line Renderer
            //    //line.SetVertexCount(pathSelected.Count);
            //    line.positionCount = pathSelected.Count;
            //}
            //if(nodeObject == null){
            //    Debug.Log(node + "is not found.");
            //    return;
            //}
            line.SetPosition(i, nodeObject.transform.position);
            i++;
        }
    }

    private void scanPaths()
    {
        var i = 0;
        var maxWeight = float.MaxValue;
        var shortPI = 0;
        foreach (ArrayList path in allPaths)
        {
            var weight = findWeight(path);
            Debug.Log("Weight of path " + (i + 1) + " is " + weight);
            if (weight < maxWeight)
            {
                shortPI = i;
                maxWeight = weight;
            }
            i++;
        }
        shortTestPathIndex = shortPI;
        isShortestPathCalculated = true;
    }

    private float findWeight(ArrayList path)
    {

        if (path.Count == 1)
        {
            Debug.Log("Weight is Zero");
            return 0;
        }
        //GameObject source;
        GameObject consequentNode = null;
        int i = 0;
        float pathWeight = 0;
        foreach (string node in path)
        {

            if (i == 0)
            {
                consequentNode = GameObject.Find(node);
            }
            else
            {

                GameObject nextNode = GameObject.Find(node);
                if (nextNode == null || consequentNode == null)
                {
                    if (consequentNode == null)
                    {
                        Debug.Log("Source is not there at place");
                    }
                    if (nextNode == null)
                    {
                        Debug.Log(node + " gameobject not found.");
                    }
                    return 0;
                }
                Vector3 difference = consequentNode.transform.position - nextNode.transform.position;
                var distance = difference.magnitude;
                pathWeight += distance;
                consequentNode = nextNode;
            }

            i += 1;

        }
        return pathWeight;
    }
}
