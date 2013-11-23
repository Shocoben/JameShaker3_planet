using UnityEngine;
using System.Collections;

public class RotateAroundSun : MonoBehaviour {

	public float speed = 20f;
	public Vector3 axis = Vector3.up;
	public bool circular = false;
	public float ellipseRX = 4;
	public float elliseRY = 7;
	public float angle = 0;
	
	// Use this for initialization
	void Start () {
		_sun = Sun.Instance().transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (circular) {
	 		//transform.RotateAround(Vector3.zero, axis, speed * Time.deltaTime);
 			transform.RotateAround(_sun.position, axis, speed * Time.deltaTime);
		} else {
			angle += Time.deltaTime*speed;
			if (angle>360) {
				angle -= 360;
			}
			transform.position = new Vector3(
				_sun.position.x+ellipseRX*Mathf.Cos(Mathf.Deg2Rad*angle),
				_sun.position.y,
				_sun.position.z+elliseRY*Mathf.Sin(Mathf.Deg2Rad*angle));
		}
	}

	private Transform _sun;
	
}
