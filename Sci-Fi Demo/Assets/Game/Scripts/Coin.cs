using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private AudioClip _coinPickupSound;


    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (Input.GetKey(KeyCode.E)) {
                Player player = other.GetComponent<Player>();
                if (player) {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinPickupSound, transform.position, 1f);
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    if (uiManager != null) {
                        uiManager.CollectedCoin();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
