using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Receptor : MonoBehaviour {
	[SerializeField] private ReceptorRing[] rings;
	[SerializeField, Tooltip("Map of colors to use in random generator")] private Gradient colorMap;
	[SerializeField] private PillarSpawner spawner;

	private List<float> colorList = new List<float>(); // List of references to color on gradient map

	private AudioSource audioSource;
	
	private int ringCount;
	private int ringsCompleted;

	private void Awake() {
		// Attach this receptor to all the rings to allow communication
		foreach (ReceptorRing ring in rings) {
			ring.AttachReceptor(this);
		}

		audioSource = GetComponent<AudioSource>();
	}

	private void Start() {
		ringCount = rings.Length;
		
		Generate();
	}

	public void NotifyRingHit() {
		ringsCompleted++;
		audioSource.Play();

		if (ringsCompleted == ringCount) {
			// Load game end screen
			GameManager gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

			gm.LoadSceneEnd();
		}
	}

	private void Generate() {
		Reset();

		foreach (ReceptorRing ring in rings) {
			// Generate a position on the gradient map
			float colorPos = Random.Range(0f, 1f);
			// Retrieve color from gradient
			Color generated = colorMap.Evaluate(colorPos);

			// Set color as key for current ring
			ring.SetKeyColor(generated);

			// Generate pillar
			if (spawner) spawner.SpawnPillar(generated);

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