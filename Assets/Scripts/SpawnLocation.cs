using UnityEngine;

public class SpawnLocation {
	public Transform spawnLocation;
	public Transform Pillar { private get; set; }

	public bool IsUsed => Pillar != null;

	public void Release() {
		Pillar = null;
	}

	public SpawnLocation(Transform location) {
		spawnLocation = location;
		Pillar = null;
	}
}