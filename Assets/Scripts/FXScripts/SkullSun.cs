using UnityEngine;
using System.Collections;

public class SkullSun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter()
	{
		if (Planet.selected != null)
		{
			renderer.enabled = true;
		}
	}
	
	void OnMouseExit()
	{
		renderer.enabled = false;
	}
}
