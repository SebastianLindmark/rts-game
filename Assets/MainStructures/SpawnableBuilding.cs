using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBuilding : BaseBuilding, ToolbarClickListener {


    public GameObject spawnLocation;

    public override void OnSelect()
    {
        base.OnSelect();
        ToolbarController toolbarController = GameObject.Find("Toolbar").GetComponent<ToolbarController>();
        toolbarController.PopulateToolbar(spawnableUnits, GetPlayer(), this);
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
        return transform.position + new Vector3(Random.Range(5, 10), 5, Random.Range(5, 10));
    }



}
