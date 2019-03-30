using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class PillarSpawner : MonoBehaviour {
	[SerializeField] private Transform pillarPrefab;
	[SerializeField] private Transform[] spawnLocations;
	
	private List<SpawnLocation> spawnLocationsList;
	private int pillarCount;

	private void Awake() {
		spawnLocationsList = new List<SpawnLocation>();
		
		// Construct a list of SpawnLocation objects based on array of transforms passed by unity editor
		foreach (Transform loc in spawnLocations) {
			spawnLocationsList.Add(new SpawnLocation(loc));
		}
	}

	public void SpawnPillar(Color c) {
		// Check if there are still spawn locations available
		if (pillarCount < spawnLocations.Length) {
			// Pick a random available spawn location
			List<SpawnLocation> available = spawnLocationsList.FindAll(el => !el.IsUsed);
			SpawnLocation loc = available[new Random().Next(available.Count)];
		
			// Build and initialize a pillar on the location
			Transform pillar = Instantiate(pillarPrefab, loc.spawnLocation);
			loc.Pillar = pillar;
			pillar.GetComponent<Pillar>().SetColor(c);
		
			// Recount current spawner occupation
			UpdatePillarCount();
		}
		else {
			// Throw a warning that the spawner is fully occupied
			Debug.LogWarning("Cannot spawn pillar, no more locations available");
		}
	}

	private void UpdatePillarCount() {
		// Recount all used spawn locations
		pillarCount = spawnLocationsList.FindAll(el => el.IsUsed).Count;
	}
}