using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class HomeManager : MonoBehaviour
{
    // Constantes
    private const string ATELIER_SCENE_NAME = "Atelier";

    // Références aux objets de la scène
    private GameObject homeObject;
    private GameObject parametresObject;
    private Text usernameText;
    private InputField emailInput;
    private InputField descriptionInput;
    private InputField usernameInput; 

    private void Start()
    {
        ShowHome();
        UpdateUserInfo();
    }

    // Navigation
    public void ShowHome()
    {
        homeObject.SetActive(true);
        parametresObject.SetActive(false);
    }

    public void ShowParametres()
    {
        homeObject.SetActive(false);
        parametresObject.SetActive(true);
    }

    public void LoadAtelierScene()
    {
        SceneManager.LoadScene(ATELIER_SCENE_NAME);
    }

    // Mise à jour des informations de l'utilisateur
    public void UpdateUserInfo()
    {
        usernameText.text = User.GetUsername();
        emailInput.text = User.GetEmail();
        descriptionInput.text = User.GetDescription();
        usernameInput.text = User.GetUsername();
    }

    public void UpdateUsername(string newUsername)
    {
        User.UpdateUsername(newUsername);
    }

    public void UpdateDescription(string newDescription)
    {
        User.UpdateDescription(newDescription);
    }

    // Gestion des erreurs
    public void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
