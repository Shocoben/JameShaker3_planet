using UnityEngine;
using System.Collections;

public class SnapObject : MonoBehaviour {
	
	public Transform oToSnap;	
	public Vector3 offset;
	
	// Update is called once per frame
	void Update () 
	{
		if (oToSnap != null)
			transform.position = oToSnap.transform.position + offset;
		else
			GameObject.Destroy(this.gameObject);
	}
}
