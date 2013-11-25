using UnityEngine;
using System.Collections;

public class ShakePosition : MonoBehaviour {
	
	public float shakeX = 2;
	public float shakeY = 2;
	public float shakeTime = 1;
	
	// Use this for initialization
	public void Shake (float factor=1f) {
	    Hashtable ht = new Hashtable();
	    ht.Add("x",shakeX*factor);
	    ht.Add("y",shakeY*factor);
	    ht.Add("time",shakeTime);
	    iTween.ShakePosition(gameObject, ht);	
	}
	
	void Update() {
		if (Input.GetKeyUp(KeyCode.S)) {
			Shake();
		}
	}
}
