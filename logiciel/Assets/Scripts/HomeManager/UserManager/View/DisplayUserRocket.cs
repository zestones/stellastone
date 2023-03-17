using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayUserRocket : MonoBehaviour {
    public List<GameObject> rocketModels;

    void Start() {
        Debug.Log(User.Rocket.Name);
        int id = User.Rocket.Id;
        rocketModels[id].SetActive(true);
    }
}
