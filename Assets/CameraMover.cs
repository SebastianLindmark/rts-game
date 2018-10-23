using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    private float screenWidth;
    private float screenHeight;

    public const int boundary = 15; //pixels
    public const float scrollSpeed = 1f;
    public const float zoomSpeed = 10f;

    public const float zoomMin = 20f;
    public const float zoomMax = 100f;

    void Start () {

        this.screenWidth = Screen.width;
        this.screenHeight = Screen.height;
    }
    
    void Update () {
        Vector3 positionDelta = new Vector3(0, 0, 0);

        if (!Input.GetKey(KeyCode.LeftControl)) {
            return;
        }


        if (Input.mousePosition.x < boundary)
        {
            positionDelta.x = scrollSpeed;
        }
        else if (Input.mousePosition.x > screenWidth - boundary) {
            positionDelta.x = -scrollSpeed;
        }


        if (Input.mousePosition.y < boundary)
        {
            positionDelta.z = scrollSpeed;
        }
        else if (Input.mousePosition.y > screenHeight- boundary)
        {
            positionDelta.z = -scrollSpeed;
        }


        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            positionDelta.y = -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }


        Vector3 newPosition = transform.position + positionDelta;

        if (newPosition.y > zoomMax)
        {
            newPosition.y = zoomMax;
        }
        else if (newPosition.y < zoomMin) {
            newPosition.y = zoomMin;
        }

        transform.position = newPosition;
    }
}
