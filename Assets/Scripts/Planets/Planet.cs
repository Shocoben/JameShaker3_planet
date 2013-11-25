using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {
	
    //Physic
	public float distanceEffect = 2;
	public float baseMassFactor = 10;
	

	public GameObject explosionFX;

    //People stuff
	private int _peopleCount;
	public int peopleStart = 1;
	public GameObject peoplePrefab;
    private List<People> peopleComingToMe = new List<People>();

    //Objectives
	private float minSize = 1;
	public float maxPeople = 5;
	public float maxRadius = 1.5f;
    public int minimPeopleToExplodeRocket = 1;
    
    //atmosphere
	public Atmosphere atmosphere;
	
	//Graphic changes
	public GameObject graphicFBX;
    public Color selectColor = Color.white;
    public Color unselectColor = Color.black;
    public float outlineWidth = 5;

    //Identification
    public static int aliveCount = 0;
    public static int instanceCount = 0;
    public static Planet selected = null;
    private int _ID = 0;

    //rocket stuff
    private List<Capsule> _attachedRockets = new List<Capsule>();

    //People transfer stuff
    public int nbrPeopleSentPerRate;
    public float sendRate = 1;
    private float lastSent;

    public int getID()
    {
        return _ID;
    }

	void Start()
	{
		aliveCount++;
		atmosphere = GetComponentInChildren<Atmosphere>();
		
		minSize = transform.localScale.x;
		_ID = instanceCount;
		instanceCount ++;
		_peopleCount = peopleStart;

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
			if (hit.collider.gameObject.GetComponent<Planet>()._ID == _ID)
				return true;
			return false;
		}
		return false;
	}
	
	
	public bool canDestroyRocket()
	{
		return _peopleCount >= minimPeopleToExplodeRocket;
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
				{
					
					onSelect();
				}
			}
		}
		else if ( Input.GetMouseButton(0) )
		{
			if (mouseTouchMe() && selected != null && selected._ID != _ID)
			{
				selected.sendPeopleTo(this);	
			}
		}
		else if ( Input.GetMouseButtonUp(0) && selected != null )
		{
			selected.onDeselect();
			selected = null;
		}
		
	}
	
	
	
	public void onDeselect()
	{		
		graphicFBX.renderer.material.SetColor("_OutlineColor", unselectColor);
		graphicFBX.renderer.material.SetFloat("_Outline", 0);
	}
	

	public void onSelect()
	{
        selected = this;
		graphicFBX.renderer.material.SetColor("_OutlineColor", selectColor);
		graphicFBX.renderer.material.SetFloat("_Outline", outlineWidth);
	}
	

	
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

		int nbrPeople = (_peopleCount < nbrPeopleSentPerRate)? _peopleCount :  nbrPeopleSentPerRate;
		for (int i = 0; i < nbrPeople; ++i)
		{
			Vector3 direction = objectiv.transform.position - transform.position;
			direction.Normalize();
			
			GameObject newPeople = GameObject.Instantiate(peoplePrefab, transform.position + direction * transform.localScale.x * 0.5f , peoplePrefab.transform.rotation) as GameObject;
			People nPol = newPeople.GetComponent<People>();
			nPol.setTarget(objectiv);
		}
		
		addPeople(-nbrPeople);
		resetLastSent();
	}

	
	public void addPeople(int nbr)
	{
		_peopleCount += nbr;
		if (_peopleCount < 0)
		{
			_peopleCount = 0;	
		}
		
		if (_peopleCount > maxPeople)
		{
			onMaxPeople();
		}
		
		resizeWithPeople();
	}
	
	public void resizeWithPeople()
	{
		float size = minSize + (maxRadius - minSize) * _peopleCount / maxPeople;
		transform.localScale = new Vector3(size, size, size);
	}
	
	private bool _died = false;
	public void onMaxPeople()
	{
		if (_died) {
			return;
		}
        _died = true;

        //detach people
		for (int i = 0; i < peopleComingToMe.Count; ++i)
		{
			peopleComingToMe[i].setTarget(null);
		}
		peopleComingToMe.Clear();
		
        //Detatch rockets
		Capsule[] rocketsToDetach = new Capsule[_attachedRockets.Count];
		_attachedRockets.CopyTo(rocketsToDetach);
		for (int j = 0; j < rocketsToDetach.Length; ++j)
		{
            if (rocketsToDetach[j] == null ||rocketsToDetach[j].isDead())
                continue;
			rocketsToDetach[j].startUrgenceEngine();
		}
		_attachedRockets.Clear();

        //instantiate explosion
		Instantiate(explosionFX, transform.position, explosionFX.transform.rotation );
		if (Camera.main.GetComponent<ShakePosition>()!=null) 
        {
			Camera.main.GetComponent<ShakePosition>().Shake();
		}
		GameObject.Destroy(this.gameObject);
		aliveCount--;
	}
	
	public float getMassFactor()
	{
		return baseMassFactor * (1 + _peopleCount);
	}
	
	
	
	void OnTriggerEnter(Collider other)
	{
		if (tag=="Sun") {
			return;
		}
		
		if (other.gameObject.GetComponent<Engine>()!=null && other.gameObject.GetComponent<Engine>().enabled == true) 
		{
			other.gameObject.GetComponent<Engine>().enabled = false;
			other.gameObject.GetComponent<Capsule>().setAttachedPlanet(this);
			
			
			RaycastHit hit = new RaycastHit();
			Vector3 dir = transform.position-other.gameObject.transform.position;
			dir.Normalize();
			
			other.gameObject.transform.rotation = Quaternion.LookRotation(-dir);
			other.gameObject.transform.position = transform.position - (dir * transform.localScale.x * .5f) - (dir * other.gameObject.transform.localScale.x*0.25f);
			other.gameObject.transform.parent = transform;
			
		}
	}
	
	public void attachRocket(Capsule rocket)
	{
		_attachedRockets.Add(rocket);	
	}
	
	public void detachRocket(Capsule rocket)
	{
		if (_attachedRockets.Contains(rocket))
			_attachedRockets.Remove(rocket);
	}
	
}
