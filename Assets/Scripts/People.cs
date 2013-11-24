using UnityEngine;
using System.Collections;

public class People : MonoBehaviour {

	private Planet _target = null;
	public float minSpeed = 2;
	public float maxSpeed = 3;
	
	private float _speed = 2;
	public string planetTag = "Planet";
	public void Awake()
	{
		_speed = Random.Range(minSpeed, maxSpeed);
	}
	
	public void setTarget(Planet target)
	{
		_target = target;
	}
	
	public void FixedUpdate()
	{
		if (_target != null)
		{
			transform.LookAt(_target.transform.position);
		}
		transform.Translate(Vector3.forward * _speed * Time.deltaTime);
	}
	
	public string sunTag = "Sun";
	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == planetTag || other.tag == sunTag)
		{
			Planet o = other.GetComponent<Planet>();
			Debug.Log(o.id);
			if (o != null && o.id == _target.id)
			{
				o.addPeople(1);
				GameObject.Destroy(this.gameObject);
			}
		}
	}
	
	
}
