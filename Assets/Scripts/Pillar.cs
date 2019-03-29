using System.Collections;
using UnityEngine;

public class Pillar : MonoBehaviour, IShootable {
	[SerializeField] private Color color; // The color of the pillar (set in inspector)
	public Color Color { get; private set; }

	[SerializeField]
	private float destroyTimeout = 5; // How many seconds it takes for the pillar to delete itself after depleting

	private Renderer rend; // Renderer component used to change color
	private MaterialPropertyBlock matPropBlock;

	private bool depleted = true; // Determines if the Update loop runs
	
	public bool Depleted {
		private get => depleted;
		set {
			if (value) {
				// If depleted is being set to false, directly remove the color (set to white)
				SetColor(Color.white);
				// Start timed-out deletion
				StartCoroutine(DelayedDelete(destroyTimeout));
			}

			depleted = value;
		}
	}

	private SpawnLocation currentLocation;

	private void Awake() {
		matPropBlock = new MaterialPropertyBlock();
		rend = GetComponent<Renderer>();
	}

	private void Start() {
		SetColor();
	}

	private IEnumerator DelayedDelete(float time) {
		// Delete gameobject after 'time' seconds
		yield return new WaitForSeconds(time);
		
		// Release the attached spawn location if available
		currentLocation?.Release();
		// Destroy the object
		Destroy(gameObject);
	}

	public void AttachSpawnLocation(SpawnLocation location) {
		currentLocation = location;
	}
	
	public Color Extract() {
		return Color;
	}

	public void SetColor(Color? color = null) {
		// Needed for functioning with custom editor
		if (!rend) rend = GetComponent<Renderer>();
		if (matPropBlock == null) matPropBlock = new MaterialPropertyBlock();

		// Update color value if it is being changed (parameter was passed)
		if (color.HasValue) Color = color.Value;

		// Get current material properties from renderer
		rend.GetPropertyBlock(matPropBlock);
		// Set color
		// Use class property if no argument given, otherwise use argument
		matPropBlock.SetColor("_Color", color ?? Color);
		// Return changed material properties to renderer
		rend.SetPropertyBlock(matPropBlock);
	}
}