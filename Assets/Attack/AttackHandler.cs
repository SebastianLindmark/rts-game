using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour, EnemyDetectedListener{



    private float lastShotTimestamp = 0;

    public float damage = 0; //should be moved to a data class
    public float attackRange = 50;
    public float fireRate = 2;


    public bool attackState = false;
    public BaseObject attackOpponent;


    public GameObject bulletPrefab;
    public GameObject projectilePosition;
    public GameObject explosionPrefab;

    public AttackRule attackRule = new StandardAttackRule();

    void Start () {
        RangedEnemyDetector red = GetComponent<RangedEnemyDetector>();
        if (red)
        {
            red.Setup(attackRule, attackRange);
            red.RegisterEnemyDetectorListener(this);
        }
	}
	
	
	void Update () {

        if (attackState)
        {

            if (attackOpponent != null && GetComponent<RangedEnemyDetector>().IsObjectInRange(attackOpponent.gameObject))
            {
                Shoot(attackOpponent);
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

    public void AttackEnemy(BaseObject target) {

        float distance = Vector3.Distance(transform.position, target.transform.position);
        
        if (distance > attackRange) {
            Vector3 pointAroundTarget = Random.insideUnitCircle.normalized * attackRange / 2;
            if (GetComponent<BaseUnit>() != null) {
                GetComponent<BaseUnit>().Walk(pointAroundTarget + target.transform.position);
            }
            
        }

        attackState = true;
        attackOpponent = target;
    }

    public void AbortAttack() {
        attackState = false;
        attackOpponent = null;
    }


    private void Shoot(BaseObject enemy) {

        TurretRotation tr = GetComponent<TurretRotation>();

        if ((tr == null || (tr != null &&  tr.IsRotationFinished())) && Time.time > fireRate + lastShotTimestamp)
        {
            lastShotTimestamp = Time.time;

            GameObject spawned = Instantiate(this.bulletPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
            GameObject explosion = Instantiate(this.explosionPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
            spawned.GetComponent<Bullet>().Setup(enemy);


        }


    }

}
