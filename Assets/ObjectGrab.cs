using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public static bool isGrabbed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Cube2")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (isGrabbed == false)
                    {
                        isGrabbed = true;
                    }
                    OnMouseDown();
                }
                if (Input.GetMouseButton(0))
                {
                    if (isGrabbed == false)
                     {
                        isGrabbed = true;
                     }
                    OnMouseDrag();
                }
            }
        }  

    }

    void OnMouseDrag()
    {
        // Get the distance from the camera to the object
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Convert the mouse position to world coordinates
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distance; // Set the z-coordinate to the distance from the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Move the object to the new world position
        transform.position = worldPosition;
    }

    void OnMouseDown()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position)));
    }
}
