using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
	public GameObject rocketModel;
	public float launchAltitude = 1000f;
	public const string SPACE_SCENE_NAME = "SpaceScene";
	public string launchAnimationName = "rocketLaunch";

	private bool animationStarted = false;

	void Update()
	{
		if (rocketModel.transform.position.y > launchAltitude && !animationStarted)
		{
			animationStarted = true;
			LoadNextScene();
			// rocketModel.GetComponent<Animation>().Play(launchAnimationName);
			// Invoke("LoadNextScene", rocketModel.GetComponent<Animation>()[launchAnimationName].length);
		}
	}

	private void LoadNextScene()
	{
		SceneManager.LoadScene(SPACE_SCENE_NAME);
	}
}
