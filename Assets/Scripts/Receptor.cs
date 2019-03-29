using UnityEngine;

public class Receptor : MonoBehaviour {
	[SerializeField] private ReceptorRing[] rings;

	private Renderer rend;

	private void Awake() {
		foreach (ReceptorRing ring in rings) {
			ring.AttachReceptor(this);
		}
	}

	private void Start() {
		foreach (ReceptorRing ring in rings) {
			
		}
	}
}