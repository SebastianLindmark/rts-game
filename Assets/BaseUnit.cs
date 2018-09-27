using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseUnit : BaseObject {

    private AttackRule attackRule = new StandardAttackRule(); //Should be DI.




    public override void Start () {
        base.Start();
    }


/*    public override void Update () {
        
    }
    
    */
    public override void OnSelect()
    {
        Debug.Log("Im selected");
        //Do your thaaang
    }


    public override void OnGroundClick(Vector3 target)
    {
        Walk(target);
    }


    public override void OnEnemyClick(BaseObject target)
    {
        //We need some kind of behaviour pattern here. Example: What happends if selected -> press on own/enemy building?

        if (target != null)
        {
            Debug.Log("My owner is " + GetOwner());
            Debug.Log("target owner is " + target.GetOwner());
            if (attackRule.canAttack(GetOwner(), target)) {
                //attack
            }
            else
            {
                //show feedback
            }
        }
    }

    public virtual void Walk(Vector3 direction)
    {
        IAstarAI ai = GetComponent<IAstarAI>();
        ai.destination = direction;
        ai.SearchPath();
    }


    public override void OnUnselect()
    {
        Debug.Log("Unselected");
    }

    
}
