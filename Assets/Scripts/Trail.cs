using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Trail : MonoBehaviour {

	[Range(0.01f, 0.05f)]
	[SerializeField] float delay = 0.02f;

	[Range(10, 1000)]
	[SerializeField] RangeInt lifetimeRange;

	[Range(0.0f, 0.1f)]
	[SerializeField] float thickness = 0.05f;

	[SerializeField] TrailRuntimeSet runtimeSet;

	private TrailRenderer trailRenderer;

	private int numberOfSteps;

	private Spawner spawner;

	private Coroutine lifecycle;


	// Use this for initialization
	void Start () 
	{
		// Randomise number of steps
		numberOfSteps = Random.Range(10, 1000);

		spawner = GameObject.FindObjectOfType<Spawner>();

		// Get trail component
		trailRenderer = GetComponent<TrailRenderer>();

		// Set trail lifetime 
		trailRenderer.time = numberOfSteps * delay;

		// Set trail thickness
		trailRenderer.startWidth = thickness;
		trailRenderer.endWidth = 0.01f;

		// Set trail color
		trailRenderer.startColor = Color.cyan; //Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
		trailRenderer.endColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 0.2f, 0.2f);
		
		// Start Coroutine
		lifecycle = StartCoroutine(Lifecycle());
	}	

	private IEnumerator Lifecycle()
	{
		int steps = 0;

		while (steps < numberOfSteps) 
		{
			float newX = transform.position.x + (Random.Range(-100, 100) / 1000.0f);
			float newY = transform.position.y + (Random.Range(-100, 100) / 1000.0f);

			transform.position = new Vector3(newX, newY, 0f);

			if (Random.value <= 0.005) 
			{
				spawner.Spawn(transform.position);
			}

			// Increase steps counter
			steps++;

			// Wait for specified delay
			yield return new WaitForSeconds(delay);
		}
	}


	public void Kill()
	{
		StopCoroutine(lifecycle);

		GetComponent<Collider2D>().enabled = false;
		runtimeSet.Remove(this);
	}


	void OnEnable() 
	{
		runtimeSet.Add(this);
	}

	void OnDisable() 
	{
		runtimeSet.Remove(this);
	}

}
