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
    public Text usernameText;
    public GameObject home;
    public GameObject parametres;
    public InputField emailInput;
    public InputField usernameInput;
    public InputField descriptionInput;

    public RawImage avatarImage; // Référence à l'élément d'interface graphique où afficher l'avatar

    void Start()
    {   
        home.SetActive(true);
        parametres.SetActive(false);
        float delay = 3.0f;
        Invoke("InitUser", delay);
    }
    void InitUser()
    {   usernameText.text=User.GetUsername();
        emailInput.text = User.GetEmail();
        descriptionInput.text = User.GetDescription();
        usernameInput.text = User.GetUsername();
        StartCoroutine(LoadLocalImage(User.GetAvatarUrl()));
    }
    public void redirectParametres(){
        home.SetActive(false);
        parametres.SetActive(true);
    }
    
    public void updateUserInfos(){
        if (usernameInput.text != User.GetUsername()){
            UpdateUsername(usernameInput.text);
        }
        if (descriptionInput.text != User.GetDescription()){
            UpdateDescription(descriptionInput.text);
        }
    }

    public void UpdateUsername(string newUsername)
    {
        User.UpdateUsername(newUsername);
    }

    public void UpdateDescription(string newDescription)
    {
        User.UpdateDescription(newDescription);
    }

    public void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private IEnumerator LoadLocalImage(string filePath)
    {
        // Charge l'image sélectionnée depuis le fichier local
        var fileContent = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileContent);

        // Vérifie que l'objet RawImage est bien initialisé
        if (avatarImage != null)
        {
            // Affiche l'image dans l'UI
            avatarImage.texture = texture;
            avatarImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("RawImage not found");
        }
        yield return null;
    }
}