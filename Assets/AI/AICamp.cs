using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICamp : MonoBehaviour {

    public GameObject campLocation;

    private float campRadius = 45; //Will be changed dynamically as camp grows.

	void Start () {
		
	}
	
	void Update () {


    }

    public void SetCampLocation(Vector3 l)
    {
        campLocation.transform.position = l;
    }

    public Vector3 GetCampLocation()
    {
        return campLocation.transform.position;
    }

    public float GetCampRadius()
    {
        return campRadius;
    }

}
