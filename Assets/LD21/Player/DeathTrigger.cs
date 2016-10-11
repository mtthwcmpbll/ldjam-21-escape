using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	public void OnTriggerEnter(Collider other) {
		Debug.Log("Dead!");
		PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
        if (null != player) {
			player.IsDead = true;
		}
    }
}
