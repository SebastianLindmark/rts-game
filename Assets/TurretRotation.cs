using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour, AttackListener {

    
   private Transform tankTurretTransform;
   private float targetRotation;
   private AttackHandler attackHandler;

    private Vector3 defaultRotation;

    void Start () {
        Transform transform = GetComponent<Transform>();
        tankTurretTransform = GetChildTransform(transform,"UpperBody");
        defaultRotation = tankTurretTransform.eulerAngles;

        Debug.Log("I was created with a rotation of " + defaultRotation);
        


        attackHandler = GetComponent<AttackHandler>();
        SetTargetRotation(transform.root.eulerAngles.y);
    }

    
    
    void Update () {


        if (attackHandler.attackState)
        {
            onAttack(attackHandler.attackOpponent);
        }

        PIDTowardsTargetAngle(tankTurretTransform, targetRotation, 50);
    }


  
    public void SetTargetRotation(float rotation) {
        targetRotation = rotation + defaultRotation.y;
    }


    private bool PIDTowardsTargetAngle(Transform transform, float targetAngle, float k)
    {
        if (transform)
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            if (Mathf.Abs(Mathf.Abs(targetRotation) - Mathf.Abs(rotation.y)) < 1f)
            {
                return true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * 5f);
        }
        return false;

    }


    public void onAttack(BaseObject target)
    {
       
        Vector2 targetVector = new Vector2(target.gameObject.transform.position.x, target.gameObject.transform.position.z);
        Vector2 meVector = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        Vector2 targetDir = meVector - targetVector;
        float angle = Vector2.Angle(targetDir, transform.up);
        SetTargetRotation(angle + 180);
    }

    public Transform GetChildTransform(Transform parent, string target) {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name.Contains(target))
            {
                return child;
            }
        }
        return null;

    }

    
}
