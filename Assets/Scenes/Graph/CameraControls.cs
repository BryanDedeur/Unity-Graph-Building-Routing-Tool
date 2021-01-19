using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    Vector3 dragOrigin;
    float minZoom = 2;
    float maxZoom = 20;
    float zoomSpeed = 2;
    bool panning = false;

    private void Update()
    {
        // When LMB clicked get mouse click position and set panning to true
        if (Input.GetKeyDown(KeyCode.Mouse0) && !panning)
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }
        // If LMB is already clicked, move the camera following the mouse position update
        if (panning)
        {
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;
            transform.position += new Vector3(-distance.x, -distance.y, 0);
        }

        // If LMB is released, stop moving the camera
        if (Input.GetKeyUp(KeyCode.Mouse0))
            panning = false;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > minZoom)
        {
            Camera.main.orthographicSize -= zoomSpeed;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < maxZoom)
        {
            Camera.main.orthographicSize += zoomSpeed;
        }

    }


}
