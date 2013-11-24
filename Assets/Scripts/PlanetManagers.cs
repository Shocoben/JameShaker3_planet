using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TouchConfig
{
	public KeyCode keycode;
	public KeyCode padCode;
	public Texture texture;
	public Color color;
}

public class PlanetManagers : MonoBehaviour {
	
	public GameObject rocketPrefab;
	public TouchConfig[] rocketTouches;
	public string planetTag = "Planet";
	public Texture2D planetWin;
	public Texture2D rocketWin;
	
	public float ratioScale = 1.25f;
	// Use this for initialization
	void Start () {
		GameObject[] planets = GameObject.FindGameObjectsWithTag(planetTag);
		
		HashSet<int> selectedPlanets = new HashSet<int>();
		
		for (int i = 0; i < rocketTouches.Length; ++i)
		{
			int rand = 0;
			while(true)
			{
				
				rand = Random.Range(0, planets.Length-1);
				
				if (selectedPlanets.Add(Mathf.FloorToInt(rand)))
				{
					break;
				}
			}
			
			KeyCode cKeyCode = rocketTouches[i].keycode;
			
			GameObject planet = planets[rand];
			Vector3 pos = planet.transform.position;
			pos.x += planet.transform.localScale.x * ratioScale;
			
			GameObject rocket = GameObject.Instantiate(rocketPrefab, pos, rocketPrefab.transform.rotation) as GameObject;
			rocket.transform.parent = planet.transform;
			rocket.GetComponent<Capsule>().setAttachedPlanet(planet.GetComponent<Planet>());
			
			string keyName;
			switch(cKeyCode)
			{
				case KeyCode.Alpha0 :
					keyName = "0";	
				break;
				case KeyCode.Alpha1 :
					keyName = "1";	
				break;
				case KeyCode.Alpha2 :
					keyName = "2";	
				break;
				case KeyCode.Alpha3 :
					keyName = "3";	
				break;
				case KeyCode.Alpha4 :
					keyName = "4";	
				break;
				case KeyCode.Alpha5 :
					keyName = "5";	
				break;
				case KeyCode.Alpha6 :
					keyName = "6";	
				break;
				case KeyCode.Alpha7 :
					keyName = "7";	
				break;
				case KeyCode.Alpha8 :
					keyName = "8";	
				break;
				case KeyCode.Alpha9 :
					keyName = "9";	
				break;
				default :
					keyName = cKeyCode.ToString();
				break;
			}
			
			
			rocket.GetComponent<Launcher>().setLetter(keyName, cKeyCode);
			rocket.GetComponent<Launcher>().setPadCode(rocketTouches[i].padCode);
			rocket.GetComponent<Launcher>().setTexture(rocketTouches[i].texture);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (_state!=State.none) {
			return;
		}
		
		if (Capsule.count<=0) {
			Debug.Log("rocket loose");
			_state = State.planetWin;
			return;
			
		}
		if (Planet.count<=0) {
			Debug.Log("planet loose");
			_state = State.rocketWin;
		} else if (Planet.count==1) {
			GameObject[] s = GameObject.FindGameObjectsWithTag("Sun");
			if (s.Length>0) {
				Debug.Log("planet loose sun");
				_state = State.rocketWin;
			}
		}
	}
	
	void OnGUI()
	{
		if (_state!=State.none) {		
		  GUI.DrawTexture(
			new Rect(
				(Screen.width-Screen.width*0.3f)/2,
				(Screen.height-Screen.width*0.3f)/2,
				Screen.width*0.3f, Screen.width*0.3f
			),
			_state==State.planetWin?planetWin:rocketWin);
		}
	}
	
	enum State
	{
		none,
		planetWin,
		rocketWin
	}
	
	State _state = State.none;
	
}
