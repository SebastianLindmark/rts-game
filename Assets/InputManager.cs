using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    public Camera cam;

    private List<BaseObject> inputListeners = new List<BaseObject>();

    private List<BaseObject> selectedObjects = new List<BaseObject>();
    

    public void RegisterListener(BaseObject listener)
    {
        inputListeners.Add(listener);
    }

    void NotifyClickListeners(Vector3 clickPosition)
    {

        Vector3 clickVector;

        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(clickPosition), out hit, Mathf.Infinity))
        {
            clickVector = hit.point;
        }
        else
        {
            Debug.LogError("Invalid raycast, ignoring click");
            return;
        }


        List<BaseObject> selectedObjects = new List<BaseObject>();

        foreach (BaseObject obj in inputListeners)
        {
            if (obj.Within(clickVector))
            {
                selectedObjects.Add(obj);
            }
        }

    }

    void NotifyDeselectListeners(float x, float y)
    {
    }

    void Start()
    {

    }





    void Update()
    {

        //test();

        

        if (Input.GetMouseButtonUp(0))
        {
            BaseUnit[] l = GameObject.FindObjectsOfType<BaseUnit>();
            foreach (BaseUnit u in l) {
                Debug.Log("Walking");
                u.Walk(Input.mousePosition);
            }
        }

        
        
        /*if (Input.GetMouseButtonDown(0))
        {
            NotifyClickListeners(Input.mousePosition);
        }


        if (Input.GetMouseButtonDown(1))
        {
            NotifyDeselectListeners(Input.mousePosition.x, Input.mousePosition.y);
        }*/

    }

    //

    ///

    //

    //
    private Vector3 startSelectionDrag;
    private Vector3 endSelectionDrag;


    private void test()
    {

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            startSelectionDrag = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) {

            List<BaseObject> clickedObjects;

            if (startSelectionDrag - Input.mousePosition == Vector3.zero)
            {
                clickedObjects = GetSelectedObject(Input.mousePosition);
            }
            else
            {
                clickedObjects = GetSelectedObjects(GetViewportBounds(cam, startSelectionDrag, Input.mousePosition));
            }


            if (clickedObjects.Count > 1)
            {
                HandleClickOnObjects(clickedObjects);
            }
            else if (clickedObjects.Count == 1)
            {
                HandleClickOnObject(clickedObjects[0]);
            }
            else {
                HandleClickOnGround(startSelectionDrag);
            }    
        }
        else if (Input.GetMouseButtonUp(1))
        {
            DeselectObjects();
        }

        
    }

    private void HandleClickOnObjects(List<BaseObject> clickedObjects)
    {
        if (selectedObjects.Count == 0) {
            SelectObjects(clickedObjects);
        }
        else
        {
            //need to check if same team. For now, just reselect
            SelectObjects(clickedObjects);
        }
    }

    private void HandleClickOnObject(BaseObject clickedObject) {
        bool sameTeam = true;

        if (sameTeam) {
            SelectObjects(new List<BaseObject> { clickedObject });
        }
        else if(selectedObjects.Count > 0)
        {
            ClickSelectObjects(clickedObject);
        }


    }


    private void HandleClickOnGround(Vector3 position)
    {
        Debug.Log("Click on ground");
        foreach (BaseObject obj in selectedObjects)
        {
            Debug.Log("Moving unit");
            obj.OnGroundClick(position);
        }
    }


    private void SelectObjects(List<BaseObject> clickedObjects)
    {
        foreach (BaseObject o in clickedObjects)
        {
            o.OnSelect();
        }
        DeselectObjects();
        selectedObjects = clickedObjects;
    }

    private void DeselectObjects() {
        foreach(BaseObject o in selectedObjects)
        {
            o.OnUnselect();
        }
    }

    private void ClickSelectObjects(BaseObject clickedObject) {
        foreach (BaseObject o in selectedObjects)
        {
            o.OnEnemyClick(clickedObject);
        }
    }
    


    private List<BaseObject> GetSelectedObject(Vector3 clickVector)
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(clickVector), out hit, Mathf.Infinity);
        clickVector = hit.point;

        List<BaseObject> selectedObjects = new List<BaseObject>();
        foreach (BaseObject obj in inputListeners)
        {
            if (obj.Within(clickVector))
            {
                return new List<BaseObject> { obj };
            }
        }
        return new List<BaseObject>();
    }

    private List<BaseObject> GetSelectedObjects(Bounds bounds) {
        List<BaseObject> selectedObjects = new List<BaseObject>();

        foreach (BaseObject unit in inputListeners)
        {

            if (bounds.Contains(cam.WorldToViewportPoint(unit.transform.position)))
            {
                selectedObjects.Add(unit);
            }
        }
        return selectedObjects;
        
    }


    public bool IsWithinSelectionBounds(GameObject gameObject, Vector3 mousePosition1)
    {
       
        var camera = Camera.main;
        var viewportBounds = GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }


    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }

}
