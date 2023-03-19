using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public GameObject moon;
    public GameObject rocket;
    public float speed = 5.0f;

    void Update()
    {
        Vector3 target = moon.transform.position;
        rocket.transform.LookAt(target);
        rocket.transform.position = Vector3.MoveTowards(rocket.transform.position, target, speed * Time.deltaTime);
    }
}
