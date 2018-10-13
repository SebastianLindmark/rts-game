using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBuilding : BaseBuilding, ToolbarClickListener {


    public GameObject spawnLocation;

    public List<BaseUnit> spawnableUnits;

    private ToolbarController toolbarController;

    public override void Start()
    {
        base.Start();
    }


    public override void OnCreated()
    {
        base.OnCreated();
        toolbarController = GameObject.FindGameObjectWithTag("Toolbar").GetComponent<ToolbarController>();

        foreach (BaseUnit unit in spawnableUnits)
        {
            toolbarController.AddToolbarField(unit, GetPlayer(), this);
        }
    }

    public override void RemoveObject()
    {
        base.RemoveObject();
        for (int i = 0; i < spawnableUnits.Count; i++) {
            toolbarController.RemoveElement(spawnableUnits[i]);
        }

    }

    public void OnToolBarClick(BaseObject clickedObj)
    {
        if (clickedObj.unitCost < GetPlayer().availableFunds)
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
            
            BaseObject instantiated = new BaseFactory().CreateUnit(this, clickedObj, initalPosition);

        }
        else
        {
            Debug.Log("Invalid funds");
        }
    }


    private Vector3 GetRandomCloseLocation() {
        return transform.position + new Vector3(Random.Range(10, 15), 5, Random.Range(10, 15));
    }



}
