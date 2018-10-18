using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : MonoBehaviour, IBaseFactory {


    public BaseObject CreateObject(BaseObject self,BaseObject type,Vector3 position)
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
        if (pEnv.GetOilResource().GetAvailableResources() >= type.productionCost) {
            pEnv.GetOilResource().RemoveResource(type.productionCost);
            return CreateUnit(player, type, position);
        }

        return null;

    }

    public BaseObject ProduceBuilding(Player player, BaseBuilding type, Vector3 position)
    {
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(player);
        if (pEnv.GetGoldResource().GetAvailableResources() >= type.productionCost)
        {
            pEnv.GetGoldResource().RemoveResource(type.productionCost);
            return CreateUnit(player, type, position);
        }


        return null;

    }



}
