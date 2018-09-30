using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player player;

    public int unitCost;


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
        
        return GetComponent<Collider>().bounds.Contains(clickVector);
    }

    public abstract void OnSelect();
    public abstract void OnEnemyClick(BaseObject target);
    public abstract void OnGroundClick(Vector3 target);
    public abstract void OnUnselect();



    virtual public void Start () {
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.RegisterListener(this);
    }


    virtual public void Update () {
    }
    

}
