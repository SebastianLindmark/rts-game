using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour, IBaseFactory {


    public BaseObject CreateUnit(BaseObject self,BaseObject type)
    {
        
        BaseObject instantiated = Instantiate<BaseObject>(type);
        instantiated.SetOwner(self.GetOwner());
        return instantiated;
    }


    void Start () {
        
    }
    
    
    void Update () {
        
    }
}
