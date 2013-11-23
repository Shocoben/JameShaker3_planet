using UnityEngine;
using System.Collections;

public class SnapObject : MonoBehaviour {
	
	public Transform oToSnap;	
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = oToSnap.transform.position + offset;
	}
}
