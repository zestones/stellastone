using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class AstronautController : MonoBehaviour
{
	private float moveSpeed = 0.1f;
	private float jumpForce =1f;
	
	private float sensitivity = 75.0f;
	public float Sensitivity { get { return sensitivity; } set { sensitivity = value; } }
	
	public float lookSensitivity = 2f;
	public bool isGrounded;

	private Rigidbody rb;
	private float xRotation = 0f;

	private const float GRAVITY = -1.62f; // gravité de la lune en m/s^2
	private const float maxGroundAngle = 150f; // maximum angle of the ground for the astronaut to be considered grounded
	
	private bool isShiftPressed = false;
	
	private const string HOME_NAME_SCENE = "HomeScene"; 
	
	private GameObject clickedObject;
	
	private Button homeButton;
	private Button resumeButton;
	private Button quitButton;

	private Animator anim;
		
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.AddForce(Vector3.up * Mathf.Abs(GRAVITY), ForceMode.Acceleration); // ajouter une force vers le haut pour compenser la gravité de la lune
		
		Cursor.visible = false;
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float verticalInput = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed * Time.deltaTime; // inverted horizontalInput and verticalInput
		transform.Translate(movement);

		float mouseX = Input.GetAxisRaw("Mouse X") * lookSensitivity;
		float mouseY = Input.GetAxisRaw("Mouse Y") * lookSensitivity;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		
		if (!isShiftPressed) transform.Rotate(Vector3.up * mouseX);
		else 
		{
			// Fix the camera position and enbale click on object
			if (Input.GetMouseButtonDown(0)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit)) {
					clickedObject = hit.transform.gameObject;
					ShowModal();
				}
			}
			
		}

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
	
	private void OnCollisionEnter(Collision collision) { 
		SetGroundedTrue(collision); 
	}
			
	void FixedUpdate() 
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			isShiftPressed = true;
			Cursor.visible = true;
		} else {
			isShiftPressed = false;
			Cursor.visible = false;
		}
		
		if (Input.GetKey(KeyCode.UpArrow)) anim.SetBool("isMoving", true);
		else anim.SetBool("isMoving", false);
	}
	
	// Show the details modal of the clicked object
	private void ShowModal()
	{
		GameObject modalObject = GameObject.Find("Dashboard UI/Canvas/ObjectModalDetails");
		GameObjectModal modal = modalObject.GetComponent<GameObjectModal>();

		if (clickedObject != null) modal.ShowModal(clickedObject);
	}
	
}