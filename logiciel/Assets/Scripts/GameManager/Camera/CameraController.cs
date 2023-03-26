using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CameraController : MonoBehaviour
{
	private float sensitivity = 75.0f;
	public float Sensitivity { get { return sensitivity; } set { sensitivity = value; } }
	
	public float maxYaw = 90.0f;
	public float minYaw = -90.0f;
	public float maxPitch = 60.0f;
	public float minPitch = -60.0f;
	
	private Vector2 mousePos;
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private bool isShiftPressed = false;
	
	private const string HOME_NAME_SCENE = "HomeScene"; 
	
	private GameObject clickedObject;
	private GameObject parametre;
	
	private Button homeButton;
	private Button resumeButton;
	private Button quitButton;
	
	void Start()
	{
		Cursor.visible = false;
		parametre = GameObject.Find("Dashboard UI/Canvas/Parametre");
		
		// Retrieve the buttons from the UI
		Button[] buttons = parametre.GetComponentsInChildren<Button>();
		homeButton = buttons.FirstOrDefault(x => x.name == "HomeScene");
		resumeButton = buttons.FirstOrDefault(x => x.name == "Reprendre");
		quitButton = buttons.FirstOrDefault(x => x.name == "Quitter");

		// Add listeners to the buttons
		homeButton.onClick.AddListener(OnHomeButtonClick);
		resumeButton.onClick.AddListener(OnResumeButtonClick);
		quitButton.onClick.AddListener(OnQuitButtonClick);
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
		
		// Mettre en pause le jeu lorsque la touche Escape est enfoncée
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.visible = true;
			if (Time.timeScale == 1.0f)
			{
				Time.timeScale = 0.0f;
				if (parametre != null) parametre.SetActive(true);
			}
			else resumeGame();
		}
	}
	
	private void resumeGame() 
	{
		Time.timeScale = 1.0f;
		parametre.SetActive(false);
		Cursor.visible = false;
	}
	
	// Public method to update the mouse sensitivity
	public void SetSensitivity(float newSensitivity) { sensitivity = newSensitivity; }
		
	// Redirect to the home scene
	void OnHomeButtonClick() { SceneManager.LoadScene(HOME_NAME_SCENE); }
	
	// Resume the game
	void OnResumeButtonClick() { resumeGame(); }

	// Quit the game
	void OnQuitButtonClick() { Application.Quit(); }
	
	// Show the details modal of the clicked object
	private void ShowModal()
	{
		GameObject modalObject = GameObject.Find("Dashboard UI/Canvas/ObjectModalDetails");
		GameObjectModal modal = modalObject.GetComponent<GameObjectModal>();

		if (clickedObject != null) modal.ShowModal(clickedObject);
	}

	void LateUpdate() { transform.LookAt(transform.position + transform.forward, Vector3.up); }

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