using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructables : MonoBehaviour
{
    [SerializeField]
    private GameObject _crateDestgroyed;

    public void DestroyCrate() {
        Instantiate(_crateDestgroyed, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
