using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour, EnemyDetectedListener{




    public float damage = 0; //should be moved to a data class
    public float attackRange = 50;

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
		
	}

    public void EnemyInRangeDetected(BaseObject enemy)
    {
        Debug.Log("Enemy in range detected");
        Attack(enemy);
    }

    public void HitByEnemyDetected(BaseObject enemy)
    {
     
    }


    private void Attack(BaseObject target) {
        attackOpponent = target;
        attackState = true;


    }
}
