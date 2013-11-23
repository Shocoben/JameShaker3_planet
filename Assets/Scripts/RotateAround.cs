using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{
	public float speed = 10;
	public Transform rotateAroundThis;
	public bool right;
	public bool angry;
	public Material calmColor;
	public Material angryColor;
	
	private bool alreadyAngry = false;

	// Use this for initialization
	void Start () 
	{
//		GetComponent<TrailRenderer>().material = calmColor;
		
		if (right)
			transform.position = new Vector3( rotateAroundThis.position.x, rotateAroundThis.position.y + 1f, rotateAroundThis.position.z);	
		else
			transform.position = new Vector3( rotateAroundThis.position.x, rotateAroundThis.position.y - 1f, rotateAroundThis.position.z);
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround( rotateAroundThis.position, Vector3.forward, speed * Time.deltaTime);
		
		if (angry && !alreadyAngry)
		{
			alreadyAngry = true;
			GetComponent<TrailRenderer>().material = angryColor;
		}
		
		if (!angry && alreadyAngry)
		{
			alreadyAngry = false;
			GetComponent<TrailRenderer>().material = calmColor;
		}
		
	}
}
