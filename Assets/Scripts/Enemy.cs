using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = -1.0f;
	public LayerMask layerMask;

	// Update is called once per frame
	void Update () {
		CustomCharacterController controller = GetComponent<CustomCharacterController>();
		controller.Move (new Vector2(speed * Time.deltaTime, 0.0f), layerMask);
		if (controller.horizontalColliders.Count > 0) {
			speed = -speed;
			Vector2 scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
		}
	}

	public void Die() {
		Destroy(gameObject);
	}
}
