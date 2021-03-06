﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player player;

    public int productionCost;

    public float health = 100;

    public string printableName;

    public GameObject selectionMarker;

    public Sprite thumbnail;
    
    private GameObject inputManagerGameObject;

    private GameObject healthbar;

    private List<ObjectLifecycleListener> lifecycleListeners = new List<ObjectLifecycleListener>();



    virtual public void Start()
    {
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.RegisterListener(this);


        PlacementEffect teamColor = GetComponent<PlacementEffect>();
        if (teamColor == null) {
            teamColor = gameObject.AddComponent<PlacementEffect>();
        }

        teamColor.ApplyColorToOriginal(GetPlayer().GetTeamColor());        
    }

    public Player GetPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Creating new player from script.");
            player = new Player();
        }

        return player;
    }

    public void SetPlayer(Player p)
    {
        player = p; //This wont work if comparing references.
        
    }

    public void DealDamage(float damage) {
        if (damage > 0)
        {
            health -= damage;
        }

        if(health <= 0)
        {
            ZeroHealth();
        }
    }

    public virtual void ZeroHealth()
    {
        RemoveObject();
    }


    public virtual void RemoveObject()
    {
        NotifyObjectRemoval();
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.UnregisterListener(this);
        //Should clear listeners here
        Destroy(gameObject);
    }

    public bool Within(Vector3 position) {

        Collider c = GetComponent<Collider>();
        if (c) {
            return c.bounds.Contains(position);
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        for(int i =0; i < colliders.Length; i++)
        {
            if (colliders[i].bounds.Contains(position))
            {
                return true;
            }
        }
        return false;        
    }

   

    public void AddLifecycleListener(ObjectLifecycleListener listener)
    {
        lifecycleListeners.Add(listener);
    }

    protected void NotifyObjectRemoval()
    {
        foreach (ObjectLifecycleListener lcl in lifecycleListeners) {
            lcl.onRemoved(this);
        }
    }


    protected void NotifyObjectCreation()
    {
        foreach (ObjectLifecycleListener lcl in lifecycleListeners)
        {
            lcl.onCreated(this);
        }
    }


    public void ShowHealthbar(bool show)
    {
     
        if (show)
        {
            
            if (!healthbar)
            {
                healthbar = Instantiate(Resources.Load("Prefabs/Healthbar") as GameObject, transform);
                healthbar.transform.parent = gameObject.transform.root;
            }
            else {
                Debug.Log("Healthbar is already displaying");
            }
            
        }
        else if(!show && healthbar){
            
            DestroyImmediate(healthbar);
        }
        
    }

    public virtual void OnSelect() {
        ShowHealthbar(true);
    }
    public virtual void OnUnselect() {
        ShowHealthbar(false);
    }

    public abstract void OnEnemyClick(BaseObject target);
    public abstract void OnGroundClick(Vector3 target);

    public abstract void Attack(BaseObject target);


    


    virtual public void Update () {
    }
    

}
