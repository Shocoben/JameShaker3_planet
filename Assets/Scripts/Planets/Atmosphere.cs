using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {
	
	public GameObject quad;
	public Material normal;
	public Material angry;
	
	public void SetAngry(bool a) {
		if (a) {
			if (!_angry) {
				quad.renderer.material = angry;
			}
		} else if (_angry) {
			quad.renderer.material = normal;
		}
		_angry = a;
	}
	
	// Use this for initialization
	void Start () {
		quad.renderer.material = normal;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private bool _angry = false;
}
