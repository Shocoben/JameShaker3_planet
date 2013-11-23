using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManagers : MonoBehaviour {
	
	public GameObject rocketPrefab;
	public int rocketCount = 5;
	public string planetTag = "Planet";
	
	// Use this for initialization
	void Start () {
		GameObject[] planets = GameObject.FindGameObjectsWithTag(planetTag);
		
		HashSet<int> selectedPlanets = new HashSet<int>();
		
		for (int i = 0; i < rocketCount; ++i)
		{
			int rand = 0;
			while(true)
			{
				
				rand = Random.Range(0, planets.Length-1);
				if (selectedPlanets.Add(Mathf.FloorToInt(rand)))
				{
					break;
				}
			}
			
			GameObject planet = planets[rand];
			
			Vector3 pos = planet.transform.position;
			pos.x += planet.transform.localScale.x * 0.7f;
			GameObject rocket = GameObject.Instantiate(rocketPrefab, pos, rocketPrefab.transform.rotation) as GameObject;
			//rocket.transform.parent = planet.transform;
			rocket.GetComponent<Capsule>().setAttachedPlanet(planet.GetComponent<Planet>());
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
