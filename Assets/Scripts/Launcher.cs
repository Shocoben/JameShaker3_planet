using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
	
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
				_lineRender.enabled = false;
				_touched = false;
				transform.parent = null;
				_engine.enabled = true;
				_engine.speed = _strenght;
			} else {
				_strenght = Mathf.Clamp((Input.mousePosition.y-_strenghtOrigine)*0.01f,0,10);
				_lineRender.SetPosition(0,transform.position);
				_lineRender.SetPosition(1,transform.position+transform.forward*_strenght);
			}
		}
	}
	
	void OnMouseDown() {
		_lineRender.enabled = true;
		_touched = true;
		_strenghtOrigine = Input.mousePosition.y;
	}
	
	LineRenderer _lineRender;
	Engine _engine;
	float _strenght = 1;
	bool _touched = false;
	float _strenghtOrigine;
}
