using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public GameObject target;
	public float springFactor = 0.1f;
	public float minY = -6.0f;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector2 targetPosition = target.transform.position;
			Vector2 position = transform.position;
			Vector2 newPosition = position + (targetPosition - position) * springFactor;
			newPosition.y = Mathf.Max(minY, newPosition.y);
			transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
		}
	}
}
