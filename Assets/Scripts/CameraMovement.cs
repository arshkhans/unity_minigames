using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX; // Left - Right

        xRotation -= mouseY; // Up - down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Not more then 90 degrees

        // Rotate camera & Orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0); // Camera Rotation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); // Orientation
    }
}
