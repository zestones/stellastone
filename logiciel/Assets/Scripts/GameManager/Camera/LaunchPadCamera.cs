using UnityEngine;
using UnityEngine.UI;

public class LaunchPadCamera : MonoBehaviour
{
    public GameObject attachedObject;
    public float rotationSpeed = 20.0f;
    public float distance = 5.0f;

    private Vector3 targetPosition;
    private float rotationCount = 0.0f;
    
    public float numberRotation = 0.5f;
    public GameObject mainCamera;

    public GameObject WelcomeText;
    public Text Status;

    void Start()
    {   Text MissionName = WelcomeText.transform.Find("MissionName").GetComponent<Text>();
        // Définir la position cible de la caméra à la position de l'objet attaché
        if (attachedObject != null) {
            targetPosition = attachedObject.transform.position;
        }
        MissionName.text = User.Rocket.mission.Name;
    }

    void Update()
    {
        // Normaliser la direction entre la caméra et l'objet
        Vector3 direction = (transform.position - attachedObject.transform.position).normalized;

        // Définir la position de la caméra en utilisant la direction normalisée et la distance spécifiée
        transform.position = attachedObject.transform.position + direction * distance;

        // Faire pivoter la caméra autour de la position cible
        transform.RotateAround(targetPosition, Vector3.up, rotationSpeed * Time.deltaTime);

        // Incrémenter le compteur de rotation
        rotationCount += Time.deltaTime * rotationSpeed / 360.0f;
        Status.text = "Embarquement en cours...";
        // Vérifier si la caméra a effectué 1,5 tours et la désactiver si c'est le cas
        if (rotationCount >= numberRotation) {
            gameObject.SetActive(false);
            mainCamera.SetActive(true);
            WelcomeText.SetActive(false);
            Status.text = "Prêt pour le décollage... Appuyez sur espace.";
        }

        // Faire en sorte que la caméra pointe toujours vers l'objet attaché à la scène
        if (attachedObject != null) {
            transform.LookAt(attachedObject.transform.position);
        }
    }
}
