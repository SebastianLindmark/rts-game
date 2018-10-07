using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : BaseObject
{


    public override void OnEnemyClick(BaseObject target)
    {

    }

    public override void OnGroundClick(Vector3 target)
    {

    }

    public override void OnSelect()
    {
        base.OnSelect();   
    }

    public override void OnUnselect()
    {
        base.OnUnselect();
    }

    public override void Attack(BaseObject target)
    {
        
    }


    // Use this for initialization
    public override void Start () {
        base.Start();
    }
    
    // Update is called once per frame
    public override void Update () {
        base.Update();

    }

}
