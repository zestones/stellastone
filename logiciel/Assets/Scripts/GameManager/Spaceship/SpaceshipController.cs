using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public Rigidbody spaceshipRigidbody;
    public float accelerationForce;
    public float rotationSpeed;
    private float mouseX;
    private float mouseY;

    void Update()
    {
        // Accelerate the spaceship when the spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            spaceshipRigidbody.AddForce(transform.up * accelerationForce);
        }

        // Get the mouse x and y values
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Clamp the mouse y value to prevent the spaceship from flipping
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Apply the rotation to the spaceship
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0f);
    }
}
