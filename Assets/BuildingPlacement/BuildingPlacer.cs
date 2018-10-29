using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour, ToolbarClickListener{

    private ToolbarController toolbarController;

    public List<BaseBuilding> spawnableBuildings;

    private BaseBuilding placementObject;

    private int gridSquareSize = 2;

    private bool m_Started = false;

    void Start () {

        
        toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();
        Player player = PlayerManager.humanPlayer;
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(player);


        foreach (BaseBuilding building in spawnableBuildings)
        {
            pEnv.GetBuildableObjects().AddObject(player, building, this);
        }
        m_Started = true;
    }

    void Update() {


        

    }

    void OnGUI()
    {

        if (placementObject != null)
        {
            
            if (HitsObstacle(placementObject.transform.position,placementObject.transform))
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
        if (Input.GetMouseButtonDown(0) && placementObject != null && !HitsObstacle(placementObject.transform.position, placementObject.transform))
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

    public BaseObject OnToolBarClick(BaseObject obj)
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placementObject = new BaseFactory().CreateUnit(PlayerManager.humanPlayer, obj, startPosition) as BaseBuilding;

        PlacementEffect placementEffect = placementObject.GetComponent<PlacementEffect>();

        if (placementObject.GetComponent<PlacementEffect>() == null) {
            placementEffect = placementObject.gameObject.AddComponent<PlacementEffect>();
        }
        placementEffect.Setup();
        return null; //Should not be used
    }


    public static bool HitsObstacle(Vector3 clickPosition, Transform buildingToPlace)
    {

        int layerMask = ((1 << LayerMask.NameToLayer("Building")));

        Collider c = buildingToPlace.GetComponentInChildren<Collider>();
        Collider[] hitColliders = Physics.OverlapBox(clickPosition, c.bounds.size / 2, buildingToPlace.rotation, layerMask);

        if (hitColliders.Length == 1) {
            return buildingToPlace.gameObject != hitColliders[0].transform.root.gameObject; //Will always collide with itself
        }

        return hitColliders.Length > 1;
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


    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started && placementObject != null) { 
            Collider c = placementObject.GetComponentInChildren<Collider>();
            Gizmos.DrawWireCube(placementObject.transform.position, c.bounds.size);
        }
    }*/

}
