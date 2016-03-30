using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * This is a light version of the CharacterController
 * designed for a Mario style 2D platformer.
 */
[RequireComponent(typeof(UnityEngine.BoxCollider2D))]
public class CustomCharacterController : MonoBehaviour {
	public bool isGrounded { get; private set; }
	public List<Collider2D> verticalColliders { get; private set; }
	public List<Collider2D> horizontalColliders { get; private set; }

	void Start () {
		isGrounded = false;
		verticalColliders = new List<Collider2D>();
		horizontalColliders = new List<Collider2D>();
	}

	public void Move(Vector2 delta) {
		Move(delta, -1);
	}
	
	public void Move(Vector2 delta, LayerMask mask) {
		isGrounded = false;
		Vector2 position = transform.position;
		position = moveHorizontal(position, delta.x, mask);
		position = moveVertical(position, delta.y, mask);
		transform.position = position;
	}

	List<Collider2D> getColliders(Vector2 position, LayerMask mask) {
		Vector2 size = GetComponent<BoxCollider2D>().size;
		Vector2 center = GetComponent<BoxCollider2D>().offset;
		Collider2D[] colliders = Physics2D.OverlapAreaAll(position - size / 2 + center, 
		                                                  position + size / 2 + center, 
		                                                  mask);
		List<Collider2D> result = new List<Collider2D>();
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				result.Add(colliders[i]);
			}
		}
		return result;
	}


	Vector2 moveHorizontal(Vector2 position, float amt, LayerMask mask) {
		Vector2 newPosition = new Vector2(position.x + amt, position.y);
		horizontalColliders = getColliders(newPosition, mask);
		if (horizontalColliders.Count > 0) {
			return position;
		}
		else {
			return newPosition;
		}
	}

	Vector2 moveVertical(Vector2 position, float amt, LayerMask mask) {
		Vector2 newPosition = new Vector2(position.x, position.y + amt);
		verticalColliders = getColliders(newPosition, mask);
		if (verticalColliders.Count > 0) {
			if (amt < 0.0f) {
				isGrounded = true;
			}
			return position;
		}
		else {
			return newPosition;
		}
	}
}
