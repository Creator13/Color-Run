using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

[Serializable]
public class SpawnLocation {
	[SerializeField] public Transform spawnLocation;
	public Transform Pillar { private get; set; }

	public bool IsUsed => Pillar != null;

	public void Release() {
		Pillar = null;
	}
}

public class PillarSpawner : MonoBehaviour {
	[SerializeField] private Transform pillarPrefab;
	[SerializeField] private List<SpawnLocation> spawnLocations;
	private int pillarCount;

	public void SpawnPillar(Color c) {
		if (pillarCount < spawnLocations.Count) {
			List<SpawnLocation> available = spawnLocations.FindAll(el => !el.IsUsed);
			SpawnLocation loc = available[new Random().Next(available.Count)];

			Transform pillar = Instantiate(pillarPrefab, loc.spawnLocation);
			loc.Pillar = pillar;
			pillar.GetComponent<Pillar>().SetColor(c);

			UpdatePillarCount();
		}
		else {
			Debug.LogWarning("Cannot spawn pillar, no more locations available");
		}
	}

	private void UpdatePillarCount() {
		pillarCount = spawnLocations.FindAll(el => el.IsUsed).Count;
	}
}