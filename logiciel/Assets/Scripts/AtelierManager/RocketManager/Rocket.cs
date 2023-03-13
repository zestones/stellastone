using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Power { get; set; }
    public GameObject Model { get; set; }

    public Rocket(string name, string description, int cost, int power, GameObject model)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Power = power;
        Model = model;
    }
}
