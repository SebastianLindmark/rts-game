using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour, ToolbarClickListener{

    private ToolbarController toolbarController;

    public List<BaseBuilding> spawnableBuildings;

    private BaseBuilding placementObject;

    private int gridSquareSize = 2;

    void Start () {

        
        toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();

        foreach (BaseBuilding building in spawnableBuildings)
        {
            toolbarController.AddToolbarField(building, GetComponent<PlayerInitializer>().GetPlayer(), this);
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
        Debug.Log("Placeing object");
        placementObject.GetComponent<PlacementEffect>().Reset();
        Destroy(placementObject.GetComponent<PlacementEffect>());
        placementObject.OnPlaced();
        placementObject = null;
    }

    public void Deselect()
    {
        if (placementObject != null) {
            Destroy(placementObject.gameObject);
            placementObject = null;
        }
    }

    public void OnToolBarClick(BaseObject obj)
    {
        placementObject = Instantiate(obj.gameObject).GetComponent<BaseBuilding>();
        placementObject.gameObject.AddComponent<PlacementEffect>().Setup();
    }


    public static bool HitsObstacle(Vector3 clickPosition, Transform buildingToPlace)
    {

        int layerMask = ((1 << LayerMask.NameToLayer("Building")));
        Collider[] hitColliders = Physics.OverlapBox(buildingToPlace.position, buildingToPlace.localScale / 2, Quaternion.identity, layerMask);
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
