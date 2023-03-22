using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInitializer
{
    public List<Mission> missionList = new List<Mission>();

    public void Init()
    {
        Mission apollo_11 = new Mission();
        apollo_11.Name = "Apollo 11";
        apollo_11.Description = "First crewed mission to land on the Moon";
        apollo_11.LaunchDate = "July 16, 1969";
        apollo_11.Destination = "Moon";
        apollo_11.DestinationDistance = 384400;
        apollo_11.Astronauts = new List<Astronaut> {
            new Astronaut("1", "Neil Armstrong", "USA", 38, 82, 1.8f,Resources.Load<Sprite>("Images/armstrong")),
            new Astronaut("2", "Buzz Aldrin", "USA", 39, 77, 1.80f,Resources.Load<Sprite>("Images/aldrin")),
            new Astronaut("3", "Michael Collins", "USA", 38, 84, 1.85f,Resources.Load<Sprite>("Images/collins"))
        };
        apollo_11.Images = new List<Texture2D>();
        apollo_11.Images.Add(Resources.Load<Texture2D>("Images/Missions/apollo11.jpg")); 
        apollo_11.Images.Add(Resources.Load<Texture2D>("Images/Missions/apollo11_2.jpg")); 
        missionList.Add(apollo_11);
    }

}