using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public float mouseSensitivity = 100f;

    public Transform cam;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse to the center of the screen
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // "-=" since we don't want to inverse the y-axis.
        xRotation = Mathf.Clamp(xRotation, -40f, 20f); // limit the camera to not flip over 180 degrees.
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotate the camera on the x-axis when moving the mouse in y-axis direction
        transform.Rotate(Vector3.up * mouseX); //rotate the player on the y-axis when moving the mous in x-axis direction
    }
}
