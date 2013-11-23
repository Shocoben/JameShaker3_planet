﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	
	public float distanceEffect = 2;
	public float baseMassFactor = 10;
	
	
	private TextMesh letterText;
	private TextMesh percentText;
	
	public KeyCode letter;
	private int _people;
	
	public int peopleStart = 1;
	
	public static Planet selected = null;
	public static int instanceCount = 0;
	
	public GameObject peoplePrefab;
	
	public int id = 0;
	
	private float minSize = 1;
	public float maxPeople = 5;
	
	private List<People> peopleComingToMe = new List<People>();
	
	private float _percentPeople = 0;
	public float peopleSelectSpeed = 3;
	
	public int minimPeopleToExplodeRocket = 1;
	
	public GameObject pLetterPrefab;
	public GameObject pPercentPrefab;
	
	
	void Start()
	{
		minSize = transform.localScale.x;
		id = instanceCount;
		instanceCount ++;
		_people = peopleStart;
		
		GameObject pLetter = GameObject.Instantiate(pLetterPrefab, Vector3.zero, pLetterPrefab.transform.rotation) as GameObject;
		pLetter.GetComponent<SnapObject>().oToSnap = this.transform;
		letterText = pLetter.GetComponent<TextMesh>();
		
		GameObject pPercent = GameObject.Instantiate(pPercentPrefab, Vector3.zero, pPercentPrefab.transform.rotation) as GameObject;
		pPercent.GetComponent<SnapObject>().oToSnap = this.transform;
		percentText = pPercent.GetComponent<TextMesh>();
		
		if (letter != null && letterText != null)
			letterText.text = letter.ToString();
		

	}
	
	public void addPeopleComingToMe(People people)
	{
		peopleComingToMe.Add(people);
	}
	
	
	
	void Update()
	{
		if (Input.GetKeyDown(letter))
		{
			if (selected == null)
			{
				selected = this;
				
			}
			else
			{
				if (selected.id == id)
				{
					selected = null; 
					return;
				}
				else
				{
					Debug.Log(this.letter);
					selected.sendPeopleTo(this);
					
					selected = null;
				}
			}
		}
		
		
		if (selected != null && selected.id == id)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_percentPeople = 0;
			}
			else if (Input.GetKey(KeyCode.Space))
			{
				if (_people > 1)
				{
					_percentPeople += peopleSelectSpeed * Time.deltaTime;
				}
				else
				{
					_percentPeople = 100;
				}
			}
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				_percentPeople = 0;	
			}
			
			if (_percentPeople > 100)
			{
				_percentPeople = 100;	
			}
			
		}
		
		if (percentText != null)
		{
			percentText.text = Mathf.Floor(_percentPeople) + "%";
		}
	}
	
	public void sendPeopleTo(Planet objectiv)
	{
		int nbrPeople = Mathf.FloorToInt(_people * (_percentPeople / 100));
		
		for (int i = 0; i < nbrPeople; ++i)
		{
			Vector3 direction = objectiv.transform.position - transform.position;
			direction.Normalize();
			
			GameObject newPeople = GameObject.Instantiate(peoplePrefab, transform.position + direction * transform.localScale.x , Quaternion.identity) as GameObject;
			People nPol = newPeople.GetComponent<People>();
			nPol.setTarget(objectiv);
		}
		Debug.Log(nbrPeople);
		
		_percentPeople = 0;
		addPeople(-nbrPeople);
	}

	
	public void addPeople(int nbr)
	{
		_people += nbr;
		if (_people < 0)
		{
			_people = 0;	
		}
		if (_people > maxPeople)
		{
			onMaxPeople();	
		}
		float size = _people;
		if (size < minSize)
			size = minSize;
		transform.localScale = new Vector3(size, size, size);
	}
	
	
	public void onMaxPeople()
	{
		for (int i = 0; i < peopleComingToMe.Count; ++i)
		{
			peopleComingToMe[i].setTarget(null);
		}
		peopleComingToMe.Clear();
	}
	
	public float getMassFactor()
	{
		return baseMassFactor * (1 + _people);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Engine>()!=null &&
			other.gameObject.GetComponent<Engine>().enabled) {
			other.gameObject.GetComponent<Engine>().enabled = false;
			other.gameObject.transform.parent = transform;
			
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(
				other.gameObject.transform.position,
				transform.position-other.gameObject.transform.position,
				out hit)) {
				other.gameObject.transform.rotation = Quaternion.LookRotation(hit.normal);
				Vector3 dir = (other.gameObject.transform.position-transform.position);
				dir.Normalize();
				other.gameObject.transform.position = transform.position + dir * transform.localScale.x*.5f + dir * other.gameObject.transform.localScale.x*.5f;
			}
			
		}
	}
	
}
