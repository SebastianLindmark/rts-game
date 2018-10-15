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

    public BaseObject ProduceUnit(Player player, BaseObject type, Vector3 position) {
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(player);
        if (pEnv.GetOilResource().GetAvailableResources() >= type.unitCost) {
            pEnv.GetOilResource().RemoveResource(type.unitCost);
            return CreateUnit(player, type, position);
        }


        return null;

    }

}
