using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RocketInitializer
{
	public List<Rocket> rocketList = new List<Rocket>();
	
	public void Init(List<GameObject> rocketModels, List<GameObject> rocketsComponents) 
	{
		Rocket saturn_V = new Rocket(0, "Saturn V");
		
		saturn_V.Description = "A powerful rocket used in the Apollo missions";
		saturn_V.Cost = 1000000;
		saturn_V.Power = 100;
		saturn_V.Velocity = 36362;
		saturn_V.Fuel_capacity = 543251;
		
		saturn_V.Model = rocketModels[saturn_V.Id];
		Debug.Log(saturn_V.Id);
		
		saturn_V.Initialize(rocketsComponents[saturn_V.Id]);
		rocketList.Add(saturn_V);

		Rocket falcon_heavy = new Rocket(1, "Falcon Heavy");
		falcon_heavy.Description = "A reusable rocket developed by SpaceX";
		falcon_heavy.Cost = 1000000;
		falcon_heavy.Power = 100;
		falcon_heavy.Velocity = 36362;
		falcon_heavy.Fuel_capacity = 543251;
		
		falcon_heavy.Model = rocketModels[falcon_heavy.Id];
		falcon_heavy.Initialize(rocketsComponents[falcon_heavy.Id]);
		rocketList.Add(falcon_heavy);
		
		Rocket delta_IV = new Rocket(2, "Delta IV");
		delta_IV.Description = "A medium-to-heavy rocket operated by the United Launch Alliance";
		delta_IV.Cost = 1000000;
		delta_IV.Power = 100;
		delta_IV.Velocity = 36362;
		delta_IV.Fuel_capacity = 543251;
		
		delta_IV.Model = rocketModels[delta_IV.Id];
		delta_IV.Initialize(rocketsComponents[delta_IV.Id]);
		rocketList.Add(delta_IV);
	}	
}