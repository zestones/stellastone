using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MoonViewController : MonoBehaviour
{
	public GameObject spectator;
	public GameObject cameraObject;
	
	public void OnSpectarorClick ()
	{
		spectator.SetActive(true);
		cameraObject.SetActive(false);
	}
	
	public void OnCameraClick ()
	{
		spectator.SetActive(false);
		cameraObject.SetActive(true);
	}
}