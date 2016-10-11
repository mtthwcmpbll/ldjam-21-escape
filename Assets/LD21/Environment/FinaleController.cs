using UnityEngine;
using System.Collections;

public class FinaleController : MonoBehaviour {
	
	public TextMesh FinaleText;
	
	public void OnTriggerEnter(Collider other) {
		Debug.Log("End!");
		PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
		
		player.transform.LookAt(FinaleText.transform);
		
	    if (null != player) {
			foreach (MouseLook ml in player.gameObject.GetComponentsInChildren<MouseLook>()) {
				ml.enabled = false;
			}
			player.gameObject.GetComponent<CharacterMotor>().enabled = false;
			player.gameObject.GetComponent<CharacterController>().enabled = false;
			player.gameObject.GetComponent<FPSInputController>().enabled = false;
			FinaleText.GetComponent<Renderer>().enabled = true;
		}
	}
}