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
        ToolbarController toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();
        foreach (BaseBuilding building in advancementBuildings)
        {
            toolbarController.AddToolbarField(building, GetPlayer(), this);
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

    public void OnToolBarClick(BaseObject obj)
    {
        BuildingPlacer bp = GameObject.Find("GameControllerObject").GetComponent<BuildingPlacer>();
        bp.OnToolBarClick(obj);
    }
}
