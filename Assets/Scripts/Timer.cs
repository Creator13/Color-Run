using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	[SerializeField] private Text text;
	
	private float cumulativeTime = 0;

	public bool Paused { get; set; } = true;

	public void StartTimer() {
		Paused = false;
	}
	
	private void Update() {
		if (!Paused) {
			cumulativeTime += Time.deltaTime;
			text.text = "Time: " + GetTime();
		}
	}

	public void Hide() {
		text.gameObject.SetActive(false);
	}

	public string GetTime() {
		return (cumulativeTime).ToString("0");
	}
}