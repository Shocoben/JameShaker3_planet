using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public GameObject helpButton;

	// Use this for initialization
	void Awake () {
		Time.timeScale = 0;
	
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
	}
	
	void StartGame()
	{
		Time.timeScale = 1;	
		guiTexture.enabled = false;
		helpButton.guiTexture.enabled = false;
	}
}
