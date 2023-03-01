using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using PlayFab.ClientModels;
using PlayFab;

public class AvatarManager : MonoBehaviour
{  
    public RawImage avatarImage; // Référence à l'élément d'interface graphique où afficher l'avatar
 
    private static string localImagePath; // Chemin d'accès local du fichier image sélectionné


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
