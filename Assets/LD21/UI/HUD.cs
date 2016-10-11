using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUISkin Skin;
	
	public int ColorIndicatorWidth = 64;
	public int ColorIndicatorHeight = 64;
	
	private Texture2D screenTexture = null;
	
	private PlayerStatus Player;
	
	public void Start() {
		Player = GetComponent<PlayerStatus>();
		screenTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
		screenTexture.SetPixel(0, 0, Color.white);
	}
	
	public void Update() {
		// Read screen contents into the texture
		screenTexture.ReadPixels(new Rect(Screen.width/2, Screen.height/2, 1, 1), 0, 0);
		screenTexture.Apply();
		
		//handle input
		if (!Player.IsDead) {
			if (Input.GetKeyDown(KeyCode.Alpha0)) {
				Player.CurrentColor = Player.ColorCapabilities[0];
			} else if (Input.GetKeyDown(KeyCode.Alpha1)) {
				Player.CurrentColor = Player.ColorCapabilities[1];
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				Player.CurrentColor = Player.ColorCapabilities[2];
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				Player.CurrentColor = Player.ColorCapabilities[3];
			}
		} else {
			//handle respawn
			if (Input.anyKeyDown) {
				Player.IsDead = false;
			}
		}
	}
	
	public void OnGUI() {
		
		//BEGIN color value readout
		Color target_color = screenTexture.GetPixel(0, 0);
		GUI.Label(new Rect(32, 32, 300, 300), System.String.Format("{0:000}", Mathf.FloorToInt(target_color.r*255)), Skin.GetStyle("ColorValueReadout"));
		GUI.Label(new Rect(32, 100, 300, 300), System.String.Format("{0:000}", Mathf.FloorToInt(target_color.g*255)), Skin.GetStyle("ColorValueReadout"));
		GUI.Label(new Rect(32, 168, 300, 300), System.String.Format("{0:000}", Mathf.FloorToInt(target_color.b*255)), Skin.GetStyle("ColorValueReadout"));
		
		//begin color selector bar
		int start_x = (Screen.width/2) - ((Player.ColorCapabilities.Count * ColorIndicatorWidth) / 2);
		int curr_y = 0;
		
		//draw dark border
		GUI.Box(new Rect(start_x-8, 0, (Player.ColorCapabilities.Count * ColorIndicatorWidth) + 16, ColorIndicatorHeight+8), GUIContent.none, Skin.box);
		
		for (int i = 0; i < Player.ColorCapabilities.Count-1; i++) {
			ColorCapability c = Player.ColorCapabilities[i+1];
			if (c.Equals(Player.CurrentColor)) {
				curr_y = 0;
			} else {
				curr_y = -16;
			}
			GUI.Label(new Rect(start_x + (i*ColorIndicatorWidth), curr_y, ColorIndicatorWidth, ColorIndicatorHeight), Player.ColorCapabilities[i+1].indicator, Skin.GetStyle("ColorSelectionIndicator"));
			GUI.Label(new Rect(start_x + (i*ColorIndicatorWidth) + 5, 1, ColorIndicatorWidth, ColorIndicatorHeight), (i+1).ToString(), Skin.GetStyle("ColorSelectionIndicatorShadow"));
			GUI.Label(new Rect(start_x + (i*ColorIndicatorWidth) + 4, 0, ColorIndicatorWidth, ColorIndicatorHeight), (i+1).ToString(), Skin.GetStyle("ColorSelectionIndicator"));
		}
		//draw the zero index last, like the keyboard layout
		if (Player.ColorCapabilities[0].Equals(Player.CurrentColor)) {
			curr_y = 0;
		} else {
			curr_y = -16;
		}
		GUI.Label(new Rect(start_x + ((Player.ColorCapabilities.Count-1)*ColorIndicatorWidth), curr_y, ColorIndicatorWidth, ColorIndicatorHeight), Player.ColorCapabilities[0].indicator, Skin.GetStyle("ColorSelectionIndicator"));
		GUI.Label(new Rect(start_x + ((Player.ColorCapabilities.Count-1)*ColorIndicatorWidth) + 5, 1, ColorIndicatorWidth, ColorIndicatorHeight), "0", Skin.GetStyle("ColorSelectionIndicatorShadow"));
		GUI.Label(new Rect(start_x + ((Player.ColorCapabilities.Count-1)*ColorIndicatorWidth) + 4, 0, ColorIndicatorWidth, ColorIndicatorHeight), "0", Skin.GetStyle("ColorSelectionIndicator"));
	}
	
}