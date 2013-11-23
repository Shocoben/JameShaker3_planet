using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	
	public float speed = 0.1f;
	public string planetTag;
	
	// Use this for initialization
	void Start () {
		GameObject[] planetGOs = GameObject.FindGameObjectsWithTag(planetTag);
		_planets = new Planet[planetGOs.Length];
		int i = 0;
		foreach (GameObject p in planetGOs) {
			_planets[i++] = p.GetComponent<Planet>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Planet p in _planets) {
			float d = Vector3.Distance(transform.position,p.gameObject.transform.position);
			if (d<p.distanceEffect) {
	 	 		Vector3 relativePos = p.gameObject.transform.position - transform.position;
        		Quaternion rotation = Quaternion.LookRotation(relativePos);
        		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime*0.5f);				
			}
		}
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
	
	private Planet[] _planets;
}
