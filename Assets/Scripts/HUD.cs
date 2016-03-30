using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public Sprite collectableTexture;
	public Sprite timesTexture;
	public GUIStyle picStyle;
	public GUIStyle textStyle;

	void OnGUI() {
		GUI.Box(new Rect(10, 10, 100, 100), collectableTexture.texture, picStyle);
		GUI.Box(new Rect(50, 10, 100, 100), timesTexture.texture, picStyle);
		GUI.Box(new Rect(125, 10, 100, 100), Collector.collectedCount.ToString(), textStyle);
	}
}
