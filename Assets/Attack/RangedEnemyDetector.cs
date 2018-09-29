using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyDetector : MonoBehaviour {

    private List<EnemyDetectedListener> listeners = new List<EnemyDetectedListener>();

    private float attackRange;
	
	void Start () {
        attackRange = GetComponent<AttackHandler>().attackRange;
	}

    public void RegisterEnemyDetectorListener(EnemyDetectedListener listener) {
        listeners.Add(listener);
    }

    public void Setup(float range)
    {
        attackRange = range;
    }

	void Update () {
        int layerMask = ((1 << LayerMask.NameToLayer("Building")));
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange,layerMask);
        Debug.Log(hitColliders.Length);
        if (hitColliders.Length > 0)
        {
            NotifyListeners(hitColliders[0].gameObject);
        }

        
	}

    private void NotifyListeners(GameObject detectedEnemy) {
        foreach(EnemyDetectedListener l in listeners)
        {
            l.EnemyInRangeDetected(detectedEnemy.GetComponent<BaseObject>());
        }
    }

}
