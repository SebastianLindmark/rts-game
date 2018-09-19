using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unselected : SelectionState {

    private SelectionHandler parent;

    public Unselected(SelectionHandler p) {
        parent = p;
    }

    public void Select(float x, float y, List<BaseObject> selectedObjects)
    {
        foreach (BaseObject selectedObject in selectedObjects) {
            selectedObject.OnSelect();
        }
    
        parent.SwitchState(new Selected(parent, selectedObjects));
        
    }

    public void Unselect(List<BaseObject> objs)
    {
        //ignore
    }

}
