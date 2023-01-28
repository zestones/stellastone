using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int newSceneBuildIndex;

    void Update()
    {
        if (transform.position.y >= 1000)
        {
            SceneManager.LoadScene(newSceneBuildIndex);
        }
    }
}

