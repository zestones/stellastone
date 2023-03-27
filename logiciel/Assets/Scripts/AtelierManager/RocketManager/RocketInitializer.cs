using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class RocketInitializer
{
	public List<Rocket> rocketList = new List<Rocket>();
	public List<Mission> missionsList = new List<Mission>();
	public void Init(List<GameObject> rocketModels, List<GameObject> rocketsComponents) 
	{		
		MissionInitializer mi = new MissionInitializer();
		mi.Init();

		Rocket delta_IV = new Rocket(0, "Delta IV");
		delta_IV.Description = "Une fusée medium américaine développée par United Launch Alliance";
		delta_IV.Cost = 38000000;
		delta_IV.Power = 4;
		delta_IV.Velocity = 11000;
		delta_IV.FuelCapacity = 465500;
		delta_IV.FuelDensity = 0.07f;
		delta_IV.Height = 63.4f;
		delta_IV.Diameter = 5.1f;
		delta_IV.Country = "USA";
		delta_IV.PayloadMass = 13150;
		delta_IV.Thrust = 7.3f;
		delta_IV.MaxAcceleration = 5.0f;
		delta_IV.Model = rocketModels[delta_IV.Id];
		delta_IV.mission = mi.missionList[delta_IV.Id];
		delta_IV.Initialize(rocketsComponents[delta_IV.Id]);
		rocketList.Add(delta_IV);
		
		Rocket falcon_heavy = new Rocket(1, "Falcon Heavy");
		falcon_heavy.Description = "Une fusée réutilisable américaine développée par SpaceX";
		falcon_heavy.Cost = 90000000;
		falcon_heavy.Power = 140;
		falcon_heavy.Velocity = 9667;
		falcon_heavy.FuelCapacity = 202800;
		falcon_heavy.FuelDensity = 0.81f;
		falcon_heavy.MaxAcceleration = 5.1f;
		falcon_heavy.Height = 70f;
		falcon_heavy.Diameter = 12.2f;
		falcon_heavy.Country = "USA";
		falcon_heavy.PayloadMass = 63800;
		falcon_heavy.Thrust = 22.819f;
		falcon_heavy.Model = rocketModels[falcon_heavy.Id];
		falcon_heavy.Initialize(rocketsComponents[falcon_heavy.Id]);
		falcon_heavy.mission = mi.missionList[falcon_heavy.Id];
		rocketList.Add(falcon_heavy);
		
		Rocket saturn_V = new Rocket(2, "Saturn V");
		saturn_V.Description = "Une fusée américaine utilisée pour les missions Apollo";
		saturn_V.Cost = 185000000;
		saturn_V.Power = 34;
		saturn_V.Velocity = 11186;
		saturn_V.FuelCapacity = 1036000;
		saturn_V.FuelDensity = 0.81f;
		saturn_V.MaxAcceleration = 4.5f;
		saturn_V.Height = 110.6f;
		saturn_V.Diameter = 10.1f;
		saturn_V.Country = "USA";
		saturn_V.PayloadMass = 14000;
		saturn_V.Thrust= 35.10f;
		saturn_V.Model = rocketModels[saturn_V.Id];
		saturn_V.mission = mi.missionList[saturn_V.Id];
		saturn_V.Initialize(rocketsComponents[saturn_V.Id]);
		rocketList.Add(saturn_V);
	}	
}