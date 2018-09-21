using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player owner;

    public GameObject inputManagerGameObject;


    public Player GetOwner()
    {
        //return owner;
        
        return GetComponent<PlayerScript>().GetPlayer();
    }

    public void SetOwner(Player p)
    {
        GetComponent<PlayerScript>().GetPlayer().setPlayerId(p.getPlayerId()); //This wont work if comparing references.
        
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


    public void Update () {
    }
    

}
