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
            
            if (HitsObstacle(Input.mousePosition))
            {
                placementObject.ApplyInvalidEffect();
            }
            else
            {
                placementObject.ApplyValidEffect();
            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            int layerMask = ((1 << LayerMask.NameToLayer("Ground")));
            if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,layerMask))
            {
                var finalPosition = GetNearestPointOnGrid(hitInfo.point);
                GameObject p = placementObject.obj;
                finalPosition.y += p.transform.lossyScale.y / 2;
                p.transform.position = finalPosition;              

            }
        }
    }


    void LateUpdate () {
        if (Input.GetMouseButtonDown(0) && placementObject != null && !HitsObstacle(Input.mousePosition))
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


    public bool HitsObstacle(Vector3 clickPosition)
    {

        GameObject gameObject = placementObject.obj;
        int layerMask = ((1 << LayerMask.NameToLayer("Building")));
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale / 2, Quaternion.identity, layerMask);
        Debug.Log(hitColliders.Length);
        return hitColliders.Length > 0;
        
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        int layerMask = ((1 << LayerMask.NameToLayer("Building")));

        Vector3 position = placementObject.obj.transform.position;
        Vector3 something = placementObject.obj.transform.localScale;
        Debug.Log(something);
        Vector3 somethingElse = placementObject.obj.transform.forward;

        RaycastHit rayHit;

        if (Physics.BoxCast(position, something, somethingElse, out rayHit))
        {
            Debug.Log("I hit something");
        }

        
        return (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask) && hitInfo.collider != null);
        */
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
