using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICamp : MonoBehaviour {

    private Vector3 campLocation = new Vector3(0,0,0);

    private float campRadius = 25; //Will be changed dynamically as camp grows.

	void Start () {
		
	}
	
	void Update () {


    }

    public void SetCampLocation(Vector3 l)
    {
        campLocation = l;
    }

    public Vector3 GetCampLocation()
    {
        return campLocation;
    }

    public float GetCampRadius()
    {
        return campRadius;
    }

}
