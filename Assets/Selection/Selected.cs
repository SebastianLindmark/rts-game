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

    public void Select(Vector3 clickVector, List<BaseObject> clickedObjects)
    {

        /* BaseObject clickedObject = null;
         if (clickedObjects.Count != 0)
         {
             clickedObject = clickedObjects[0];

             if (clickedObject.GetOwner().Equals())
             {

             }


         }



         foreach(BaseObject obj in selectedObjects){
             obj.OnSelectClick(clickVector, clickedObject);
         }

     }*/
    }
}
