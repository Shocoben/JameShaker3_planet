using UnityEngine;
using System.Collections;

public class unscaleParent : MonoBehaviour {

	
	private Vector3 scaleStart;
	void Start()
	{
		if (transform.parent!=null) {
			scaleStart = transform.parent.localScale;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent != null)
			transform.localScale = new Vector3(scaleStart.x / transform.parent.localScale.x, scaleStart.y / transform.parent.localScale.y, scaleStart.z / transform.parent.localScale.z);
	}
}
