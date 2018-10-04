using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyDetector : MonoBehaviour {

    private List<EnemyDetectedListener> listeners = new List<EnemyDetectedListener>();

    private float attackRange;

    private AttackRule attackRule;

    private BaseObject attachedBaseObj;

	void Start () {
        attackRange = GetComponent<AttackHandler>().attackRange;
        attachedBaseObj = GetComponent<BaseObject>();
	}

    public void RegisterEnemyDetectorListener(EnemyDetectedListener listener) {
        listeners.Add(listener);
    }

    public void Setup(AttackRule attackRule, float attackRange)
    {
        this.attackRule = attackRule;
        this.attackRange = attackRange;
    }

	void Update () {
        int layerMask = (~(1 << LayerMask.NameToLayer("Ground")));
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange,layerMask);

        for(int i = 0; i < hitColliders.Length; i++)
        {

            BaseObject collidingObject = GetBaseObject(hitColliders[i]);

            if (collidingObject != null && attackRule.canAttack(attachedBaseObj, collidingObject)) {
                NotifyListeners(collidingObject);
                return;
            }

            
        }
	}

    BaseObject GetBaseObject(Collider collider) {
        GameObject parent = collider.transform.root.gameObject;
        return parent.GetComponent<BaseObject>();
    }


    public bool IsObjectInRange(GameObject target)
    {
        return attackRange > Vector3.Distance(target.transform.position, transform.position);
    }

    private void NotifyListeners(BaseObject detectedEnemy) {
        foreach(EnemyDetectedListener l in listeners)
        {
            l.EnemyInRangeDetected(detectedEnemy);
        }
    }

}
