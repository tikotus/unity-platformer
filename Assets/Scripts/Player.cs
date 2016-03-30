using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float slowFactor = 0.7f;
	public float minSpeed = 0.3f;
	public float physicsFactor = 1.1f;
	public Vector2 maxSpeed = new Vector2(4.0f, 5.0f);
	public float runAcceleration = 0.4f;
	public float runStartSpeed = 0.5f;
	public float jumpStartSpeed = 5.0f;
	public float jumpAcceleration = 5.0f;
	public float jumpAccelerationTime = 0.5f;
	public float killJump = 10.0f;
	public float jumpAfterKillTime = 0.1f;
	public LayerMask layerMask;
	public Transform menuOnDeath;

	private float m_jumpAfterKillTimer;
	private float m_jumpAccelerationFactor;
	private Vector2 m_velocity = new Vector2(0.0f, 0.0f);

	void FixedUpdate () {

		CustomCharacterController controller = gameObject.GetComponent<CustomCharacterController>();

		// Custom physics work better for platformers
		{
			int xDirection = 0;
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				xDirection = -1;
			}
			else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				xDirection = 1;
			}
			if (xDirection == 0) {
				m_velocity.x *= slowFactor;
				if (Mathf.Abs(m_velocity.x) < minSpeed) {
					m_velocity.x = 0.0f;
				}
			}
			else {
				// Change in direction resets velocity
				if (xDirection * Mathf.Sign(m_velocity.x) - 1 != 0) {
					m_velocity.x = 0.0f;
				}
				m_velocity.x = Mathf.Max(runStartSpeed, Mathf.Abs(m_velocity.x)) * xDirection;
				m_velocity.x += runAcceleration * xDirection;

				Vector2 scale = transform.localScale;
				scale.x = Mathf.Abs(scale.x) * xDirection;
				transform.localScale = scale;
			}

			m_velocity.y += Physics.gravity.y * physicsFactor * Time.deltaTime;
			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
				if (controller.isGrounded || m_jumpAfterKillTimer > 0.0f) {
					m_velocity.y = Mathf.Max(0.0f, m_velocity.y);
					m_velocity.y += jumpStartSpeed;
					m_jumpAccelerationFactor = jumpAccelerationTime;
					m_jumpAfterKillTimer = 0.0f;
					GetComponent<Animator>().SetTrigger("Jump");
				}
				m_velocity.y += jumpAcceleration * m_jumpAccelerationFactor * Time.deltaTime;
			}
			else {
				m_jumpAccelerationFactor = 0.0f;
			}
			m_jumpAccelerationFactor = Mathf.Max(0.0f, m_jumpAccelerationFactor - Time.deltaTime);
			m_jumpAfterKillTimer -= Time.deltaTime;

			m_velocity.x = Mathf.Min(Mathf.Abs(m_velocity.x), maxSpeed.x) * Mathf.Sign(m_velocity.x);
			m_velocity.y = Mathf.Min(Mathf.Abs(m_velocity.y), maxSpeed.y) * Mathf.Sign(m_velocity.y);
		}

		controller.Move(m_velocity * Time.deltaTime, layerMask);


		foreach (Collider2D collider in controller.horizontalColliders) {
			m_velocity.x = 0.0f;
			if (collider.gameObject.tag == "Enemy") {
				Die();
				return;
			}
		}

		bool falling = m_velocity.y < 0.0f;
		foreach (Collider2D collider in controller.verticalColliders) {
			m_velocity.y = 0.0f;
			if (collider.gameObject.tag == "Enemy") {
				if (!falling) {
					Die();
				}
				else {
					collider.gameObject.GetComponent<Enemy>().Die();
					m_velocity.y = killJump;
					m_jumpAfterKillTimer = jumpAfterKillTime;
				}
			}
		}

		if (transform.position.y < -20.0f) {
			Die();
		}

		GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(m_velocity.x));
	}

	void Die() {
		Instantiate(menuOnDeath);
		Destroy(gameObject);
	}
}
