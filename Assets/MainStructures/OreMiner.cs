using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiner : BaseUnit {

    private enum MineState {
        IDLE, SEARCH,MINE,RETURN, UNLOAD,EXIT
    }

    private OreResource[] resources;

    private MineState mineState = MineState.IDLE;

    private AIPath ai;

    private int mineInterval = 5;

    private int inventorySize = 4;

    private int currentInventory = 0;

    private int inventoryValue = 0;

    private float mineTime = 0;

    private OreResource closestResource;

    private OreRefinery refineryHomebase;

    private List<Renderer> oreRenderers = new List<Renderer>();

    private Vector3 savedGravity;

    public override void Start()
    {
        base.Start();
        ai = GetComponent<AIPath>();
        resources = FindObjectsOfType<OreResource>();
        oreRenderers.Add(gameObject.transform.Find("Ore1").GetComponent<Renderer>());
        oreRenderers.Add(gameObject.transform.Find("Ore2").GetComponent<Renderer>());
        oreRenderers.Add(gameObject.transform.Find("Ore3").GetComponent<Renderer>());
        oreRenderers.Add(gameObject.transform.Find("Ore4").GetComponent<Renderer>());

        savedGravity = ai.gravity;
      

        OreRefinery[] refineries = FindObjectsOfType<OreRefinery>();
        for (int i = 0; i < refineries.Length; i++)
        {

            if (refineries[i].GetPlayer().Equals(GetPlayer())) {
                refineryHomebase = refineries[i];
                break;
            }
        }
        
        Reset();
    }

    public override void OnEnemyClick(BaseObject o) {
        //Do nothing
    }

    public override void Update()
    {
        base.Update();
        
        switch (mineState) {
            case MineState.IDLE:
                //check if both minerals and a ore factory exists, iff -> SEARCH
                float shortestDistance = Vector3.Distance(transform.position, resources[0].transform.position);
                closestResource = resources[0];
                for (int i = 1; i < resources.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, resources[i].transform.position);
                    if (distance < shortestDistance) {
                        shortestDistance = distance;
                        closestResource = resources[i];
                    }
                }
                
                ai.destination = closestResource.transform.position;
                ai.SearchPath();
                
                mineState = MineState.SEARCH;

                break;
            case MineState.SEARCH:
                if ( (ai.reachedEndOfPath && !ai.pathPending) || ai.remainingDistance < 5) {
                    mineState = MineState.MINE;
                    mineTime = Time.time;
                }

                break;
            case MineState.MINE:

                if(mineTime + mineInterval < Time.time)
                {
                    addOreToInventory(closestResource.getResourceValue());
                    mineTime = Time.time;
                }
                if (inventorySize <= currentInventory) {
                    if (refineryHomebase)
                    {
                        ai.destination = refineryHomebase.GetEntrancePosition();
                        ai.SearchPath();
                        mineState = MineState.RETURN;
                    }
                    
                }
            break;
            case MineState.RETURN:
                
                if ((ai.reachedEndOfPath && !ai.pathPending) && refineryHomebase) {

                    ai.gravity = new Vector3(0,0,0);
                    GetComponent<Collider>().enabled = false;

                    ai.destination = refineryHomebase.transform.position;
                    ai.SearchPath();
                    mineState = MineState.UNLOAD;
                }
                break;
            case MineState.UNLOAD:

                if ((ai.reachedEndOfPath && !ai.pathPending) && refineryHomebase)
                {
                    refineryHomebase.AddResources(inventoryValue);
                
                    Reset();
                    ai.destination = refineryHomebase.GetEntrancePosition();
                    ai.SearchPath();
                    mineState = MineState.EXIT;
                    
                }
                break;

            case MineState.EXIT:
                if (ai.reachedEndOfPath && !ai.pathPending)
                {
                    ai.gravity = savedGravity;
                    GetComponent<Collider>().enabled = true;
                    mineState = MineState.IDLE;
                }


                break;

        }

       


    }

    private void Reset()
    {
        currentInventory = 0;
        inventoryValue = 0;
        foreach(Renderer r in oreRenderers) {
            r.enabled = false;
        }
    }

    public void addOreToInventory(int oreValue) {
        currentInventory += 1;
        inventoryValue += oreValue;

        for (int i = 0; i < currentInventory; i++) {
            oreRenderers[i].enabled = true;
        }
    }


}
