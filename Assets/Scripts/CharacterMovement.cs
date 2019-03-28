using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour {

	[SerializeField]
	private Camera cam;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float camRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private Vector3 jumpForce = Vector3.zero;

	[SerializeField]
	private float cameraRotationLimit = 85f;
	
	private Rigidbody rb;

	private void Start() {
		// Obtain rigidbody component
		rb = GetComponent<Rigidbody>();
	}
	
	private void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	// Set movement velocity amount
	public void Move(Vector3 vel) {
		velocity = vel;
	}

	// Set rotation amount
	public void Rotate(Vector3 rot) {
		rotation = rot;
	}

	// Set camera rotation amount
	public void RotateCamera(float camRotX) {
		camRotationX = camRotX;
	}

	// Set jump force amount
	public void Jump(Vector3 jump) {
		rb.AddForce(jump);
	}

	// Execute movement
	private void PerformMovement() {
		// Forward and strafe
		if (velocity != Vector3.zero) {
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	// Execute player and camera rotation
	private void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime * 50));

		// Only perform camera rotation if camera is set to an instance
		if (cam != null) {
			currentCameraRotationX -= camRotationX * Time.fixedDeltaTime * 50;
			
			// Limit rotation
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
			
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0,0);
		}
	}
}