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


    public bool IsRotationFinished() {
        Vector3 rotation = tankTurretTransform.rotation.eulerAngles;
        return Mathf.Abs(Mathf.Abs(targetRotation) - Mathf.Abs(rotation.y)) < 0.5f;
    }



    public void SetTargetRotation(float rotation) {
        targetRotation = rotation + defaultRotation.y;
    }


    private bool PIDTowardsTargetAngle(Transform tankTurretTransform, float targetAngle, float k)
    {
        if (tankTurretTransform)
        {
            Vector3 rotation = tankTurretTransform.rotation.eulerAngles;

            if (Mathf.Abs(Mathf.Abs(targetRotation) - Mathf.Abs(rotation.y)) < 0.5f)
            {
                return true;
            }

            tankTurretTransform.rotation = Quaternion.Slerp(tankTurretTransform.rotation, Quaternion.Euler(0, targetRotation, 0), Time.deltaTime * 6f);
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
