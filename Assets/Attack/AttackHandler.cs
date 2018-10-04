using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour, EnemyDetectedListener{




    public float damage = 0; //should be moved to a data class
    public float attackRange = 50;

    public float fireRate = 2;

    private float lastShotTimestamp = 0;

    public bool attackState = false;
    public BaseObject attackOpponent;

    void Start () {
        RangedEnemyDetector red = GetComponent<RangedEnemyDetector>();
        if (red)
        {
            red.RegisterEnemyDetectorListener(this);
        }
	}
	
	
	void Update () {

        if (attackState)
        {

            if (attackOpponent != null && GetComponent<RangedEnemyDetector>().IsObjectInRange(attackOpponent.gameObject))
            {
                Attack(attackOpponent);
            }
            else
            {
                attackState = false;
            }

        }
        else {
            
        }

	}

    public void EnemyInRangeDetected(BaseObject enemy)
    {
        attackOpponent = enemy;
        attackState = true;
    }

    public void HitByEnemyDetected(BaseObject enemy)
    {
     
    }


    private void Attack(BaseObject enemy) {
        if (GetComponent<TurretRotation>().IsRotationFinished() && Time.time > fireRate + lastShotTimestamp)
        {
            lastShotTimestamp = Time.time;
            GetComponent<BaseUnit>().Attack(attackOpponent);

        }


    }

}
