using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorCollisionManager : MonoBehaviour {
	
	public PlayerStatus Player;
	
	private Dictionary<ColorCapability, List<GameObject>> Index;

	void Start () {
		Index = new Dictionary<ColorCapability, List<GameObject>>();
		
		//build index of gameobjects mapped to colors
		//Renderer[] all_meshes = (Renderer[])FindObjectsOfTypeAll(typeof(Renderer));
		Renderer[] all_meshes = this.GetComponentsInChildren<Renderer>();
		Debug.Log(System.String.Format("There are {0} meshes in the scene", all_meshes.Length));
		
		//set up the empty indexes for each color
		foreach (ColorCapability color in Player.ColorCapabilities) {
			Index.Add(color, new List<GameObject>());
		}
		//index all the renderers in the environment
		foreach (Renderer mesh in all_meshes) {
			foreach (ColorCapability color in Player.ColorCapabilities) {
				if (mesh.sharedMaterial.Equals(color.material)) {
					Index[color].Add(mesh.gameObject);
					break;
				}
			}
		}
		
		foreach (ColorCapability color in Index.Keys) {
			Debug.Log(System.String.Format("There are {0} {1} Renderers in the scene", Index[color].Count, color.name));
		}
		
//		//make all materials solid to start
//		foreach (ColorCapability cc in Player.ColorCapabilities) {
//			if (null != cc.material) {
//				float a = 1.0f;
//				if (cc.Equals(Player.CurrentColor)) {
//					a = 0.3f;
//				} else {
//					a = 1.0f;
//				}
//				Color c = cc.material.color;
//				cc.material.color = new Color(c.r, c.g, c.b, a);
//			}
//		}
		
		//register for player color change events
		Player.ColorChanged += HandlePlayerColorChange;
	}
	
	private void HandlePlayerColorChange(object sender, ColorChangedEventArgs eventArgs) {
		//make all currently-transparent objects solid
		if (null != eventArgs.OldColor.material) {
			foreach (GameObject gob in Index[eventArgs.OldColor]) {
				gob.GetComponent<Renderer>().sharedMaterial = eventArgs.OldColor.material;
				gob.GetComponent<Collider>().isTrigger = false;
			}
		}
		
		//make all new color objects transparent
		if (null != eventArgs.NewColor.material) {
			if (!eventArgs.NewColor.name.Equals("white")) {
				foreach (GameObject gob in Index[eventArgs.NewColor]) {
					gob.GetComponent<Renderer>().sharedMaterial = eventArgs.NewColor.transparentMaterial;
					gob.GetComponent<Collider>().isTrigger = true;
				}
			}
		} else {
			Debug.LogError(System.String.Format("The color capability '{0}' doesn't have a material assigned to it!", eventArgs.NewColor.name));
		}
	}
	
	void Update () {
	
	}
}
