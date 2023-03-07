using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using PlayFab.ClientModels;
using PlayFab;
using UnityEditor;
public class AvatarManager : MonoBehaviour
{  
    public RawImage avatarImageParametres; // Référence à l'élément d'interface graphique où afficher l'avatar
    public RawImage avatarImageHome; // Référence à l'élément d'interface graphique où afficher l'avatar
    public static string localImagePath;
    void Start(){
        StartCoroutine(LoadLocalImage(User.GetAvatarUrl()));
    }
    public void OnBrowseButtonClick()
    {
        // Ouvre la boîte de dialogue de sélection de fichiers
        string filePath = UnityEditor.EditorUtility.OpenFilePanel("Select image file", "", "png,jpg,jpeg");

        if (!string.IsNullOrEmpty(filePath))
        {
            // Stocke le chemin d'accès local du fichier sélectionné
            localImagePath = filePath;
            OnUploadButtonClick();
            // Affiche l'image sélectionnée
            StartCoroutine(LoadLocalImage(filePath));
        }
    }

    private IEnumerator LoadLocalImage(string filePath)
    {
        // Charge l'image sélectionnée depuis le fichier local
        var fileContent = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileContent);

        // Vérifie que l'objet RawImage est bien initialisé
        if (avatarImageParametres != null && avatarImageHome != null)
        {
            // Affiche l'image dans l'UI
            avatarImageParametres.texture = texture; avatarImageHome.texture = texture;
            avatarImageParametres.gameObject.SetActive(true); avatarImageHome.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("RawImage not found");
        }

        yield return null;
    }

    public void OnUploadButtonClick()
    {
        if (string.IsNullOrEmpty(localImagePath))
        {
            Debug.Log("No file selected");
            return;
        }

        // Envoie l'image sélectionnée à PlayFab
        PlayFabClientAPI.UpdateAvatarUrl(new UpdateAvatarUrlRequest
        {
            ImageUrl = localImagePath
        }, OnUpdateAvatarUrlSuccess, OnUpdateAvatarUrlFailure);
    }

    private void OnUpdateAvatarUrlSuccess(EmptyResponse response)
    {   
        Debug.Log("Avatar URL updated successfully");   
    }   

    private void OnUpdateAvatarUrlFailure(PlayFabError error)
    {
        Debug.LogError("Failed to update avatar URL: " + error.ErrorMessage);
    }

    
}
