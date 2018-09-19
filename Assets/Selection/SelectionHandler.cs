using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler
{

    private SelectionState selectedState;

    SelectionHandler() {
        selectedState = new Unselected(this);
    }

    public void Unselect(List<BaseObject> objs)
    {
        selectedState.Unselect(objs);
    }

    public void Select(float x,float y,List<BaseObject> selectedObjects)
    {

        selectedState.Select(x, y, selectedObjects);
    }

    public void SwitchState(SelectionState newState)
    {
        selectedState = newState;
    }
}
