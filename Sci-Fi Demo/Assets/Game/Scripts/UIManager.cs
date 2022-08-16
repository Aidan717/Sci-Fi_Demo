using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _ammoText;

    [SerializeField]
    private GameObject _coin;

    public void UpdateAmmo(int count) {
        _ammoText.text = "Ammo: " + count.ToString();
    }

    public void CollectedCoin() {
        _coin.SetActive(true);
    }

    public void CoinUsed() {
        _coin.SetActive(false);
    }
}