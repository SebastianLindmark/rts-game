using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseUnit : BaseObject {

    private AttackRule attackRule = new StandardAttackRule(); //Should be DI.

    public GameObject projectilePosition;

    public GameObject bulletPrefab;

    public GameObject explosionPrefab;

    public override void Start () {
        base.Start();
        NotifyObjectCreation();
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
            if (attackRule.canAttack(GetPlayer(), target)) {
                //attack
                //Walk to the enemy position - our shooting range. The RangedEnemyDetector will then handle the attacking.
            }
            else
            {
                //show feedback
            }
        }
    }

    public virtual void Walk(Vector3 position)
    {
        IAstarAI ai = GetComponent<IAstarAI>();
        ai.destination = position;
        ai.SearchPath();
    }


   



    public override void Attack(BaseObject target)
    {
        GetComponent<AttackHandler>().AttackEnemy(target);
        //GameObject spawned = Instantiate(this.bulletPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
        //GameObject explosion = Instantiate(this.explosionPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
        //spawned.GetComponent<Bullet>().Setup(target);

    }
}
