using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
	
	public float maxStrength = 5f;
	public float maxTime = 2f;
	public bool timed = true;
	
	// Use this for initialization
	void Start () {
		_lineRender = GetComponent<LineRenderer>();
		_lineRender.enabled = false;
		_engine = GetComponent<Engine>();
		_engine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_touched) {			
			if (Input.GetMouseButtonUp(0)) {
				activeEngine(_strenght);
			} else {
				if (timed) {
					float t = Mathf.Clamp(Time.time-_timeOrigine,0,maxTime);
					_strenght = (t/maxTime)*maxStrength;
				} else {
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
		_touched = false;
		transform.parent = null;
		_engine.enabled = true;
		_engine.speed = _strenght;	
	}
	
	void OnMouseDown() {
		_lineRender.enabled = true;
		_touched = true;
		_strenghtOrigine = Input.mousePosition.y;
		_timeOrigine = Time.time;
	}
	
	LineRenderer _lineRender;
	Engine _engine;
	float _strenght = 1;
	bool _touched = false;
	float _strenghtOrigine;
	float _timeOrigine;
}
