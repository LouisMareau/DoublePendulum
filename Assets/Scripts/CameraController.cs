using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private new Camera camera;

    public float minZoom = 25f;
    public float maxZoom = 65f;
    private float zoom;
    [Space(10)]
    public float initialZoom = 40f;
    public float zoomIncrement = 1f;

    private void Awake() 
    {
        camera = Camera.main;
        camera.orthographicSize = zoom = initialZoom;
    }

    private void Update() 
    {
        float startzoom = zoom;

        // The camera is inverted so we have to invert the values
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoom < maxZoom) { zoom += zoomIncrement; }
            else { zoom = maxZoom; }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoom > minZoom) { zoom -= zoomIncrement; }
            else { zoom = minZoom; }
        }

        float smoothedZoom = Mathf.Lerp(startzoom, zoom, Time.deltaTime);

        camera.orthographicSize = smoothedZoom;
    }
}