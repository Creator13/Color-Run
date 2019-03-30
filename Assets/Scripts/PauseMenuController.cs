using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
	[SerializeField] private GameManager gm;
	[SerializeField] private string menuSceneName = "Menu";

	public void Unpause() {
		gm.IsPaused = false;
	}

	public void QuitGame() {
		SceneManager.LoadScene(menuSceneName);
	}
	
}