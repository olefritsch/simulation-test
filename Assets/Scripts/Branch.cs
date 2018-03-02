using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Branch : MonoBehaviour 
{
	[Range(0.01f, 0.05f)]
	[SerializeField] float delay = 0.2f;

	[Range(0, 1000)]
	[SerializeField] int numberOfSteps = 500;

	[Range(0.0f, 0.1f)]
	[SerializeField] float thickness = 0.05f;

	// [SerializeField] Color startColor;
	// [SerializeField] Color endColor;

	private LineRenderer lineRenderer;

	private List<Vector3> positions;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () 
	{
		positions = new List<Vector3>();

		// Set last position to the gameObjects position
		lastPosition = transform.position;

		// Setup LineRenderer Component
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startWidth = thickness;
		lineRenderer.endWidth = thickness;
		lineRenderer.startColor = Random.ColorHSV();
		lineRenderer.endColor = Random.ColorHSV();

		// Start Coroutine
		StartCoroutine(DrawBranch());
	}

	private IEnumerator DrawBranch()
	{
		int steps = 0;

		while (steps < numberOfSteps) 
		{
			float newX = lastPosition.x + (Random.Range(-100, 100) / 250.0f);
			float newY = lastPosition.y + (Random.Range(-100, 100) / 250.0f);

			Vector3 newPosition = new Vector3(newX, newY, 0f);
			positions.Add(newPosition);

			lineRenderer.positionCount = steps + 1;
			lineRenderer.SetPosition(steps, newPosition);


			//lineRenderer.SetPositions(positions.ToArray());

			// Save the position for next iteration
			lastPosition = newPosition;

			// Increase steps counter
			steps++;

			// Wait for specified delay
			yield return new WaitForSeconds(delay);
		}
	}

}
