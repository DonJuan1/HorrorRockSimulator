using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover: MonoBehaviour
{
	[SerializeField]
	private float speed = 4.0f;
	[SerializeField]
	private float jumpForce = 4.0f;

	private Vector2 rot;
	private bool isGrounded;
	private Rigidbody characterRigidbody;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		characterRigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void FixedUpdate()
	{
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate(straffe, 0, translation);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			characterRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}

	private void OnCollisionStay()
	{
		isGrounded = true;
	}
}
