using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Rigidbody rocket;
    public float launchForce = 100f;
    public float fuel = 1000f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            rocket.AddRelativeForce(Vector3.up * launchForce, ForceMode.Acceleration);
            fuel -= Time.deltaTime;
        }
        else if(fuel <= 0)
        {
            rocket.AddForce(Physics.gravity * rocket.mass);
        }
    }
}

