using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreRefinery : SpawnableBuilding {


    private GameResourceManager resourceManager;

    public BaseObject spawnOnCreation;

    public GameObject entrancePosition;

    public GameObject smokeParticleEffect;

    private bool showSmoke = false;

    private float smokeTimeout = 5;
    private float smokeTimestamp;

    public GameObject smokeLocation1;
    public GameObject smokeLocation2;

    private ParticleSystem ps;

    public override void Start()
    {
        base.Start();
        resourceManager = GameObject.Find("GameControllerObject").GetComponent<GameResourceManager>();
    }

    public Vector3 GetEntrancePosition() {
        return entrancePosition.transform.position;
    }

    public override void OnPlaced()
    {
        base.OnPlaced();

        if (spawnOnCreation)
        {
            Vector3 pos = transform.position;
            pos.y = 1; //Temporary, make constant or calculate height of spawned vehicle instead
            OreMiner spawnedMiner = new BaseFactory().CreateUnit(this, spawnOnCreation, pos).GetComponent<OreMiner>();
            spawnedMiner.SetMineState(OreMiner.MineState.RETURN);
            
        }

        smokeLocation1 = Instantiate(smokeParticleEffect, smokeLocation1.transform.position, smokeLocation1.transform.rotation);
        smokeLocation1.transform.parent = this.transform;

        ps = smokeLocation1.GetComponent<ParticleSystem>();


    }

    public void AddResources(int coins) {
        resourceManager.addResource(0, coins);
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
