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
        //AstarPath.active.UpdateGraphs(gameObject.GetComponent<Collider>().bounds);
        //AddMeshCollider(gameObject);
        //test();
    }


    private void test() {

        BoxCollider bc = GetComponentInChildren<BoxCollider>();
        if (bc != null) {
            BoxCollider newBc = gameObject.AddComponent<BoxCollider>();
            newBc.center = Vector3.zero;
            newBc.size = bc.bounds.size;
            Destroy(bc);
        }
        gameObject.AddComponent<DynamicGridObstacle>();


    }

    private void AddMeshCollider(GameObject containerModel)
    {
        // Add mesh collider
        MeshFilter meshFilter = containerModel.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.sharedMesh;
        }
        // Add mesh collider (convex) for each mesh in child elements.
        Component[] meshes = containerModel.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mesh in meshes)
        {
            MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh.sharedMesh;
        }
    }



    public override void OnSelect()
    {
        base.OnSelect();

    }

    public override void OnUnselect()
    {
        base.OnUnselect();
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

    public virtual void Walk(Vector3 direction)
    {
        IAstarAI ai = GetComponent<IAstarAI>();
        ai.destination = direction;
        ai.SearchPath();
        
    }


   



    public override void Attack(BaseObject target)
    {
        GameObject spawned = Instantiate(this.bulletPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
        GameObject explosion = Instantiate(this.explosionPrefab, projectilePosition.transform.position, projectilePosition.transform.rotation);
        spawned.GetComponent<Bullet>().Setup(this,target);

    }
}
