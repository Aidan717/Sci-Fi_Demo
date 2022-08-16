using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour {

    [SerializeField]
    private AudioClip _winAudio;


    
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if(player != null) {
                if (Input.GetKey(KeyCode.E)) {
                    if( player.hasCoin == true) {
                        player.hasCoin = false;
                        UIManager _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        if(_uiManager != null) {
                            _uiManager.CoinUsed();
                        }
                        AudioSource.PlayClipAtPoint(_winAudio, Camera.main.transform.position, 1f); 
                        player.EnableWeapons();
                    } 
                    else {
                        Debug.Log("You dont got coinzz");
                    }
                }
            }
            
        }
    }
}
