using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour, IShootable {
	private Renderer rend;
	
	private void Start() {
		rend = GetComponent<Renderer>();
	}

	public void Hit(Color color) {
		rend.material.color = color;
	}

}