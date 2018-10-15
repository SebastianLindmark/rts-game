using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : BaseObject, ToolbarClickListener
{


    public List<BaseBuilding> advancementBuildings;


    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

    }

    public virtual void OnCreated()
    {
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer());
        pEnv.GetBuiltObjects().Add(this);

        foreach (BaseBuilding unit in advancementBuildings)
        {
            pEnv.GetBuildableObjects().AddObject(GetPlayer(), unit, this);
        }

        NotifyObjectCreation();
    }

    public override void Attack(BaseObject target)
    {

    }

    public override void OnEnemyClick(BaseObject target)
    {

    }

    public override void OnGroundClick(Vector3 target)
    {

    }

    public virtual BaseObject OnToolBarClick(BaseObject obj)
    {
        BuildingPlacer bp = GameObject.Find("GameControllerObject").GetComponent<BuildingPlacer>();
        bp.OnToolBarClick(obj);
        return null;
    }
}
