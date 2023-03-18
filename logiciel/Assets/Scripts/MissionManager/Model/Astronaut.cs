using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Astronaut
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public Texture2D Image { get; set; }
    public Astronaut(string id, string name, string nationality, int age, double weight, double height, Texture2D image)
    {
        Id = id;
        Name = name;
        Nationality = nationality;
        Age = age;
        Weight = weight;
        Height = height;
        Image = image;
    }
}
