using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : BaseObject
{

    public List<BaseObject> spawnableUnits;

    public override void OnEnemyClick(BaseObject target)
    {

    }

    public override void OnGroundClick(Vector3 target)
    {

    }

    public override void OnSelect()
    {
        
    }

    public override void OnUnselect()
    {
        
    }

    public override void Attack(BaseObject target)
    {
        
    }


    // Use this for initialization
    public override void Start () {
        base.Start();
        //GameObject gameObject = Resources.Load("Prefabs/Tank_02_Prefeb") as GameObject;
    }
    
    // Update is called once per frame
    public override void Update () {
        base.Update();

    }

}
