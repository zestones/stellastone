using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
	private CameraController cameraController;
	public Slider slider;

	private void Start()
	{
		// get the slider component
		slider = GetComponentInChildren<Slider>();
		
		// get the camera controller
		cameraController = Camera.main.GetComponent<CameraController>();
		
		// set the slider value to the camera controller sensitivity
		slider.value = cameraController.Sensitivity;
	}

	// called when the slider value is changed
	public void OnSliderValueChanged() { cameraController.SetSensitivity(slider.value); }
}
