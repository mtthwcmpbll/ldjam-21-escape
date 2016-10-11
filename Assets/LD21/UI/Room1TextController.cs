using UnityEngine;
using System.Collections;

public class Room1TextController : MonoBehaviour {
	
	public PlayerStatus Player;
	
	private bool state_satisfied = false;
	
	public void Start() {
		Player.ColorChanged += UpdateText;
	}
	
	private void UpdateText(object sender, ColorChangedEventArgs data) {
		if (data.NewColor.name.Equals("magenta")) {
			GetComponent<TextMesh>().text = "Good.  I made something for you.";
			state_satisfied = true;
		} else if (!state_satisfied) {
			GetComponent<TextMesh>().text = "This is going to be so much fun!";
		}
	}
	
	public void OnTriggerExit(Collider other) {
		PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
        if (null != player) {
			Player.ColorChanged -= UpdateText;
			Destroy(gameObject);
		}
    }
}
