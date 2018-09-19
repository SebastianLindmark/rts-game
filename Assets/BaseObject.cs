using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour , IBaseObject {

    private Player owner;

    public GameObject inputManagerGameObject;


    public Player GetOwner()
    {
        return owner;
    }

    public void SetOwner(Player p)
    {
        owner = p;
    }

    public abstract bool Within(float x, float y);

    public abstract void OnSelect();
    public abstract void OnSelectClick(float x, float y,BaseObject target);
    public abstract void OnUnselect();



    virtual public void Start () {

        InputManager inputManager = inputManagerGameObject.GetComponent<InputManager>();
        inputManager.RegisterListener(this);
    }


    virtual public void Update () {
        
    }
    

}
