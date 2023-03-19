using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skybox1; // Le premier skybox à utiliser
    public Material skybox2; // Le deuxième skybox à utiliser
    private bool isSkybox1Active = true; // Indique si le premier skybox est actif

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSkybox1Active = !isSkybox1Active; // Inverser l'état du skybox actif
            if (isSkybox1Active)
            {
                RenderSettings.skybox = skybox1; // Activer le premier skybox
            }
            else
            {
                RenderSettings.skybox = skybox2; // Activer le deuxième skybox
            }
        }
    }
}
