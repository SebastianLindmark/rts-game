﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour, ToolbarClickListener{

    private ToolbarController toolbarController;

    public List<BaseBuilding> spawnableBuildings;

    private BaseBuilding placementObject;

    private int gridSquareSize = 2;

    void Start () {

        
        toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();
        Player player = PlayerManager.humanPlayer;
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(player);


        foreach (BaseBuilding building in spawnableBuildings)
        {
            pEnv.GetBuildableObjects().AddObject(player, building, this);
        }
        
    }

    void Update() {


        

    }

    void OnGUI()
    {

        if (placementObject != null)
        {
            
            if (HitsObstacle(Input.mousePosition,placementObject.transform))
            {
                placementObject.GetComponent<PlacementEffect>().ApplyInvalidEffect();
            }
            else

            {
                placementObject.GetComponent<PlacementEffect>().ApplyValidEffect();
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            int layerMask = ((1 << LayerMask.NameToLayer("Ground")));
            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,layerMask))
            {
                var finalPosition = GetNearestPointOnGrid(hitInfo.point);



                float objectHeight = placementObject.GetComponentInChildren<Renderer>().bounds.size.y;
                finalPosition.y += objectHeight / 2;
                placementObject.transform.position = finalPosition;
                
            }
        }
    }


    void LateUpdate () {
        if (Input.GetMouseButtonDown(0) && placementObject != null && !HitsObstacle(Input.mousePosition, placementObject.transform))
        {
            PlaceObject();
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (placementObject == null)
            {
                //toolbarController.PopulateToolbar(spawnableBuildings, GetComponent<PlayerInitializer>().GetPlayer(), this);
            }
            else
            {
                Deselect();
            }

        }
    }

    public void PlaceObject()
    {
        placementObject.GetComponent<PlacementEffect>().Reset();
        Destroy(placementObject.GetComponent<PlacementEffect>());
        placementObject.OnCreated();
        placementObject = null;
    }

    public void Deselect()
    {
        if (placementObject != null) {
            placementObject.RemoveObject();
            placementObject = null;
        }
    }

    public void OnToolBarClick(BaseObject obj)
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placementObject = new BaseFactory().CreateUnit(PlayerManager.humanPlayer, obj, startPosition) as BaseBuilding;
        placementObject.gameObject.AddComponent<PlacementEffect>().Setup();
    }


    public static bool HitsObstacle(Vector3 clickPosition, Transform buildingToPlace)
    {

        int layerMask = ((1 << LayerMask.NameToLayer("Building")));
        Collider[] hitColliders = Physics.OverlapBox(buildingToPlace.position, buildingToPlace.localScale, Quaternion.identity, layerMask);
        if (hitColliders.Length > 0 ) {
            Debug.Log(hitColliders[0].name);
        }
        return hitColliders.Length > 0;
    }


    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        int xCount = Mathf.RoundToInt(position.x / gridSquareSize);
        int yCount = Mathf.RoundToInt(position.y / gridSquareSize);
        int zCount = Mathf.RoundToInt(position.z / gridSquareSize);

        Vector3 result = new Vector3(
            (float)xCount * gridSquareSize,
            (float)yCount * gridSquareSize,
            (float)zCount * gridSquareSize);

        return result;
    }

}
