using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    public Camera cam;

    public GameObject selectionPrefab;

    private List<BaseObject> inputListeners = new List<BaseObject>();
    private List<BaseObject> selectedObjects = new List<BaseObject>();
    

    public void RegisterListener(BaseObject listener)
    {
        inputListeners.Add(listener);
    }

    public void UnregisterListener(BaseObject listener) {
        inputListeners.Remove(listener);
        selectedObjects.Remove(listener);
    }
    

    void Start()
    {

    }


    void Update()
    {
        test();

    }

    public Texture t;

    void OnGUI()
    {

        //Debug.Log(startSelectionDrag);

        if (startSelectionDrag != Vector3.zero)
        {
            Vector3 curPos = Input.mousePosition;
            Rect rect = new Rect(startSelectionDrag.x, startSelectionDrag.y, startSelectionDrag.x - curPos.x, startSelectionDrag.y - curPos.y);
            float width =  curPos.x - startSelectionDrag.x;
            float height = startSelectionDrag.y - curPos.y;
            Rect r = new Rect(startSelectionDrag.x, Screen.height - startSelectionDrag.y, width, height);

            Utils.DrawScreenRect(r, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(r, 1, Color.white);
        }



    }

    private Vector3 startSelectionDrag = Vector3.zero;
    private Vector3 endSelectionDrag;


    private void test()
    {

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down");
            startSelectionDrag = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {

            Vector3 worldPosClick = ConvertMousePosToWorldSpace(Input.mousePosition);

            List<BaseObject> clickedObjects;

            if (startSelectionDrag - Input.mousePosition == Vector3.zero)
            {
                Debug.Log("Single click");
                clickedObjects = GetSelectedObject(Input.mousePosition);
            }
            else
            {
                Debug.Log("Draged area");
                clickedObjects = GetSelectedObjects(GetViewportBounds(cam, startSelectionDrag, Input.mousePosition));
            }


            if (clickedObjects.Count > 1)
            {
                Debug.Log("Clicked on objects");
                HandleClickOnObjects(clickedObjects);
            }
            else if (clickedObjects.Count == 1)
            {
                Debug.Log("Clicked on object");
                HandleClickOnObject(clickedObjects[0]);
            }
            else
            {
                HandleClickOnGround(worldPosClick);
            }

            startSelectionDrag = Vector3.zero;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            DeselectObjects();
            startSelectionDrag = Vector3.zero;
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

    private Vector3 ConvertMousePosToWorldSpace(Vector3 mousePosition) {

        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(mousePosition), out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;

    }


    private void HandleClickOnGround(Vector3 position)
    {
        Debug.Log("Click on ground");
        foreach (BaseObject obj in selectedObjects)
        {
            obj.OnGroundClick(position);
        }
    }


    private void SelectObjects(List<BaseObject> clickedObjects)
    {
        DeselectObjects();
        foreach (BaseObject o in clickedObjects)
        {
            o.OnSelect();
        }
        selectedObjects = clickedObjects;
    }

    private void DeselectObjects() {
        foreach(BaseObject o in selectedObjects)
        {
            o.OnUnselect();
        }
        selectedObjects.Clear();
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
