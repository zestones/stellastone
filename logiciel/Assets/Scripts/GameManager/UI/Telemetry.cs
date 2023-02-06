using UnityEngine;
using UnityEngine.UI;

public class Telemetry : MonoBehaviour
{
    public float speed;
    public float altitude;

    public Text speedText;
    public Text altitudeText;

    private void Update()
    {
        speedText.text = string.Format("{0:N2}", speed);
        altitudeText.text = string.Format("{0:N2}", altitude);
    }
}
