using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Receptor : MonoBehaviour {
	[SerializeField] private ReceptorRing[] rings;
	[SerializeField, Tooltip("Map of colors to use in random generator")] private Gradient colorMap;
	[SerializeField] private PillarSpawner spawner;

	private List<float> colorList = new List<float>(); // List of references to color on gradient map

	private void Awake() {
		// Attach this receptor to all the rings to allow communication
		foreach (ReceptorRing ring in rings) {
			ring.AttachReceptor(this);
		}
	}

	private void Start() {
		Generate();
	}

	private void Generate() {
		Reset();

		foreach (ReceptorRing ring in rings) {
			// Generate a position on the gradient map
			float colorPos = Random.Range(.2f * (1f / rings.Length), 1f / rings.Length) + colorList.Sum();
			// Retrieve color from gradient
			Color generated = colorMap.Evaluate(colorPos);

			// Set color as key for current ring
			ring.SetKeyColor(generated);
			
			// Generate pillar
			spawner.SpawnPillar(generated);
			
			// Save color on list
			colorList.Add(colorPos);
		}
	}

	private void Reset() {
		// Clear color list
		colorList.Clear();

		// Reset all rings
		foreach (ReceptorRing ring in rings) {
			ring.Reset();
		}
	}
}