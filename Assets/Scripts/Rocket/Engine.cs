using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	
	public float speed = 0.1f;
	public string planetTag;
	public string sunTag;
	public GameObject trailFX;
	public GameObject rocketMesh;
	
	// Use this for initialization
	void Start () {
		GameObject[] planetGOs = GameObject.FindGameObjectsWithTag(planetTag);
		_planets = new Planet[planetGOs.Length];
		int i = 0;
		foreach (GameObject p in planetGOs) {
			_planets[i++] = p.GetComponent<Planet>();
		}
		
		GameObject[] sunGOs = GameObject.FindGameObjectsWithTag(sunTag);
		_suns = new Planet[sunGOs.Length];
		i = 0;
		foreach (GameObject p in sunGOs) {
			_suns[i++] = p.GetComponent<Planet>();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Planet p in _planets) {
			if (p == null)
				continue;
			float d = Vector3.Distance(transform.position,p.gameObject.transform.position);
			if (d<p.UpdateDistanceEffect()) {
	 	 		Vector3 relativePos = p.gameObject.transform.position - transform.position;
        		Quaternion rotation = Quaternion.LookRotation(relativePos);
        		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime*0.5f);				
			}
		}
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		/*foreach (Planet p in _suns) {
			if (p == null)
				continue;
			float d = Vector3.Distance(transform.position,p.gameObject.transform.position);
			if (d<p.UpdateDistanceEffect()) {
	 	 		/*Vector3 relativePos = transform.position - p.gameObject.transform.position;
        		Quaternion rotation = Quaternion.LookRotation(relativePos);
        		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime*0.5f);*/			
				//transform.Translate(-Vector3.right * speed * Time.deltaTime * 10);
			/*}
		}*/
		
		transform.position = new Vector3(transform.position.x,0,transform.position.z);
	}
	
	void OnEnable()	
	{
		trailFX.SetActive(true);
		rocketMesh.renderer.material.color = Color.white;
	}
	void OnDisable()	
	{
		trailFX.GetComponent<ParticleSystem>().Clear();
		trailFX.SetActive(false);
	}
	
	private Planet[] _planets;
	private Planet[] _suns;
}
