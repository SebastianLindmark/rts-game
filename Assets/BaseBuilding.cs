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
        toolbarController.PopulateToolbar(spawnableUnits, GetOwner(), this);
    }

    public override void OnUnselect()
    {
        
    }

    public void OnToolBarClick(BaseObject clickedObj)
    {
        Debug.Log("Building item was clicked");
        if (clickedObj.unitCost < GetOwner().availableFunds)
        {
            BaseObject instantiated = new BaseFactory().CreateUnit(this, clickedObj);
            instantiated.transform.position = transform.position + new Vector3(5, 5, 0); //add spacing
        }
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        GameObject gameObject = Resources.Load("Prefabs/ExampleUnit") as GameObject;
        spawnableUnits.Add(gameObject.GetComponent<BaseObject>());
    }
    
    // Update is called once per frame
    public override void Update () {
        base.Update();

    }

}
