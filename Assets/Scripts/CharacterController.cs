using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterController : MonoBehaviour {
	
	[SerializeField] private float speed = 5f;				// Player movement speed
	[SerializeField] private float sprintSpeed = 8f;		// Player movement speed while sprinting
	[SerializeField] private float jumpSpeed = 8f;			// Player jump force
	[SerializeField] private float lookSensitivity = 3f;	// Camera sensitivity
	[SerializeField] private LayerMask groundLayer;			// Layer mask with objects that count as ground

	private CharacterMovement movement;	// Reference the movement controller component
	private CapsuleCollider col;		// Reference the collider component

	// Distance from origin point to the floor.
	private float distToGround;

	private void Start() {
		// Obtain components (note that this script requires these components to be installed on the GameObject)
		movement = GetComponent<CharacterMovement>();
		col = GetComponent<CapsuleCollider>();

		// Distance to the underside of the player is the lower bound of the collider
		distToGround = col.bounds.extents.y;
	}

	private void Update() {
		// Calculate movement velocity as a 3D vector
		float xMov = Input.GetAxisRaw("Horizontal");
		float zMov = Input.GetAxisRaw("Vertical");

		Transform tf = transform;
		Vector3 movHorizontal = tf.right * xMov;
		Vector3 movVertical = tf.forward * zMov;

		// Get sprinting input
		bool sprint = Input.GetButton("Sprint");
		
		// Final movement vector
		Vector3 velocity = (movHorizontal + movVertical).normalized * (sprint ? sprintSpeed : speed);

		movement.Move(velocity);
		
		// Get jump (only if grounded)
		if (GetGrounded() && Input.GetButtonDown("Jump")) {
			Vector3 jumpForce = Vector3.zero;
			jumpForce = Vector3.up * jumpSpeed;
			
			// Apply jump
			movement.Jump(jumpForce * 100);
		}
		
		
		// Calculate character rotation (only for turning the character)
		float yRot = Input.GetAxisRaw("Mouse X");
		
		Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
		
		// Apply character rotation
		movement.Rotate(rotation);
		
		// Calculate camera rotation (only for turning the character)
		float xRot = Input.GetAxisRaw("Mouse Y");
		
		float camRotationX = xRot * lookSensitivity;

		movement.RotateCamera(camRotationX);
	}

	// Returns if the player is on the ground (ground is defined by 'ground' layer mask)
	private bool GetGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.25f, groundLayer);
	}
}