using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private bool lockCursor = true;
	
	private void Start() {
		if (lockCursor) {
			LockCursor();
		}
	}

	// Update is called once per frame
	private void Update() {
		if (lockCursor) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
            	if (Cursor.lockState == CursorLockMode.Locked) {
            		UnlockCursor();
            	}
            	else if (Cursor.lockState == CursorLockMode.None) {
            		LockCursor();
            	}
            }
		}
		
	}

	private void UnlockCursor() {
		Cursor.lockState = CursorLockMode.None;
	}
	
	private void LockCursor() {
		Cursor.lockState = CursorLockMode.Locked;
	}
}