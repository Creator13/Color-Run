using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour {
	[SerializeField] private Timer timer;
	[SerializeField] private Text timerDisplay;
	[SerializeField] private string menuSceneName;

	private void OnEnable() {
		timerDisplay.text = "Your time: " + timer.GetTime();
		Debug.Log(timerDisplay.text);
	}

	public void MainMenu() {
		SceneManager.LoadScene(menuSceneName);
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}