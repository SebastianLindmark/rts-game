using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRefinery : SpawnableBuilding {

    public BaseObject spawnOnCreation;

    public GameObject entrancePosition;

    public GameObject smokeParticleEffect;

    private bool showSmoke = false;

    private float smokeTimeout = 5;
    private float smokeTimestamp;

    public GameObject smokeLocation1;
    public GameObject smokeLocation2;

    private GameResource goldResource;

    private ParticleSystem ps;


    public override void Start()
    {
        base.Start();
        goldResource = PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer()).GetGoldResource();

    }

    public Vector3 GetEntrancePosition() {
        return entrancePosition.transform.position;
    }

    public Vector3 GetUnloadPosition() {
        Vector3 unloadPos = Vector3.zero;
        unloadPos.x = entrancePosition.transform.position.x;
        unloadPos.y = entrancePosition.transform.position.y;
        unloadPos.z = entrancePosition.transform.position.z + 13;
        return unloadPos;

    }

    public override void OnCreated()
    {
        base.OnCreated();

        if (spawnOnCreation)
        {
            Vector3 pos = transform.position;
            pos.y = 1; //Temporary, make constant or calculate height of spawned vehicle instead
            OreMiner spawnedMiner = new BaseFactory().CreateUnit(this, spawnOnCreation, pos).GetComponent<OreMiner>();
            spawnedMiner.SetMineState(OreMiner.MineState.EXIT);
            spawnedMiner.enterUnloadState();
            

        }

        smokeLocation1 = Instantiate(smokeParticleEffect, smokeLocation1.transform.position, smokeLocation1.transform.rotation);
        smokeLocation1.transform.parent = this.transform;
        Debug.Log(smokeLocation1);
        ps = smokeLocation1.GetComponent<ParticleSystem>();


    }
    
    public void AddResources(int gold) {
        goldResource.AddResource(gold);
        enableSmokeEffect();
    }


    private void enableSmokeEffect() {
        showSmoke = true;
        smokeTimestamp = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (showSmoke)
        {
            if (smokeTimestamp + smokeTimeout < Time.time)
            {
                ParticleSystem.MainModule psMain = ps.main;
                psMain.startColor = Color.gray;
                showSmoke = false;
            }
            else
            {
                ParticleSystem.MainModule psMain = ps.main;
                psMain.startColor = Color.yellow;
            }
        }

    }

}
