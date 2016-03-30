using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.BoxCollider2D))]
public class Collector : MonoBehaviour {

	public LayerMask coinLayer;
	// TODO: This is not a good place for this property
	public string nextScene = "MainMenu";

	public static int collectedCount;

	void Start () {
		collectedCount = 0;
	}

	// Update is called once per frame
	void Update () {
		Vector2 size = GetComponent<BoxCollider2D>().size;
		Vector2 center = GetComponent<BoxCollider2D>().offset;
		Vector2 position = transform.position;
		Collider2D[] colliders = Physics2D.OverlapAreaAll(position - size / 2 + center, 
		                                                  position + size / 2 + center, 
		                                                  coinLayer);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].tag == "Objective") {
				Application.LoadLevel(nextScene);
			}
			else {
				collectedCount++;
				Destroy(colliders[i].gameObject);
			}
		}
	}
}
