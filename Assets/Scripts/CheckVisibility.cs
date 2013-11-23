using UnityEngine;
using System.Collections;

public class CheckVisibility : MonoBehaviour {

	void OnBecameInvisible() {
		// TODO use real settings from camera
		if (transform.parent) {
			if (transform.parent.transform.position.x<-8 || transform.parent.transform.position.x>8 ||
				transform.parent.transform.position.z<-4.5 || transform.parent.transform.position.z>4.5) {
				transform.parent.transform.position = new Vector3(-transform.parent.transform.position.x,0,-transform.parent.transform.position.z);
			}
		} else {
			if (transform.position.x<-8 || transform.position.x>8 ||
				transform.position.z<-4.5 || transform.position.z>4.5) {
				transform.position = new Vector3(-transform.position.x,0,-transform.position.z);
			}
		}
    }

}
