using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{

    public float minZoom;
    public float maxZoom;
    new private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (ScrollWheel > 0.0f) {
            // Forwards
            camera.fieldOfView = maxZoom;
        }
        else if (ScrollWheel < 0.0f) {
            //Backwards
            camera.fieldOfView = minZoom;
        }
    }
}
