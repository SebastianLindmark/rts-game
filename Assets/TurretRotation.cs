using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour, AttackListener {

    
   private Transform tankTurretTransform;
   private float targetRotation;
   private AttackHandler attackHandler;

   

    void Start () {
        Transform transform = GetComponent<Transform>();
        tankTurretTransform = GetChildTransform(transform,"UpperBody");

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


   


    public bool RotationDone() {
        Vector3 rotation = transform.rotation.eulerAngles;
        return Mathf.Abs(Mathf.Abs(targetRotation) - Mathf.Abs(rotation.y)) < 1f;
    }

    public void SetTargetRotation(float rotation) {
        targetRotation = rotation + 90;
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

            Vector3 position = new Vector3(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * 5f);
        }
        return false;

    }


    public void onAttack(BaseObject target)
    {
       
        Debug.Log("Other " + target.gameObject.transform.position);
        Debug.Log("Me " + gameObject.transform.position);
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
