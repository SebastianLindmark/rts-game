using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour, ToolbarClickListener{

    private ToolbarController toolbarController;

    public List<BaseObject> spawnableBuildings;

    private GameObject placementObject;

    private int gridSquareSize = 2;

    void Start () {
        toolbarController = GameObject.Find("Toolbar").GetComponent<ToolbarController>();
        GameObject gameObject = Resources.Load("Prefabs/ExampleBuilding") as GameObject;
        spawnableBuildings.Add(gameObject.GetComponent<BaseObject>());
        spawnableBuildings.Add(gameObject.GetComponent<BaseObject>());
        spawnableBuildings.Add(gameObject.GetComponent<BaseObject>());
        toolbarController.PopulateToolbar(spawnableBuildings, GetComponent<PlayerScript>().GetPlayer(), this);
    }

    void Update() {


        

    }

    void OnGUI()
    {

        if (placementObject != null)
        {
            
            if (HitsObstacle(Input.mousePosition))
            {
                Debug.Log("Invalid");
                placementObject.GetComponent<PlacementEffect>().ApplyInvalidEffect();
            }
            else

            {
                Debug.Log("Valid");
                placementObject.GetComponent<PlacementEffect>().ApplyValidEffect();
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            int layerMask = ((1 << LayerMask.NameToLayer("Ground")));
            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,layerMask))
            {
                var finalPosition = GetNearestPointOnGrid(hitInfo.point);
                finalPosition.y += placementObject.transform.lossyScale.y / 2;
                placementObject.transform.position = finalPosition;              

            }
        }
    }


    void LateUpdate () {
        if (Input.GetMouseButtonDown(0) && placementObject != null && !HitsObstacle(Input.mousePosition))
        {
            placementObject.GetComponent<PlacementEffect>().Reset();
            placementObject = null;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (placementObject == null)
            {
                toolbarController.PopulateToolbar(spawnableBuildings, GetComponent<PlayerScript>().GetPlayer(), this);
            }
            else
            {
                Deselect();
            }

        }
    }

    public void Deselect()
    {
        if (placementObject != null) {
            Destroy(placementObject);
            placementObject = null;
        }
    }

    public void OnToolBarClick(BaseObject obj)
    {
        placementObject = Instantiate(obj.gameObject);
        placementObject.GetComponent<PlacementEffect>().Setup();
    }


    public bool HitsObstacle(Vector3 clickPosition)
    {
        int layerMask = ((1 << LayerMask.NameToLayer("Building")));
        Collider[] hitColliders = Physics.OverlapBox(placementObject.transform.position, placementObject.transform.localScale / 2, Quaternion.identity, layerMask);
        Debug.Log(hitColliders.Length);
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
