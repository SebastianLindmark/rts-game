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
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 4f * Time.deltaTime);
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
            Debug.Log(damage);
            Debug.Log("Collided with target " + target.name);
            target.DealDamage(damage);
            Destroy(gameObject);
        }
                
    }
}
