using UnityEngine;
using System.Collections;

public class Sun : Planet {
	
	public string SUNCLASS = "---------------------------------------";
	public float rate = 1;
	private float _lastDestroy = 0;
	public int peopleToDestroyPerRate = 1;
	
	public override void Update ()
	{
		base.Update ();
		if (_lastDestroy + rate < Time.time)
		{
			addPeople(-peopleToDestroyPerRate);
			_lastDestroy = Time.time;
		}
	}
	
	
	
}
