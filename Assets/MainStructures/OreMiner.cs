using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreMiner : BaseUnit {

    public enum MineState {
        IDLE, SEARCH, MINE, RETURN, UNLOAD, EXIT
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

        FindOreRefinery();
        Reset();
    }

    public void SetMineState(MineState state) {
        mineState = state;
    }

    private OreRefinery FindOreRefinery() {
        OreRefinery[] refineries = FindObjectsOfType<OreRefinery>();
        for (int i = 0; i < refineries.Length; i++)
        {

            if (refineries[i].GetPlayer().Equals(GetPlayer()))
            {
                return refineries[i];
            }
        }
        return null;
    }

    public override void OnEnemyClick(BaseObject o) {
        //Do nothing
    }

    public override void Update()
    {

        base.Update();

        if (refineryHomebase == null) {
            refineryHomebase = FindOreRefinery();
            if (refineryHomebase == null) {
                return;
            }
        }


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
                if ((ai.reachedEndOfPath && !ai.pathPending) || ai.remainingDistance < 6) {
                    mineState = MineState.MINE;
                    mineTime = Time.time;
                }

                break;
            case MineState.MINE:

                if (mineTime + mineInterval < Time.time)
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

                if ((ai.reachedEndOfPath && !ai.pathPending) && refineryHomebase)
                {

                    enterUnloadState();


                    Vector3 _direction = (refineryHomebase.transform.position - transform.position).normalized;

                    Quaternion _lookRotation = Quaternion.LookRotation(_direction);

                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 0.35f);



                    transform.position = Vector3.MoveTowards(transform.position, refineryHomebase.GetUnloadPosition(), 0.2f);
                    if (Vector3.Distance(transform.position, refineryHomebase.GetUnloadPosition()) < 1f) {
                        mineState = MineState.UNLOAD;
                    }
                }
                break;
            case MineState.UNLOAD:
                refineryHomebase.AddResources(inventoryValue);
                Reset();
                mineState = MineState.EXIT;
                break;
            case MineState.EXIT:
                
                if (refineryHomebase) { 
                    transform.position = Vector3.MoveTowards(transform.position, refineryHomebase.GetEntrancePosition(), 0.3f);
                    if (Vector3.Distance(transform.position, refineryHomebase.GetEntrancePosition()) < 2f)
                    {
                        GetComponent<Collider>().enabled = true;
                        ai.gravity = savedGravity;
                        ai.enabled = true;
                        mineState = MineState.IDLE;
                    }
                }
                break;

        }




    }

    public void enterUnloadState() {
        GetComponent<Collider>().enabled = false;
        AIPath a = GetComponent<AIPath>();
        a.gravity = new Vector3(0, 0, 0);
        a.enabled = false;
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
