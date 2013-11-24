using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour 
{
	public GameObject helpButton;
	public GameObject startButton;
	
		 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		helpButton.guiTexture.enabled = true;
		startButton.guiTexture.enabled = true;
		gameObject.SetActive(false);
	}
}
