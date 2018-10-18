using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTank : BaseBuilding {

    private int oilUpdateInterval = 5;

    private int oilAmount = 20;

    public GameObject explosionPrefab;

	public override void Start () {
        InvokeRepeating("AddResourceOil", 5.0f, 5f);
        
    }


    private void AddResourceOil()
    {
        PlayerDataEnvironment.PlayerEnvironment pEnv = PlayerDataEnvironment.GetPlayerEnvironment(GetPlayer());
        pEnv.GetOilResource().AddResource(oilAmount);
    }



    public override void ZeroHealth()
    {
        Invoke("DelayRemovalEffect", Random.Range(0.3f,0.5f));
    }

    private void DelayRemovalEffect() {
        Instantiate(explosionPrefab, transform.position, Quaternion.Euler(new Vector3(1, 0, 0)));
        Invoke("dealExplosionDamage", 0f);
        base.ZeroHealth();
    }


    private void dealExplosionDamage()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, 30);
        foreach (Collider col in objectsInRange)
        {
            BaseObject enemy = col.transform.root.GetComponent<BaseObject>();
            if (enemy != null)
            {
                // linear falloff of effect
                float proximity = (transform.position - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / 30);

                Debug.Log("Dealing " + 130 * effect);
                enemy.DealDamage(130 * effect);
            }
            else {

                Debug.Log("Not an enemy ");
            }
        }
    }




}
