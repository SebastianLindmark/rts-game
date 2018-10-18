using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBuilding : BaseBuilding, ToolbarClickListener {


    public GameObject spawnLocation;

    public List<BaseUnit> spawnableUnits;

    private ToolbarController toolbarController;

   


    public override void OnCreated()
    {
        base.OnCreated();
        toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();

        
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer());

        foreach (BaseUnit unit in spawnableUnits)
        {
            pEnv.GetBuildableObjects().AddObject(GetPlayer(), unit, this);
        }
    }

    public override void RemoveObject()
    {
        base.RemoveObject();

        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer());
        //Todo Should investigate why this would ever return null. (Triggered by the AIBuildingPlacer)

        if (pEnv != null) {
            for (int i = 0; i < spawnableUnits.Count; i++) {
                pEnv.GetBuildableObjects().RemoveElement(spawnableUnits[i]);
            }
        }

    }

    public override BaseObject OnToolBarClick(BaseObject clickedObj)
    {
        
        if (clickedObj.productionCost < GetAvailableGold())
        {
            Vector3 initalPosition;
            if (!spawnLocation)
            {
                 initalPosition = GetRandomCloseLocation(); //add spacing
            }
            else
            {
                initalPosition = spawnLocation.transform.position;
            }

            return new BaseFactory().ProduceUnit(GetPlayer(), clickedObj, initalPosition);

        }
        else
        {
            Debug.Log("Insufficient funds");
            return null;
        }
    }


    private Vector3 GetRandomCloseLocation() {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * 20;
        Vector3 spawn = Vector3.zero;
        spawn.x = randomCirclePoint.x;
        spawn.z = randomCirclePoint.y;
        spawn.y = 5;

        return spawn + transform.position;
    }

    protected int GetAvailableGold() {
        return PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer()).GetGoldResource().GetAvailableResources();
    }

    protected int GetAvailableOil()
    {
        return PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer()).GetOilResource().GetAvailableResources();
    }



}
