using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour, ToolbarClickListener{

    private ToolbarController toolbarController;

    public List<BaseObject> spawnableBuildings;

    private PlacementEffect placementObject;

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
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = ((1 << LayerMask.NameToLayer("Building")));
            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity, layerMask) && hitInfo.collider != null)
            {
                placementObject.ApplyInvalidEffect();
            }
            else
            {
                placementObject.ApplyValidEffect();
            }

            if (Physics.Raycast(ray, out hitInfo))
            {
                var finalPosition = GetNearestPointOnGrid(hitInfo.point);
                GameObject p = placementObject.obj;
                finalPosition.y += p.transform.lossyScale.y / 2;
                p.transform.position = finalPosition;              

            }
        }

    }


    void LateUpdate () {
        if (Input.GetMouseButtonDown(0) && placementObject != null)
        {
            placementObject.Reset();
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
            Destroy(placementObject.obj);
            placementObject = null;
        }
    }

    public void OnToolBarClick(BaseObject obj)
    {
        placementObject = new PlacementEffect(Instantiate<BaseObject>(obj).gameObject);       
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
