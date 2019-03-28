using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IShootable {
	[SerializeField] private Color color;	// The color of the pillar (set in inspector)

	private Renderer rend;	// Renderer component used to change color

	private float contents = 1f;	// How much color still is in this pillar
	private bool active = true;		// Determines if the Update loop runs

	private void Start() {
		SetColor();
	}

	private void Update() {
		// Set the color of the pillar to white to show it's color has been depleted
		if (active && contents <= 0) {
			rend.material.color = Color.white;
			// Deactivate so the above code doesn't keep running, might save some performance
			active = false;
		}
		
		//TODO add recharge in case players stops, moves to different pillar, then comes back (or fix this loophole in a different way)
	}

	public Color Extract(float extractRate) {
		// Subtract contents only if there are contents
		if (contents > 0) {
			// Subtract the amount indicated by the gun that orders for extraction, or the amount until zero reached if this value is smaller
			contents -= extractRate > contents ? contents : extractRate;
		}
		// Always return color because of state conflict (TODO maybe solve this conflict?)
		return color;
	}

	public void SetColor() {
		rend = GetComponent<Renderer>();

		// Create copy of the original material with the color set in the inspector
		Material matCopy = new Material(rend.material) {color = color};
		rend.material = matCopy;
	}
}