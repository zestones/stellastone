using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	private bool isShiftPressed = false;
	
	private GameObject clickedObject;

	void Start()
	{
		Cursor.visible = false;
	}

	void Update()
	{
		mousePos.x = Input.GetAxis("Mouse X");
		mousePos.y = Input.GetAxis("Mouse Y");

		if (!isShiftPressed) {
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
		else 
		{
			// Fix the camera position and enbale click on object
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit)) {
					clickedObject = hit.transform.gameObject;
					ShowModal();
				}
			}
		}
	}
	
	private void ShowModal()
	{
		GameObject modalObject = GameObject.Find("Dashboard UI/Canvas/ObjectModalDetails");
		GameObjectModal modal = modalObject.GetComponent<GameObjectModal>();

		if (clickedObject != null) modal.ShowModal(clickedObject);
	}

	void LateUpdate()
	{
		transform.LookAt(transform.position + transform.forward, Vector3.up);
	}

	void FixedUpdate() {
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			isShiftPressed = true;
			Cursor.visible = true;
		} else {
			isShiftPressed = false;
			Cursor.visible = false;
		}
	}
}