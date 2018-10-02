using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player player;

    public int unitCost;

    public float health = 100;

    public GameObject selectionMarker;

    private GameObject inputManagerGameObject;


    public Player GetPlayer()
    {
        //return owner;

        if (player == null)
        {
            Debug.LogWarning("Creating new player from script.");
            player = new Player();
        }

        return player;
    }

    public void SetPlayer(Player p)
    {
        if (player == null)
        {
            Debug.LogWarning("Creating new player from script.");
            player = new Player();
        }
        
        player.setPlayerId(p.getPlayerId()); //This wont work if comparing references.
        
    }

    public bool Within(Vector3 clickVector) {

        Collider c = GetComponent<Collider>();
        if (c) {
            return c.bounds.Contains(clickVector);
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        for(int i =0; i < colliders.Length; i++)
        {
            if (colliders[i].bounds.Contains(clickVector))
            {
                return true;
            }
        }
        return false;


        
    }

    public abstract void OnSelect();
    public abstract void OnUnselect();

    public abstract void OnEnemyClick(BaseObject target);
    public abstract void OnGroundClick(Vector3 target);

    public abstract void Attack(BaseObject target);


    virtual public void Start () {
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.RegisterListener(this);
    }


    virtual public void Update () {
    }
    

}
