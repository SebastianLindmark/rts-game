using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuiltObjects : ObjectLifecycleListener {

    private List<BaseObject> builtBuildings = new List<BaseObject>();

    public void AddBuilding(BaseBuilding obj) {
        obj.AddLifecycleListener(this);
        builtBuildings.Add(obj);

    }

    public List<BaseObject> GetBuildings() {
        return builtBuildings;
    }

    public void onCreated(BaseObject baseObject)
    {
        
    }

    public void onRemoved(BaseObject baseObject)
    {
        builtBuildings.Remove(baseObject);
    }
}
