public class Mission
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LaunchDate { get; set; }
    public double PayloadMass { get; set; }
    public double DestinationDistance { get; set; }

    public Mission(string name, string description, string launchDate, double payloadMass, double destinationDistance)
    {
        Name = name;
        Description = description;
        LaunchDate = launchDate;
        PayloadMass = payloadMass;
        DestinationDistance = destinationDistance;
    }
}