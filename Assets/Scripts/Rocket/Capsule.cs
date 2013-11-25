using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

	public float rate = 0.5f;
	public float lastGeneration = 0;
	
	private Planet attachedPlanet = null;
	public int peoplePerGeneration = 1;
	
	public GameObject explosionFX;
	
	public AudioClip FXimminent;
	private Launcher launch;
	public static int count = 0;

    public float urgenceLaunchStrength = 2;

    private bool _died = false;

    public bool isDead()
    {
        return _died;
    }
	public void resetGeneration()
	{
		lastGeneration = Time.time;
		
	}
	
	private Animation _anim;
	private static int countInstance = 0;
	public int ID =0;
    private float _startPitch = 0;

	void Start()
	{
		count++;
		launch = GetComponent<Launcher>();
		ID = countInstance;
		countInstance++;
		_anim = GetComponent<Animation>();
		_anim["jumpingPeople"].speed = rate;
        _startPitch = audio.pitch;
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
		if (!_isDestroying && attachedPlanet != null && attachedPlanet.canDestroyRocket() && Input.GetMouseButtonDown(0) && mouseTouchMe() )
		{
			startDestroy();
		}
		
		if (_isDestroying && Input.GetMouseButtonUp(0))
		{
            _isDestroying = false;
            stopDestroy();
		}
		
		if (_isDestroying && !mouseTouchMe())
		{
			_currentDestroyTime = _destroyTime;
			stopDestroy();
		}
		
		if (_isDestroying && mouseTouchMe())
		{
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
        audio.pitch = _startPitch;
		audio.Stop ();
		_anim["scaling"].time = 0;
		_anim.Sample();
		_anim["scaling"].enabled = false;
        
        
	}
	
	public void playSoundImminent()
	{
		audio.PlayOneShot(FXimminent);
	}
	
	public void changePitchImmiment()
	{
		audio.pitch += 0.1f;
	}
	
	public void destroy()
	{
        if (_died)
            return;
        _died = true;

		GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation) as GameObject;
		explosion.transform.parent = attachedPlanet.transform;
		GameObject.Destroy(this.gameObject);
		if (Camera.main.GetComponent<ShakePosition>()!=null) {
			Camera.main.GetComponent<ShakePosition>().Shake(0.5f);
		}
		count--;
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
	
	
   
	public void detachFromPlanet()
	{
		if (attachedPlanet != null)
		{
			attachedPlanet.detachRocket(this);
		}
		attachedPlanet = null;
		transform.parent = null;
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
