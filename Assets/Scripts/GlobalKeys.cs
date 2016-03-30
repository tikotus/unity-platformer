using UnityEngine;
using System.Collections;

public class GlobalKeys : MonoBehaviour {

	public Transform menuOnEsc;

	private GameObject m_menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (m_menu) {
				Destroy(m_menu);
			}
			else {
				m_menu = ((Transform)Instantiate(menuOnEsc)).gameObject;
			}
		}
	}
}
