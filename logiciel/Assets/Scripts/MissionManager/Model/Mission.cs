using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LaunchDate { get; set; }
    public string Destination { get; set; }
    public double DestinationDistance { get; set; }
    public List<Astronaut> Astronauts { get; set; }
    public List<Texture2D> Images { get; set; }

    public Mission()
    {
        Astronauts = new List<Astronaut>();
        Images = new List<Texture2D>();
    }

    public Mission(string name, string description, string launchDate, string destination, double destinationDistance, List<Astronaut> astronauts)
    {
        Name = name;
        Description = description;
        LaunchDate = launchDate;
        Destination = destination;
        DestinationDistance = destinationDistance;
        Astronauts = astronauts;
        Images = new List<Texture2D>();
    }
}
