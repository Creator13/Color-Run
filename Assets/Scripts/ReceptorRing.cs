using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class ReceptorRing : MonoBehaviour, IShootable {
	private static readonly int ColorId = Shader.PropertyToID("_Color");
	
	[SerializeField] private Color color;
	
	private Receptor receptor;

	private Renderer rend;
	private MaterialPropertyBlock matPropBlock;

	private void Awake() {
		matPropBlock = new MaterialPropertyBlock();
		rend = GetComponent<Renderer>();
	}

	private void Start() {
		SetColor(Color.cyan);
	}

	public void AttachReceptor(Receptor receptor) {
		this.receptor = receptor;
	}
	
	private void SetColor(Color color) {
		// Avoid null reference
		if (!rend) rend = GetComponent<Renderer>();

		// Save color for later use
		this.color = color;
		
		// Assign color to shader
		rend.GetPropertyBlock(matPropBlock);
		matPropBlock.SetColor("_Color", this.color);
		rend.SetPropertyBlock(matPropBlock);
	}

	private void SetEmmisive() {
		// Avoid null reference
		if (!rend) rend = GetComponent<Renderer>();

		Color emmissionColor = color * 1f;
		
		// Enable emission
		rend.material.EnableKeyword("_EMISSION");
		
		// Assign color to shader
		rend.GetPropertyBlock(matPropBlock);
		matPropBlock.SetColor("_EmissionColor", emmissionColor);
		rend.SetPropertyBlock(matPropBlock);
	}
	
	public void Hit(Color color) {
		SetEmmisive();
		// TODO notify receptor object
	}
}