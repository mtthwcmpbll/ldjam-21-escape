using UnityEngine;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		Debug.Log("Checkpoint!");
		PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
        if (null != player) {
			player.CurrentCheckpoint = gameObject.transform.position;
		}
    }
}
