using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	
	
	private static Sun s_Instance = null;
	public static Sun Instance()
	{
		return s_Instance;
	}
	
	// Use this for initialization
	void Awake () {
		s_Instance = this;
	}
	
}
