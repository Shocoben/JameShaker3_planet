using UnityEngine;
using System.Collections;

public class PlanetBase : MonoBehaviour {
	
	public float distanceEffect = 2;
	public float baseMassFactor = 10;
	
	public GameObject explosionFX;
	
	public static Planet selected = null;
	public static int instanceCount = 0;
	public int id = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
