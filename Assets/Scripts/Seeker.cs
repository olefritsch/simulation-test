using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Seeker : MonoBehaviour 
{
	[Range(0.01f, 0.05f)]
	[SerializeField] float delay = 0.02f;

	[Range(0.0f, 0.1f)]
	[SerializeField] float thickness = 0.05f;

	[SerializeField] TrailRuntimeSet entities;

	private TrailRenderer trailRenderer;

	private bool isSeeking = true;

	private Trail target;

	// Use this for initialization
	void Start () 
	{
		trailRenderer = GetComponent<TrailRenderer>();

		trailRenderer.startWidth = thickness;
		trailRenderer.endWidth = thickness / 10;

		// Set trail color
		trailRenderer.startColor = Color.red; //Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
		trailRenderer.endColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 0, 0);

		StartCoroutine(DoAIThings());
	}

	private IEnumerator Search()
	{
		while (target == null) 
		{
			// Make sure we keep moving
			MoveRandom();

			Collider2D collider = Physics2D.OverlapCircle(transform.position, thickness * 50); // ~0, QueryTriggerInteraction.Collide);

			if (collider != null) 
				target = collider.GetComponent<Trail>();

			yield return new WaitForSeconds(delay);

//			if (colliders.Length == 0) 
//			{
//				yield return new WaitForSeconds(delay);
//				continue;
//			}
//
//			int closestIndex = -1;
//
//			for (int i = 0; i < colliders.Length; i++) 
//			{
//				if (closestIndex < 0) 
//				{
//					closestIndex = i;
//					continue;
//				}
//					
//				float thisDistance = (colliders[i].transform.position - transform.position).magnitude;
//				float closestDistance = (colliders[closestIndex].transform.position - transform.position).magnitude;
//					
//				if (thisDistance < closestDistance)
//					closestIndex = i;
//			}
//				
//			target = colliders[closestIndex].GetComponent<Trail>();
		}
	}
	
	private IEnumerator DoAIThings() 
	{
		while (true) 
		{
			if (entities.Count < 1) 
			{
				MoveRandom();
				yield return new WaitForSeconds (delay);
				continue;
			}

			// If we do not have a target search for one
			if (target == null)
				yield return StartCoroutine(Search());

			// Check if we have reached the target
			if (Vector3.Distance (transform.position, target.transform.position) < thickness) 
			{
				target.Kill (); 
				target = null;
			}
			else 
			{
				MoveTowardsTarget();
			}

			// Wait for specified delay
			yield return new WaitForSeconds(delay);
		}
	}


	private void MoveTowardsTarget() 
	{
		// Normalize to ignore distance
		Vector3 direction = (target.transform.position - transform.position).normalized;

		// Add randomness to movement so we don't move in a perfectly straight line
		float randomInDirectionX = (Random.Range(-50, 50) + direction.x * 100) / 1000.0f;
		float randomInDirectionY = (Random.Range(-50, 50) + direction.y * 100) / 1000.0f;

		float newX = transform.position.x + randomInDirectionX;
		float newY = transform.position.y + randomInDirectionY;

		// Update position
		transform.position = new Vector3(newX, newY, 0f);

	}

	private void MoveRandom() 
	{
		float newX = transform.position.x + (Random.Range(-100, 100) / 1000.0f);
		float newY = transform.position.y + (Random.Range(-100, 100) / 1000.0f);

		transform.position = new Vector3(newX, newY, 0f);
	}


//	private IEnumerator Wait()
//	{
//		while (entities == null || entities.Count < 1) 
//		{
//			// Get mouse position as world point
//			Vector3 mousePosition = Input.mousePosition;
//			mousePosition.z = 0f;
//			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
//
//			Vector3 direction = (mousePosition - transform.position).normalized;
//
//			float x = (Random.Range(-50, 50) + direction.x * 100) / 1000.0f;
//			float y = (Random.Range(-50, 50) + direction.y * 100) / 1000.0f;
//
//			float newX = transform.position.x + x;
//			float newY = transform.position.y + y;
//
//			transform.position = new Vector3(newX, newY, 0f);
//
//			// Wait for specified delay
//			yield return new WaitForSeconds(delay);
//		}
//
//		//StartCoroutine(Seek());
//	}

}
