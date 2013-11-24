﻿using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

	public float rate = 0.5f;
	public float lastGeneration = 0;
	
	private Planet attachedPlanet = null;
	public int peoplePerGeneration = 1;
	
	public GameObject explosionFX;
	

	
	private Launcher launch;

	public void resetGeneration()
	{
		lastGeneration = Time.time;
		
	}
	
	private Animation _anim;
	private static int countInstance = 0;
	public int ID =0;
	void Start()
	{
		launch = GetComponent<Launcher>();
		ID = countInstance;
		countInstance++;
		_anim = GetComponent<Animation>();
		_anim["jumpingPeople"].speed = rate;
	}
	
	public float rayDistance = 100;
	public LayerMask rayMask;
	bool mouseTouchMe()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green, 1.5f);
		if (Physics.Raycast(ray, out hit, rayDistance, rayMask.value))
		{
			if (hit.collider.gameObject.GetComponent<Capsule>().ID == ID)
				return true;
			return false;
		}
		return false;
	}
	
	// Update is called once per frame
	private bool _isDestroying = false;
	public float _destroyTime = 1;
	private float _currentDestroyTime = 1;
	
	void Update ()
	{
		
		/*if ( attachedPlanet != null && lastGeneration + rate < Time.time )
		{
			attachedPlanet.addPeople(peoplePerGeneration);
			resetGeneration();
		}*/
		
		if (!_isDestroying && attachedPlanet != null && attachedPlanet.canDestroyRocket() && Input.GetMouseButtonDown(0) && mouseTouchMe() )
		{
			startDestroy();
		}
		
		if (_isDestroying && Input.GetMouseButtonUp(0))
		{
			_isDestroying = false;
		}
		
		if (_isDestroying && !mouseTouchMe())
		{
			_currentDestroyTime = _destroyTime;
			stopDestroy();
		}
		
		if (_isDestroying && mouseTouchMe())
		{
			startDestroy();
			_currentDestroyTime -= Time.deltaTime;
		}
		
		if (_currentDestroyTime <= 0)
		{
			_currentDestroyTime = _destroyTime;
			destroy();
		}
	}
	
	public void onPeopleLand()
	{
		if (attachedPlanet == null)
			return;
		
		attachedPlanet.addPeople(peoplePerGeneration);
	}
	
	
	
	public void startDestroy()
	{
		_isDestroying = true;
		_anim["scaling"].enabled = true;
		_anim.Play("scaling");
		
	}
	
	public void stopDestroy()
	{
		_anim["scaling"].time = 0;
		_anim.Sample();
		_anim["scaling"].enabled = false;
		
	}
	
	public void destroy()
	{
		GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation) as GameObject;
		explosion.transform.parent = attachedPlanet.transform;
		GameObject.Destroy(this.gameObject);
	}
	
	public void setAttachedPlanet(Planet planet)
	{
		
		attachedPlanet = planet;
		attachedPlanet.attachRocket(this);
		if (planet != null && _anim != null)
		{
			people.SetActive(true);
			_anim.Play("jumpingPeople");
		}
	}
	
	public float urgenceLaunchStrength = 2;
	public void detachFromPlanet()
	{
		if (attachedPlanet != null)
		{
			attachedPlanet.detachRocket(this);
		}
		attachedPlanet = null;
		stopAnimation();
		
	}
	
	public void startUrgenceEngine()
	{
		launch.activeEngine(urgenceLaunchStrength);	
	}
	
	public GameObject people;
	
	public void stopAnimation()
	{
		_anim.Stop();
		people.SetActive(false);
	}
	

}
