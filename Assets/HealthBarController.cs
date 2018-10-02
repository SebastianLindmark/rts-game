using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    // Use this for initialization
    private BaseObject player;
    private Slider slider;
    private Image healthColorImage;

    private Camera gameCamera;


    void Start () {

        player = transform.parent.GetComponent<BaseObject>();
        slider = GetComponentInChildren<Slider>();
        healthColorImage = slider.GetComponentInChildren<Image>();

        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        float health = player.health;
        slider.value = health / 100;
        healthColorImage.color = Color.Lerp(Color.red, Color.green, health / 100);

        transform.LookAt(transform.position + gameCamera.transform.rotation * Vector3.back,gameCamera.transform.rotation * Vector3.up);

    }
}
