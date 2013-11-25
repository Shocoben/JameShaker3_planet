using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour 
{
	public GameObject helpButton;

	// Use this for initialization
	void Awake () {
		Time.timeScale = 0;
	
	}
	
	void Start() {
		GUITexture gtext = GetComponent<GUITexture>();
		gtext.pixelInset = new Rect(
			-(gtext.pixelInset.width*Screen.width/1600)*0.5f,
			(gtext.pixelInset.height*Screen.height/1200),
			gtext.pixelInset.width*Screen.width/1600,
			gtext.pixelInset.height*Screen.height/1200);
		
		gtext = helpButton.GetComponent<GUITexture>();
		gtext.pixelInset = new Rect(
			-(gtext.pixelInset.width*Screen.width/1600)*0.5f,
			0,
			gtext.pixelInset.width*Screen.width/1600,
			gtext.pixelInset.height*Screen.height/1200);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton7))
		{
			StartGame();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(1);
		}
	
	}
	
	void OnMouseDown()
	{
		StartGame();
		audio.Play();
	}
	
	void StartGame()
	{
		Time.timeScale = 1;	
		guiTexture.enabled = false;
		helpButton.guiTexture.enabled = false;
	}
}
