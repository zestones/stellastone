using System.Collections.Generic;
using UnityEngine;

public class Mission
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string LaunchDate { get; set; }
	public string Destination { get; set; }
	public double DestinationDistance { get; set; }
	
	private List<Astronaut> astronauts = new List<Astronaut>();
	public List<Astronaut> Astronauts { get; set; }
	
	private List<Texture2D> images = new List<Texture2D>();
	public List<Texture2D> Images { get; set; }


	public Mission(string name, string description, string launchDate, string destination, double destinationDistance, List<Astronaut> astronauts, List<Texture2D> images)
	{	
		Name = name;
		Description = description;
		LaunchDate = launchDate;
		Destination = destination;
		DestinationDistance = destinationDistance;
		Astronauts = astronauts;
		Images = images;
	}
}
