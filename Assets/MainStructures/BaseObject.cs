using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player player;

    public int unitCost;

    public float health = 100;

    public GameObject selectionMarker;

    private GameObject inputManagerGameObject;

    private GameObject healthbar;

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

    public void DealDamage(float damage) {
        if (damage > 0)
        {
            health -= damage;
        }

        if(health <= 0)
        {
            RemoveObject();
        }

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

    protected void RemoveObject()
    {
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.UnregisterListener(this);
        Destroy(gameObject);
    }

    public void ShowHealthbar(bool show)
    {
        if (show)
        {
            healthbar = Instantiate(Resources.Load("Prefabs/Healthbar") as GameObject, transform);
            healthbar.transform.parent = gameObject.transform.root;
        }
        else if(!show && healthbar){
            Destroy(healthbar);
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


    virtual public void Start () {
        InputManager inputManager = GameObject.Find("GameControllerObject").GetComponent<InputManager>();
        inputManager.RegisterListener(this);
    }


    virtual public void Update () {
    }
    

}
