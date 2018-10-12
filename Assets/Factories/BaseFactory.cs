using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour, IBaseFactory {


    public BaseObject CreateUnit(BaseObject self,BaseObject type,Vector3 position)
    {
        
        BaseObject instantiated = Instantiate<BaseObject>(type,position, Quaternion.Euler(new Vector3(1,0,0)));
        instantiated.SetPlayer(self.GetPlayer());
        return instantiated;
    }


    public BaseObject CreateUnit(Player self, BaseObject type, Vector3 position)
    {

        BaseObject instantiated = Instantiate<BaseObject>(type, position, Quaternion.Euler(new Vector3(1, 0, 0)));
        instantiated.SetPlayer(self);
        return instantiated;
    }


    void Start () {
        
    }
    
    
    void Update () {
        
    }
}
