using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DisplayUserRocket : MonoBehaviour {
    public List<GameObject> rocketModels;
    private const string ATELIER_SCENE_NAME = "AtelierScene";
    public float rotationSpeed = 150f; // Vitesse de rotation de la fusée
    void Start() {
        GameObject newUserInterface = GameObject.Find("NewUserInterface");

        if (User.Rocket != null){
            newUserInterface.SetActive(false);
            int id = User.Rocket.Id;
            rocketModels[id].SetActive(true);
        }
        else {
            newUserInterface.SetActive(true);
            Button newMissionButton = GameObject.Find("NewMissionButton").GetComponent<Button>();
            newMissionButton.onClick.AddListener(OnNewMissionClick);
        }
    }

    void Update(){
        if(User.Rocket != null){
        int id = User.Rocket.Id;
        rocketModels[id].transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
    void OnNewMissionClick()
    {
        SceneManager.LoadSceneAsync(ATELIER_SCENE_NAME);
    }
}
