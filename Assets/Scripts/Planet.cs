using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	
	public float distanceEffect = 2;
	public float baseMassFactor = 10;
	
	
	public TextMesh guiText;
	
	public KeyCode letter;
	public int people = 0;
	
	public static Planet selected = null;
	public static int instanceCount = 0;
	
	public GameObject peoplePrefab;
	
	public int id = 0;
	
	private float minSize = 1;
	public float maxPeople = 5;
	
	private List<People> peopleComingToMe = new List<People>();
	
	void Start()
	{
		minSize = transform.localScale.x;
		id = instanceCount;
		instanceCount ++;
		if (letter != null && guiText != null)
			guiText.text = letter.ToString();
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
					selected.addPeople(-1);
					selected.sendPeopleTo(this);
				
					selected = null;
				}
			}
		}
	}
	
	public void sendPeopleTo(Planet objectiv)
	{
		Debug.Log(objectiv.letter.ToString());
		Vector3 direction = objectiv.transform.position - transform.position;
		direction.Normalize();
		
		GameObject newPeople = GameObject.Instantiate(peoplePrefab, transform.position + direction * transform.localScale.x, Quaternion.identity) as GameObject;
		People nPol = newPeople.GetComponent<People>();
		nPol.setTarget(objectiv);
	}
	
	public void addPeople(int nbr)
	{
		people += nbr;
		if (people < 0)
		{
			people = 0;	
		}
		if (people > maxPeople)
		{
			onMaxPeople();	
		}
		float size = minSize + people;
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
		return baseMassFactor * (1 + people);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Engine>()!=null &&
			other.gameObject.GetComponent<Engine>().enabled) {
			other.gameObject.GetComponent<Engine>().enabled = false;
			other.gameObject.transform.parent = transform;
			other.gameObject.transform.RotateAround(other.gameObject.transform.position,Vector3.up,180);
		}
	}

}
