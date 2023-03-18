using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;
    public float maxYaw = 90.0f;
    public float minYaw = -90.0f;
    public float maxPitch = 60.0f;
    public float minPitch = -60.0f;

    private Vector2 mousePos;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mousePos.x = Input.GetAxis("Mouse X");
        mousePos.y = Input.GetAxis("Mouse Y");

        yaw += mousePos.x * sensitivity * Time.deltaTime;
        pitch -= mousePos.y * sensitivity * Time.deltaTime;

        // Limiter l'angle de pitch
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Limiter l'angle de yaw
        if (yaw > maxYaw) {
            yaw -= 360.0f;
        } else if (yaw < minYaw) {
            yaw += 360.0f;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + transform.forward, Vector3.up);
    }
}