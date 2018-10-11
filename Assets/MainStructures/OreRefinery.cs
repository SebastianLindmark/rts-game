using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRefinery : SpawnableBuilding {


    private GameResourceManager resourceManager;

    public BaseObject spawnOnCreation;

    public GameObject entrancePosition;

    public override void Start()
    {
        base.Start();
        resourceManager = GameObject.Find("GameControllerObject").GetComponent<GameResourceManager>();

        if (spawnOnCreation)
        {
            OnToolBarClick(spawnOnCreation);
        }
    }

    public Vector3 GetEntrancePosition() {
        return entrancePosition.transform.position;
    }


    public void AddResources(int coins) {
        resourceManager.addResource(0, coins);
    }

}
