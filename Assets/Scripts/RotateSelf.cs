using UnityEngine;
using System.Collections;

public class RotateSelf : MonoBehaviour {

	public float speed = 20f;
	public Vector3 axis = Vector3.up;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
 		transform.RotateAround(transform.position, axis, speed * Time.deltaTime);
	}
}
