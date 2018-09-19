using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    public Camera cam;

    private List<BaseObject> inputListeners = new List<BaseObject>();
    private SelectionHandler selection;
    

    public void RegisterListener(BaseObject listener) {
        inputListeners.Add(listener);
    }

    void NotifyClickListeners(float x, float y) {


        Vector3 worldCoords = cam.ScreenToWorldPoint(new Vector3(x, y, 0));
        x = worldCoords.x;
        y = worldCoords.y;

        List<BaseObject> selectedObjects = new List<BaseObject>();

        foreach (BaseObject obj in inputListeners)
        {
            if (obj.Within(x, y)) {
                selectedObjects.Add(obj);
            }
        }

        selection.Select(x, y, selectedObjects);

    }

    void NotifyDeselectListeners(float x, float y) {
        foreach (BaseObject obj in inputListeners)
        {
            obj.OnUnselect();
        }
    }

    void Start () {

    }
    
    
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            NotifyClickListeners(Input.mousePosition.x,Input.mousePosition.y);
        }


        if (Input.GetMouseButtonDown(1)) {
            NotifyDeselectListeners(Input.mousePosition.x, Input.mousePosition.y);
        }
        
    }
}
