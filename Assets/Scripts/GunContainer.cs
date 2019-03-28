using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour {
	[SerializeField] private GameObject fillCube;
	[SerializeField] private float maxHeight;

	private Renderer rend;

	private void Start() {
		rend = fillCube.GetComponent<Renderer>();
	}

	public void SetFillPercentage(float pct) {
		pct = Mathf.Clamp(pct, 0, 1);

		float fillHeight = pct * maxHeight;

		Debug.Log("Gun fill scale: " + fillHeight);
		fillCube.transform.localScale = new Vector3(fillCube.transform.localScale.x, fillHeight, fillCube.transform.localScale.z);
		fillCube.transform.localPosition = new Vector3(0, fillHeight / 2f, 0);
	}

	public void SetColor(Color c) {
		rend.material.color = c;
	}
}