using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage = 10;

    private BaseObject target;

    private BaseObject owner;

    private Collider[] colliders;

	void Start () {
		
	}

    public void Setup(BaseObject owner, BaseObject target) {
        this.target = target;
        this.owner = owner;

        colliders = target.GetComponentsInChildren<BoxCollider>();
    }

	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 50f * Time.deltaTime);
        }
        else {
            Destroy(gameObject);
        }
        
        
    }

    bool CollidedWithTarget(Collision collision) {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == collision.gameObject)
            {
                return true;
            }
        }
        return false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (CollidedWithTarget(collision))
        {
            target.DealDamage(damage);
            Destroy(gameObject);
        }
                
    }
}
