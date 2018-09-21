using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler
{

    private SelectionState selectedState;

    public SelectionHandler() {
        selectedState = new Unselected(this);
    }

    public void Unselect(List<BaseObject> objs)
    {
        selectedState.Unselect(objs);
    }

    public void Select(Vector3 clickVector, List<BaseObject> selectedObjects)
    {

        selectedState.Select(clickVector, selectedObjects);
    }

    public void SwitchState(SelectionState newState)
    {
        selectedState = newState;
    }
}
