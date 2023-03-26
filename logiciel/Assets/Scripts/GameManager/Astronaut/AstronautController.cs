using UnityEngine;

public class AstronautController : MonoBehaviour
{
	private float moveSpeed = 0.1f;
	private float jumpForce = 2.5f;
	
	public float lookSensitivity = 2f;
	public bool isGrounded;

	private Rigidbody rb;
	private float xRotation = 0f;

	private const float GRAVITY = -1.62f; // gravité de la lune en m/s^2
	private const float maxGroundAngle = 45f; // maximum angle of the ground for the astronaut to be considered grounded
		
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.AddForce(Vector3.up * Mathf.Abs(GRAVITY), ForceMode.Acceleration); // ajouter une force vers le haut pour compenser la gravité de la lune
	}

	private void Update()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(-horizontalInput, 0f, -verticalInput).normalized * moveSpeed * Time.deltaTime; // inverted horizontalInput and verticalInput
    	transform.Translate(movement);

		float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
		float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensitivity;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.Rotate(Vector3.up * mouseX);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(Vector3.up * Mathf.Sqrt(Mathf.Abs(GRAVITY)) * jumpForce, ForceMode.VelocityChange); // ajouter une force de saut en fonction de la gravité de la lune
			isGrounded = false;
		}
	}
	
	private void SetGroundedTrue(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			
			// Add a Physics Material to the Ground with friction set to 0
			PhysicMaterial pm = new PhysicMaterial();
			pm.dynamicFriction = 0f;
			pm.staticFriction = 0f;
			collision.gameObject.GetComponent<Collider>().material = pm;
			
			Vector3 normal = collision.contacts[0].normal;
			float angle = Vector3.Angle(Vector3.up, normal);

			if (angle <= maxGroundAngle)
			{
				isGrounded = true;

				// Apply moon's gravity instead of Earth's gravity
				float moonGravity = 1.62f;
				Physics.gravity = -normal * moonGravity;
			}
		}
	}
	
	private void OnCollisionEnter(Collision collision) { SetGroundedTrue(collision); }
}