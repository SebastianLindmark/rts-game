using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : SelectionState
{
    private SelectionHandler parent;

    private List<BaseObject> selectedObjects;

    public Selected(SelectionHandler selection, List<BaseObject> s) {
        selectedObjects = s;
        parent = selection;
    }

    public void Unselect(List<BaseObject> objs)
    {
        foreach (BaseObject obj in objs)
        {
            obj.OnUnselect();
        }
        
        parent.SwitchState(new Unselected(parent));

    }

    public void Select(float x,float y, List<BaseObject> clickedObjects)
    {
        BaseObject clickedObject = clickedObjects[0];
        
        foreach(BaseObject obj in selectedObjects){
            obj.OnSelectClick(x, y, clickedObject);
        }
        
    }
}
