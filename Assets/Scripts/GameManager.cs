using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour {
	[SerializeField] private bool lockCursor = true;
	[SerializeField] private float endScreenTimeout = 1f;
	[SerializeField] private GameObject player;
	
	[Header("UI references")]
	[SerializeField] private EndGameController endGameController;
	[SerializeField] private PauseMenuController pauseMenuController;
	[SerializeField] private Canvas reticleCanvas;

	private Timer timer;

	private bool paused;
	public bool IsPaused {
		get => paused;
		set {
			// Open panel
			pauseMenuController.gameObject.SetActive(value);
			// Hide reticle
			reticleCanvas.gameObject.SetActive(!value);
			// Pause timer
			timer.Paused = value;
			// Lock player movement
			CanMove = !value;
			// Lock cursor
			CursorLocked = !value;
			// Save value
			paused = value;
		}
	}

	private bool canMove;
	public bool CanMove {
		set {
			// Disable player movement scripts
			player.GetComponent<CharacterController>().enabled = value;
			player.GetComponent<CharacterMovement>().enabled = value;
			// Save value
			canMove = value;
		}
	}

	private bool cursorLocked;
	public bool CursorLocked {
		get => cursorLocked;
		set {
			// Set lock state
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			// Hide cursor
			Cursor.visible = !value;
			// Save value
			cursorLocked = value;
		}
	}

	private void Awake() {
		// Disable pause and end game UI at start of game
		endGameController.gameObject.SetActive(false);
		pauseMenuController.gameObject.SetActive(false);
	}

	private void Start() {
		timer = GetComponent<Timer>();

		if (lockCursor) {
			CursorLocked = true;
		}

		timer.StartTimer();
	}

	private void Update() {
		if (lockCursor) {
			if (Input.GetKeyDown(KeyCode.L)) {
				// Toggle cursor lock
				CursorLocked = !CursorLocked;
			}
		}

		// Check for esc press and pause/unpause
		if (Input.GetButtonDown("Cancel")) {
			// Toggle pause
			IsPaused = !IsPaused;
		}
	}

	public void LoadSceneEnd() {
		// Lock player movement
		CanMove = false;

		StartCoroutine(LoadEndGameUI());
	}

	private IEnumerator LoadEndGameUI() {
		yield return new WaitForSeconds(endScreenTimeout);
		reticleCanvas.gameObject.SetActive(false);
		CursorLocked = false;
		endGameController.gameObject.SetActive(true);
		timer.Hide();
	}
}