using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] GameObject spawnPrefab;
	[SerializeField] int maxNumberOfPrefabs = 1000;

	void Start() 
	{
		Spawn(Vector3.zero);
	}
		
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			Vector3 spawnPoint = Input.mousePosition;
			spawnPoint.z = 0f;
			spawnPoint = Camera.main.ScreenToWorldPoint(spawnPoint);

			Spawn(spawnPoint);
		}
	}

	public void Spawn(Vector3 spawnPoint)
	{
		if (!spawnPrefab) 
		{
			Debug.LogError("Branch Prefab is not assigned");
			return;
		}

		// Limit number of prefabs spawned to prevent performance issues
		if (transform.childCount >= maxNumberOfPrefabs) 
			return;

		Instantiate(spawnPrefab, spawnPoint, Quaternion.identity, this.transform);

	}
}
