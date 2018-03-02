using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trail))]
public class AddTrailToRuntimeSet : MonoBehaviour {

	[SerializeField] TrailRuntimeSet runtimeSet;

	private Trail trail;

	void OnEnable() 
	{
		trail = GetComponent<Trail>();
		runtimeSet.Add(trail);
	}

	void OnDisable() 
	{
		runtimeSet.Remove(trail);
	}

}
