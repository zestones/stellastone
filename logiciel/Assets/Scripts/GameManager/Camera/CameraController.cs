using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;

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

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + transform.forward, Vector3.up);
    }
}