using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public float buttonWidth = 300;
	public float buttonHeight = 100;
	public string startText = "Start";
	public string quitText = "Quit";
	public GUIStyle startButtonStyle;
	public GUIStyle quitButtonStyle;
	public GUIStyle titleStyle;
	public Sprite title;
	public string nextScene = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2 * 2 - 10, Screen.height * 0.6f, buttonWidth, buttonHeight), startText, startButtonStyle)) {
			if (nextScene != "") {
				Application.LoadLevel(nextScene);
			}
			else {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if (GUI.Button(new Rect(Screen.width / 2 + 10, Screen.height * 0.6f, buttonWidth, buttonHeight), quitText, quitButtonStyle)) {
			Application.Quit();
		}
		GUI.Box(new Rect(Screen.width / 2 - buttonWidth,  Screen.height * 0.2f, buttonWidth * 2, buttonHeight * 2), title.texture, titleStyle);
	}
}
