using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class ReceptorRing : MonoBehaviour, IShootable {
	[SerializeField] private float emissionStrength = 1.5f; // HDR strength of emission after activation

	[SerializeField] private Color unsetColor = new Color(0.32f, 0.4f, 0.42f, 1.0f); // Default color

	private Receptor receptor; // The receptor pillar this ring is attached to (set by AttachReceptor)
	private Color color;       // The color this receptor accepts.
	private Color keyColor;
	private bool isHit; // State variable hit is true if this receptor was successfully activated

	private bool IsHit {
		get => isHit;
		set {
			if (isHit != value) {
				// Only change emission setting when new value is different
				SetEmmisive(value);
			}

			isHit = value;
		}
	}

	// Fields needed for material (color) editing
	private Renderer rend;
	private MaterialPropertyBlock matPropBlock;

	private void Awake() {
		// Initialize fields
		matPropBlock = new MaterialPropertyBlock();
		rend = GetComponent<Renderer>();
	}

	public void AttachReceptor(Receptor receptor) {
		this.receptor = receptor;
	}

	public void Reset() {
		IsHit = false;
		SetColor(unsetColor);
	}

	public void SetKeyColor(Color color) {
		keyColor = color;
		SetColor(keyColor);
	}

	// Todo find out how im gonna show the color that needs to be collected
	private void SetColor(Color color) {
		// Avoid null reference (allows usage of custom editors)
		if (!rend) rend = GetComponent<Renderer>();

		// Save color for later use
		this.color = color;

		// Assign color to shader
		rend.GetPropertyBlock(matPropBlock);
		matPropBlock.SetColor("_Color", this.color);
		rend.SetPropertyBlock(matPropBlock);
	}

	private void SetEmmisive(bool emmission) {
		// Avoid null reference
		if (!rend) rend = GetComponent<Renderer>();

		Color emmissionColor = color * emissionStrength;

		if (emmission) {
			// Enable emission
			rend.material.EnableKeyword("_EMISSION");

			// Assign color to shader
			rend.GetPropertyBlock(matPropBlock);
			matPropBlock.SetColor("_EmissionColor", emmissionColor);
			rend.SetPropertyBlock(matPropBlock);
		}
		else {
			rend.material.DisableKeyword("_EMISSION");
		}
	}

	public bool Hit(Color color) {
		// Cannot hit this object if it is already hit
		if (IsHit) {
			Debug.Log("Tried to hit object that was already hit"); // Todo remove debug
			return false;
		}

		if (color == keyColor) {
			receptor.NotifyRingHit();
			IsHit = true;
			return true;
		}

		return false;
	}
}