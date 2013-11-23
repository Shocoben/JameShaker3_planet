using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
	
	public float maxStrength = 5f;
	public float maxTime = 2f;
	public bool timed = true;
	
	
	public GameObject letterPrefab;
	private KeyCode _letter;
	private SnapObject letterSnaped;
	public bool controlsWhenFree = false;
	public GameObject fbx;
	
	private Capsule _capsule;
	
	// Use this for initialization
	void Start () 
	{
		_lineRender = GetComponent<LineRenderer>();
		_lineRender.enabled = false;
		_engine = GetComponent<Engine>();
		_engine.enabled = false;
		_capsule = GetComponent<Capsule>();
	
	}
	public bool showNumber = false;
	public void setLetter(string name, KeyCode keycode)
	{
		if (showNumber )
		{
			if (letterSnaped == null)
			{
				GameObject letterGO = GameObject.Instantiate(letterPrefab, Vector3.zero, letterPrefab.transform.rotation) as GameObject;
				letterSnaped = letterGO.GetComponent<SnapObject>();
				letterSnaped.oToSnap = this.transform;
			}
			TextMesh letterMesh = letterSnaped.GetComponent<TextMesh>();
			if (keycode != null && letterMesh != null)
				letterMesh.text = name;
		}
		_letter = keycode;
	}
	
	public void setColor(Color color)
	{
		fbx.renderer.material.SetColor("_Color", color);
	}
	
	public void setTexture(Texture texture)
	{
		fbx.renderer.material.SetTexture("_MainTex", texture);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!_engine.enabled || controlsWhenFree)
		{
			if (Input.GetKeyUp(_letter)) 
			{
				activeEngine(_strenght);
				
				
			} 
			else if (Input.GetKeyDown(_letter))
			{
				_lineRender.enabled = true;
				_lineRender.SetPosition(0,transform.position);
				_lineRender.SetPosition(1,transform.position );
				_strenghtOrigine = Input.mousePosition.y;
				_timeOrigine = Time.time;
			}
			else if (Input.GetKey(_letter))
			{
				if (timed) 
				{
					float t = Mathf.Clamp(Time.time-_timeOrigine,0,maxTime);
					_strenght = (t/maxTime)*maxStrength;
				} 
				else 
				{
					_strenght = Mathf.Clamp((Input.mousePosition.y-_strenghtOrigine)*0.01f,0,maxStrength);
				}
				_lineRender.SetPosition(0,transform.position);
				_lineRender.SetPosition(1,transform.position+transform.forward*_strenght);
			}
		}
	}
	
	public void activeEngine(float strenght)
	{
		_lineRender.enabled = false;
		transform.parent = null;
		_engine.enabled = true;
		_engine.speed = _strenght;
		_capsule.stopDestroy();
		_capsule.detachFromPlanet();
	}
	
	LineRenderer _lineRender;
	Engine _engine;
	float _strenght = 1;
	float _strenghtOrigine;
	float _timeOrigine;

}
