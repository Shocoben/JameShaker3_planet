using UnityEngine;
using System.Collections;

public class DieOnInvisible : MonoBehaviour {

	void OnBecameInvisible() {
		if (transform.parent!=null) {
			GameObject.Destroy(transform.parent.gameObject);
		} else {
			GameObject.Destroy(this);
		}
    }

}
