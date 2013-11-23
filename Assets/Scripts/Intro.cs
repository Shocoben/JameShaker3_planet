using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	void LaunchGame() {
		Application.LoadLevel("Main");
	}
	
	void Update() {
		if (Input.GetKeyUp(KeyCode.Escape) ||
			Input.GetKeyUp(KeyCode.Space) ||
			Input.GetKeyUp(KeyCode.S)) {
		Application.LoadLevel("Main");
		}
	}
	
}
