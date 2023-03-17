using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Power { get; set; }
    public GameObject Model { get; set; }
    
    public Image UnselectedBackground { get; set; }

    public Image SelectedBackground { get; set; }

    public Image Image { get; set; }
    public Rocket(string name, string description, int cost, int power, GameObject model, GameObject component)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Power = power;
        Model = model;
        Initialize(component);
    }
    private void Initialize(GameObject component)
    {
        Transform unselectedBackgroundTransform = component.transform.Find("UnselectedBackground");
        UnselectedBackground = unselectedBackgroundTransform.GetComponent<Image>();

        Transform selectedBackgroundTransform = component.transform.Find("SelectedBackground");
        SelectedBackground = selectedBackgroundTransform.GetComponent<Image>();

        Transform ImageTransform = component.transform.Find("Image");
        Image = ImageTransform.GetComponent<Image>();
    }
}
