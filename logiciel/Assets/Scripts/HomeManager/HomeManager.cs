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
    public static string playerId;
    public string username; 
    string email;
    string description;
    public static string avatarUrl;

    public RawImage avatarImage; // Référence à l'élément d'interface graphique où afficher l'avatar

    void Start()
    {   usernameText.text=" ";
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnError);
        home.SetActive(true);
        parametres.SetActive(false);
    }

    void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {   playerId=result.AccountInfo.PlayFabId;
        //For the welcoming text
        username = result.AccountInfo.Username;
        email = result.AccountInfo.PrivateInfo.Email;
        GetDescription();

        usernameText.text = "Bonjour " + username;
        //For the parameters interface
        emailInput.text = email;
        usernameInput.text = username;
        return;
    }

    public void redirectParametres(){
        home.SetActive(false);
        parametres.SetActive(true);
    }
    
    public void updateUserInfos(){
        if (emailInput.text != email){
            updateEmail(emailInput.text);
        }
        if (usernameInput.text != username){
            updateUsername(usernameInput.text);
        }
        if (descriptionInput.text != description){
            UpdateDescription(descriptionInput.text);
        }
    }
    public void updateEmail(string newEmail)
    {
        var request = new UpdateUserDataRequest { Data = new System.Collections.Generic.Dictionary<string, string>() { { "Email", newEmail } } };
        PlayFabClientAPI.UpdateUserData(request, OnUpdateEmailSuccess, OnUpdateEmailError);
    }

    private void OnUpdateEmailSuccess(UpdateUserDataResult result)
    {
        Debug.Log("L'adresse e-mail de connexion de l'utilisateur a été mise à jour avec succès!");
    }

    private void OnUpdateEmailError(PlayFabError error)
    {
        Debug.LogError("Erreur lors de la mise à jour de l'adresse e-mail de connexion de l'utilisateur : " + error.ErrorMessage);
    }

    public void updateUsername(string newUsername)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = newUsername };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateSuccess, OnUpdateError);
    }


    public void OnUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Mise à jour réussie");
    }

    public void OnUpdateError(PlayFabError error)
    {
        Debug.LogError("Erreur lors de la mise à jour " + error.ErrorMessage);
    }

    public void GetDescription()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = new List<string>() { "description" }
        }, OnGetUserDataSuccess, OnGetUserDataFailure);

        GetAvatarUrl(playerId);
    }

    private void OnGetUserDataSuccess(GetUserDataResult result)
    {
        if (result.Data.TryGetValue("description", out PlayFab.ClientModels.UserDataRecord descriptionOut))
        {
            description = descriptionOut.Value;
            descriptionInput.text = description;
            Debug.Log("Description: " + description);
        }
        else
        {   description = " ";
            Debug.Log("Description not found.");
        }
    }

    private void OnGetUserDataFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get user data: " + error.ErrorMessage);
    }

    public void UpdateDescription(string newDescription)
    {
        // Met à jour les données utilisateur avec une nouvelle description
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "description", newDescription }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnUpdateDescriptionSuccess, OnUpdateDescriptionFailure);
    }

    private void OnUpdateDescriptionSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Description updated successfully");
    }

    private void OnUpdateDescriptionFailure(PlayFabError error)
    {
        Debug.LogError("Failed to update description: " + error.ErrorMessage);
    }

    public void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


    void GetAvatarUrl(string playFabId)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowAvatarUrl = true
            }
        }, result =>
        {
            if (result.PlayerProfile != null && !string.IsNullOrEmpty(result.PlayerProfile.AvatarUrl))
            {
                avatarUrl = result.PlayerProfile.AvatarUrl;
                Debug.Log("On a l'avatar URL !" + avatarUrl);
                StartCoroutine(LoadLocalImage(avatarUrl));
                // Utiliser l'URL de l'avatar pour charger l'image correspondante dans votre interface utilisateur
            }
            else
            {
               avatarUrl = " ";
            }
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }


    private IEnumerator LoadLocalImage(string filePath)
    {
        Debug.Log("Je suis dans LoadLocal de  HomeManager: filepath= " + filePath);
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