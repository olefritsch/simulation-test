using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour 
{
	public TransformRuntimeSet runtimeSet;

	private float speed = 0.1f;

	private Spawner spawner;
	private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () 
	{
		spawner = GameObject.FindObjectOfType<Spawner>();

		trailRenderer = GetComponent<TrailRenderer>();

		// Set trail color
		trailRenderer.startColor = Color.cyan; //Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
		trailRenderer.endColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 0.2f, 0.2f);

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector3 movement = Random.onUnitSphere * speed;
		movement.z = 0;

		transform.Translate(movement);


		if (Random.value <= 0.005) 
		{
			spawner.Spawn(transform.position);
		}
	}


	void OnEnable()
	{
		runtimeSet.Add(this.transform);
	}

	void OnDisable()
	{
		runtimeSet.Remove(this.transform);
	}
}
