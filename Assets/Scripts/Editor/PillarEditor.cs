using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pillar))]
public class PillarEditor : Editor {
	private void OnSceneGUI() {
		Pillar pillar = (Pillar) target;

		pillar.SetColor();
	}
}