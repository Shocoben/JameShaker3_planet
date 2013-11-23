using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	public float distanceEffect = 2;
	public float massFactor = 10;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Engine>()!=null &&
			other.gameObject.GetComponent<Engine>().enabled) {
			other.gameObject.GetComponent<Engine>().enabled = false;
			other.gameObject.transform.parent = transform;
		}
	}

}
