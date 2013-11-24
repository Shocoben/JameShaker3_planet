using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	
	public float distanceEffect = 2;
	public float baseMassFactor = 10;
	
	
	private TextMesh letterText;
	private TextMesh percentText;
	public KeyCode letter;
	
	public GameObject explosionFX;
	
	
	public static Planet selected = null;
	public static int instanceCount = 0;
	public int id = 0;

	private int _people;
	public int peopleStart = 1;
	public GameObject peoplePrefab;
	
	private float minSize = 1;
	public float maxPeople = 5;
	public float maxRadius = 1.5f;
	
	private List<People> peopleComingToMe = new List<People>();
	
	private float _percentPeople = 0;
	public float peopleSelectSpeed = 3;
	
	public int minimPeopleToExplodeRocket = 1;
	
	public GameObject pLetterPrefab;
	public GameObject pPercentPrefab;
	
	public Atmosphere atmosphere;
	
	public float sizeFactor = 0.2f;
	public bool showLetter = false;
	void Start()
	{
		atmosphere = GetComponentInChildren<Atmosphere>();
		
		minSize = transform.localScale.x;
		id = instanceCount;
		instanceCount ++;
		_people = peopleStart;
		
		if (showLetter)
		{
			GameObject pLetter = GameObject.Instantiate(pLetterPrefab, Vector3.zero, pLetterPrefab.transform.rotation) as GameObject;
			pLetter.GetComponent<SnapObject>().oToSnap = this.transform;
			letterText = pLetter.GetComponent<TextMesh>();
			if (letter != null && letterText != null)
				letterText.text = letter.ToString();
			
			GameObject pPercent = GameObject.Instantiate(pPercentPrefab, Vector3.zero, pPercentPrefab.transform.rotation) as GameObject;
			pPercent.GetComponent<SnapObject>().oToSnap = this.transform;
			percentText = pPercent.GetComponent<TextMesh>();
			
	
			
			if (pPercent != null)
			{
				pPercent.SetActive(false);	
			}
		}
		
		

		resizeWithPeople();
	}
	
	public void addPeopleComingToMe(People people)
	{
		peopleComingToMe.Add(people);
	}
	
	public float UpdateDistanceEffect()
	{
		return transform.localScale.x * distanceEffect / minSize;
	}
	
	public float rayDistance = 100;
	public LayerMask rayMask;
	protected bool mouseTouchMe()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green, 1.5f);
		if (Physics.Raycast(ray, out hit, rayDistance, rayMask.value))
		{
			if (hit.collider.gameObject.GetComponent<Planet>().id == id)
				return true;
			return false;
		}
		return false;
	}
	
	
	public bool canDestroyRocket()
	{
		return _people >= minimPeopleToExplodeRocket;
	}
	
	public virtual void Update()
	{
		if (atmosphere) {
			atmosphere.SetAngry(canDestroyRocket());
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if (mouseTouchMe())
			{
				if (selected == null)
					selected = this;
			}
		}
		else if ( Input.GetMouseButton(0) )
		{
			if (mouseTouchMe() && selected != null && selected.id != id)
			{
				selected.sendPeopleTo(this);	
			}
		}
		else if ( Input.GetMouseButtonUp(0) && selected != null )
		{
			selected.onDeselect();
			selected = null;
		}
		/*
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
					selected.sendPeopleTo(this);
					selected.onDeselect();	
					selected = null;
				}
			}
		}
		
		
		if (selected != null && selected.id == id)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_percentPeople = 0;
				percentText.gameObject.SetActive(true);
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
				percentText.gameObject.SetActive(false);
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
		*/
	}
	
	public void onDeselect()
	{
		if (percentText)
			percentText.gameObject.SetActive(false);
		_percentPeople = 0;
	}
	
	public int nbrPeopleSentPerRate;
	public float sendRate = 1;
	private float lastSent;
	
	public void resetLastSent()
	{
		lastSent = Time.time;	
	}
	
	public void sendPeopleTo(Planet objectiv)
	{
		if (lastSent + sendRate > Time.time)
		{
			return;
		}
		//int nbrPeople = Mathf.FloorToInt(_people * (_percentPeople / 100));
		int nbrPeople = (_people < nbrPeopleSentPerRate)? _people :  nbrPeopleSentPerRate;
		for (int i = 0; i < nbrPeople; ++i)
		{
			Vector3 direction = objectiv.transform.position - transform.position;
			direction.Normalize();
			
			GameObject newPeople = GameObject.Instantiate(peoplePrefab, transform.position + direction * transform.localScale.x * 0.5f , peoplePrefab.transform.rotation) as GameObject;
			People nPol = newPeople.GetComponent<People>();
			nPol.setTarget(objectiv);
		}
		
		_percentPeople = 0;
		addPeople(-nbrPeople);
		resetLastSent();
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
		
		resizeWithPeople();
	}
	
	public void resizeWithPeople()
	{
		float size = minSize + (maxRadius - minSize) * _people / maxPeople;
		transform.localScale = new Vector3(size, size, size);
	}
	
	public void onMaxPeople()
	{
		for (int i = 0; i < peopleComingToMe.Count; ++i)
		{
			peopleComingToMe[i].setTarget(null);
		}
		peopleComingToMe.Clear();
		
		Capsule[] rocketsToDetach = new Capsule[attachedRockets.Count];
		attachedRockets.CopyTo(rocketsToDetach);
		for (int j = 0; j < rocketsToDetach.Length; ++j)
		{
			rocketsToDetach[j].startUrgenceEngine();
		}
		attachedRockets.Clear();
		Instantiate(explosionFX, transform.position, Quaternion.Euler(Vector3.right * 90) );
		GameObject.Destroy(this.gameObject);
	}
	
	public float getMassFactor()
	{
		return baseMassFactor * (1 + _people);
	}
	
	public List<Capsule> attachedRockets = new List<Capsule>();
	
	void OnTriggerEnter(Collider other)
	{
		if (tag=="Sun") {
			return;
		}
		
		if (other.gameObject.GetComponent<Engine>()!=null &&
			other.gameObject.GetComponent<Engine>().enabled) {
			other.gameObject.GetComponent<Engine>().enabled = false;
			other.gameObject.GetComponent<Capsule>().setAttachedPlanet(this);
			
			
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(
				other.gameObject.transform.position,
				transform.position-other.gameObject.transform.position,
				out hit)) 
			{
				other.gameObject.transform.rotation = Quaternion.LookRotation(hit.normal);
				Vector3 dir = (other.gameObject.transform.position-transform.position);
				dir.Normalize();
				other.gameObject.transform.position = transform.position + (dir * transform.localScale.x * .5f) + (dir * other.gameObject.transform.localScale.x*0.25f);
			}
			other.gameObject.transform.parent = transform;
			
		}
	}
	
	public void attachRocket(Capsule rocket)
	{
		attachedRockets.Add(rocket);	
	}
	
	public void detachRocket(Capsule rocket)
	{
		if (attachedRockets.Contains(rocket))
			attachedRockets.Remove(rocket);
	}
	
}
