using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : BaseObject, ToolbarClickListener
{

    public List<BaseObject> spawnableUnits;

    public override void OnEnemyClick(BaseObject target)
    {

    }

    public override void OnGroundClick(Vector3 target)
    {

    }

    public override void OnSelect()
    {
        ToolbarController toolbarController = GameObject.Find("Toolbar").GetComponent<ToolbarController>();
        toolbarController.PopulateToolbar(spawnableUnits, GetPlayer(), this);
    }

    public override void OnUnselect()
    {
        
    }

    public void OnToolBarClick(BaseObject clickedObj)
    {
        if (clickedObj.unitCost < GetPlayer().availableFunds)
        {
            Vector3 initalPosition = transform.position + new Vector3(Random.Range(5,10), 5, Random.Range(5, 10)); //add spacing
            BaseObject instantiated = new BaseFactory().CreateUnit(this, clickedObj,initalPosition);            
        }
        else
        {
            Debug.Log("Invalid funds");
        }
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        //GameObject gameObject = Resources.Load("Prefabs/Tank_02_Prefeb") as GameObject;
    }
    
    // Update is called once per frame
    public override void Update () {
        base.Update();

    }

}
