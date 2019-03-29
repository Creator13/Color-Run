using UnityEngine;

public class GunController : MonoBehaviour {
	[SerializeField] private Camera cam;
	[SerializeField] private GameObject magazine;
	[SerializeField] private float range = 50f;
	[SerializeField] private float extractionRate = 25f; // Percent per second

	private float containerFill;
	private Color? colorInContainer;
	private GunContainer container;

	private const float _emptyingTimeout = 2;
	private float timeSinceLastEmpty;
	
	private void Start() {
		container = magazine.GetComponent<GunContainer>();
		container.SetFillPercentage(0);
		
		timeSinceLastEmpty = _emptyingTimeout;
	}

	private void Update() {
		// Fire if the 'Fire' button is pressed
		if (Input.GetButton("Fire")) {
			Shoot();
		}

		// Update timeout
		if (timeSinceLastEmpty < _emptyingTimeout) {
			timeSinceLastEmpty += Time.deltaTime;
		}
	}

	// Handle code for firing
	private void Shoot() {
		// Origin point for the raycast is center of the camera (TODO why is this not just camera.transform?)
		Vector3 origin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

		// Raycast forward from camera origin, within user-set gun range
		if (Physics.Raycast(origin, cam.transform.forward, out RaycastHit hit, range)) {
			// Try to find a receptor or pillar component (interfaced through IShootable) on the object that was hit
			IShootable hitObject = hit.collider.GetComponent<IShootable>();

			// If hit pillar, fill the gun container
			if (hitObject is Pillar pillar) {
				FillContainer(pillar);
			}
			// If hit receptor, empty the container
			else if (hitObject is ReceptorRing recp) {
				// Use timeout to register a click only once (instead of firing multiple colors after one another)
				if (! (timeSinceLastEmpty < _emptyingTimeout)) recp.Hit(EmptyContainer());
			}
		}
	}

	// Extraction amount normalized to framerate and with the rate converted from percentage to number
	private float GetExtractionAmt() {
		return Time.deltaTime * (extractionRate / 100);
	}
	
	// Fills the color container of the gun
	private void FillContainer(Pillar pillar) {
		// Fill container if it's not full
		if (containerFill < 1f) {
			// Extract from pillar
			Color c = pillar.Extract(GetExtractionAmt());
			
			// Add fill to container
			// WARNING: note that this is unrelated to pillar extraction, both values are changed separately
			containerFill = Mathf.Clamp(containerFill + GetExtractionAmt(), 0f, 1f);
			
			Debug.Log("Filling: " + containerFill * 100f + "%"); // TODO remove debugging code
			
			// If the color is empty (null) or different from the color currently being extracted, empty the container
			// and load a new color
			if (colorInContainer != c) {
				colorInContainer = c;
				containerFill = 0;
			}
		}
		else {
			// If container is full and color is set
			if (colorInContainer != null) {
				// Set container color to pillar color (which was loaded into the variable earlier)
				Debug.Log("Done"); // TODO remove debugging code
			}
		}
		
		UpdateContainer();
	}

	// Empties the contents of the color container of the gun
	private Color EmptyContainer() {
		// Only run if both control vars are in the correct state (container must be filled and a color must be set)
		// TODO this code could turn flaky, fix use of two control variables
		if (colorInContainer.HasValue && containerFill >= 1f) {
			// Reset the color of the container to white to represent empty

			// Clear stored color and return
			Color returnColor = colorInContainer.Value;
			colorInContainer = null;
			containerFill = 0;
			
			UpdateContainer();

			// Set timeout
			timeSinceLastEmpty = 0;
			
			return returnColor;
		}
		
		// Return white as 'empty' color
		return Color.white;
	}

	private void UpdateContainer() {
		container = magazine.GetComponent<GunContainer>();
		container.SetFillPercentage(containerFill);
		
		if (colorInContainer != null) {
			container.SetColor(colorInContainer.Value);
		}
	}
}