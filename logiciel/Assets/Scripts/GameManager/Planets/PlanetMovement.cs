using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public Transform sun;
    public float rotationSpeed = 10.0f;

    private Vector3 initialPosition;
    private float distance;

    void Start()
    {
        initialPosition = transform.position - sun.position;
        distance = initialPosition.magnitude;
    }

    void Update()
    {
        float x = sun.position.x + distance * Mathf.Cos(Time.time * speed);
        float y = sun.position.y;
        float z = sun.position.z + distance * Mathf.Sin(Time.time * speed);

        transform.position = new Vector3(x, y, z);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
