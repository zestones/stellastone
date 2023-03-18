using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket
{
	public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Power { get; set; }
    public int Velocity { get; set; }
    public int FuelCapacity { get; set; }
    public int PayloadMass { get; set; }
	public float Thrust { get; set; } 
    public float Height { get; set; }
    public float Diameter { get; set; }
    public string Country { get; set; }

    public GameObject Model { get; set; }

    public Image UnselectedBackground { get; set; }
    public Image SelectedBackground { get; set; }

    public Image Image { get; set; }

    public Rocket(int id, string name)
    {
        Id = id;
        Name = name;
    }
	
	public void Initialize(GameObject component)
	{
		Transform unselectedBackgroundTransform = component.transform.Find("UnselectedBackground");
		UnselectedBackground = unselectedBackgroundTransform.GetComponent<Image>();

		Transform selectedBackgroundTransform = component.transform.Find("SelectedBackground");
		SelectedBackground = selectedBackgroundTransform.GetComponent<Image>();

		Transform ImageTransform = component.transform.Find("Image");
		Image = ImageTransform.GetComponent<Image>();
	}
}
