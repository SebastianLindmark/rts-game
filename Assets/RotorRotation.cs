using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotation : MonoBehaviour {


    private Transform rotorTransform;

    private Transform rotorCenter;

    bool rotorEnabled = false;

    void Start () {
        //Chopper_01_MainPropel	
        Transform transform = GetComponent<Transform>();
        rotorTransform = GetChildTransform(transform, "Chopper_01_MainPropel");
        Debug.Log(rotorTransform.name);
        rotorCenter = GetChildTransform(transform, "Chopper_01_MainProperllerRader");
    }


    public void Enable()
    {
        rotorEnabled = true;
    }

    public void Disable()
    {
        rotorEnabled = false;
    }


	void Update () {

        rotorTransform.transform.RotateAround(rotorCenter.transform.position, new Vector3(0, 10, 0), 10);
        //rotorTransform.transform.RotateAround()
	}

    public Transform GetChildTransform(Transform parent, string target)
    {
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
