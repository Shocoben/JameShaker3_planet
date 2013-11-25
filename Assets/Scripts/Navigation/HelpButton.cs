using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour 
{
	public GameObject helpImage;
	public GameObject startButton;

	
	void Update () 
	{
	
	}
	
	void OnMouseDown()
	{
		audio.Play();
		helpImage.SetActive(true);
		guiTexture.enabled = false;
		startButton.guiTexture.enabled = false;
	}
}
