using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionInitializer
{
    public List<Mission> missionList = new List<Mission>();

   public void Init()
    {   
        
        List<Astronaut> parkerAstronauts = new List<Astronaut>();
        List<Texture2D> parkerImages = new List<Texture2D>();
        parkerImages.Add(Resources.Load<Texture2D>("Images/Missions/parker_1")); 
        parkerImages.Add(Resources.Load<Texture2D>("Images/Missions/parker_2")); 
        Mission parkerMission = new Mission("Delta IV Parker Solar Probe", "Mission d'étude du soleil de plus près que jamais auparavant", "12 août 2018", "Soleil", 149600000, parkerAstronauts, parkerImages);
        missionList.Add(parkerMission);

        List<Astronaut> arabSatAstronauts = new List<Astronaut>();
        List<Texture2D> arabSatImages = new List<Texture2D>();
        arabSatImages.Add(Resources.Load<Texture2D>("Images/Missions/arabsat_1")); 
        arabSatImages.Add(Resources.Load<Texture2D>("Images/Missions/arabsat_2")); 
        Mission arabSatMission = new Mission("Arabsat-6A", "Lancement du premier satellite de communication géostationnaire pour Arabsat", "11 avril 2019", "Orbite de transfert géostationnaire", 36000, arabSatAstronauts, arabSatImages);
        missionList.Add(arabSatMission);

    
        List<Astronaut> apolloAstronauts = new List<Astronaut> {
            new Astronaut("1", "Neil Armstrong", "USA", 38, 82, 1.8f,Resources.Load<Sprite>("Images/armstrong")),
            new Astronaut("2", "Buzz Aldrin", "USA", 39, 77, 1.80f,Resources.Load<Sprite>("Images/aldrin")),
            new Astronaut("3", "Michael Collins", "USA", 38, 84, 1.85f,Resources.Load<Sprite>("Images/collins"))
        };
        List<Texture2D> apolloImages = new List<Texture2D>();
        apolloImages.Add(Resources.Load<Texture2D>("Images/Missions/apollo11_1")); 
        apolloImages.Add(Resources.Load<Texture2D>("Images/Missions/apollo11_2")); 
        Mission apollo_11 = new Mission("Apollo 11", "Première mission en équipage à atterrir sur la Lune", "16 juillet 1969", "Lune", 384400, apolloAstronauts, apolloImages);
        missionList.Add(apollo_11);
    }

}