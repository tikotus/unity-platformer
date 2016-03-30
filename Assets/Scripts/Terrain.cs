using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Terrain : MonoBehaviour {

	public Sprite left;
	public Sprite center;
	public Sprite right;
	public Sprite single;
	public string stopperLayer;
	public string groundLayer;

	public int size = 3;
	public float tileWidth = 0.69f;
	public float tileHeight = 0.69f;

	private List<GameObject> m_spriteObjects = new List<GameObject>();

	void Update () {
		if (m_spriteObjects.Count != size) {
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++) {
				DestroyImmediate(transform.GetChild(0).gameObject);
			}
			m_spriteObjects.Clear();
			if (size <= 1) {
				addSprite(single);
			}
			else {
				addSprite(left);
				for (int i = 0; i < size - 2; i++) {
					addSprite(center);
				}
				addSprite(right);
			}
			GetComponent<BoxCollider2D>().size = new Vector2(tileWidth * size, tileHeight);
			GetComponent<BoxCollider2D>().offset = new Vector2(tileWidth * (size - 1) / 2, 0.0f);
			gameObject.layer = LayerMask.NameToLayer(groundLayer);
			addStopper(-tileWidth / 2 - 0.1f);
			addStopper(tileWidth * size - tileWidth / 2 + 0.1f);
		}
	}

	void addStopper(float x) {
		GameObject stopper = new GameObject("stopper");
		BoxCollider2D collider = stopper.AddComponent<BoxCollider2D>();
		collider.size = new Vector2(0.1f, 0.1f);
		stopper.transform.parent = transform;
		stopper.layer = LayerMask.NameToLayer(stopperLayer);
		stopper.transform.localPosition = new Vector3(x, tileHeight / 2, 0.0f);
	}

	void addSprite(Sprite sprite) {
		GameObject spriteObject = new GameObject("tile");
		SpriteRenderer renderer = spriteObject.AddComponent<SpriteRenderer>();
		renderer.sprite = sprite;
		spriteObject.transform.parent = transform;
		Vector3 position = new Vector3(m_spriteObjects.Count * tileWidth, 0.0f);
		spriteObject.transform.localPosition = position;
		m_spriteObjects.Add(spriteObject);
	}
}
