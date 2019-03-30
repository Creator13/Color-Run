using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {
	[SerializeField] private string mainSceneName = "Main";
	
	public void Load() {
		SceneManager.LoadScene(mainSceneName);
	}

	public void Quit() {
		Application.Quit();
	}
}