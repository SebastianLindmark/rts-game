using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class BaseUnit : BaseObject {

    private AttackRule attackRule = new StandardAttackRule(); //Should be DI.

    public override void Start () {
        
    }


    public override void Update () {
        
    }

    public override bool Within(float x, float y) {
        return true;
    }

    public override void OnSelect()
    {
        Debug.Log("Im am selected");
        //Do your thaaang
    }


    public override void OnSelectClick(float x, float y, BaseObject target)
    {
        Debug.Log("Im am selected and clicked");
        //We need some kind of behaviour pattern here. Example: What happends if selected -> press on own/enemy building?

        if (target != null)
        {
            if (attackRule.canAttack(GetOwner(), target)) {
                //attack
            }
            else
            {
                //show feedback
            }
        }
        else
        {
            //Pressed on ground (or a non BaseObject)
            Walk(x,y);
        }

    }

    public virtual void Walk(float x, float y) {

        IAstarAI ai = GetComponent<IAstarAI>();
        ai.destination = new Vector3(x,y,transform.position.z);
        ai.SearchPath();
    }


    public override void OnUnselect()
    {

    }

    
}
