using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

	public float rate = 0.5f;
	public float lastGeneration = 0;
	
	private Planet attachedPlanet = null;
	public int peoplePerGeneration = 1;
	public void resetGeneration()
	{
		lastGeneration = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (attachedPlanet != null && lastGeneration + rate < Time.time)
		{
			attachedPlanet.addPeople(peoplePerGeneration);
			resetGeneration();
		}
	}
	
	public void setAttachedPlanet(Planet planet)
	{
		attachedPlanet = planet;
		attachedPlanet.attachRocket(this);
	}
}
