using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDivision : ObjectLifecycleListener  {

    private List<BaseObject> division = new List<BaseObject>();

    public static int UnitsInDivision = 4;


    public AIDivision(List<BaseObject> objs) {
        objs.ForEach(obj => AddObject(obj));
    }

    public void AddObject(BaseObject obj) {
        obj.AddLifecycleListener(this);
        division.Add(obj);
    }

    public void onCreated(BaseObject baseObject)
    {
        
    }

    public void onRemoved(BaseObject baseObject)
    {
        division.Remove(baseObject);
    }

    public List<BaseObject> getDivision() {
        return division;
    }
}
