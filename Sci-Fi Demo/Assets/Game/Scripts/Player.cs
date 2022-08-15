using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3.5f;
    private float _gravity = 9.81f;
    private CharacterController _controller;
    [SerializeField]
    private  GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarker;
    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool _isReloading = false;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  

        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRaycast();
        CalcuateMovement();
        Reload();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Reload() {
        if (Input.GetKey(KeyCode.R) && _isReloading == false ) {
            _isReloading = true;
            StartCoroutine("WeaponReload");
            
        }
    }

    IEnumerator WeaponReload() {
        yield return new WaitForSeconds(2f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }
        

    private void CalculateRaycast()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0) {
            _muzzleFlash.SetActive(true);
            if (!_weaponAudio.isPlaying) {
                _weaponAudio.Play();    
            }
            currentAmmo--;
            _uiManager.UpdateAmmo(currentAmmo);
            
            Ray origin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(origin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                GameObject hitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                //GameObject hitMarker = (GameObject)Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 0.2f);
            }
        } else {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }
    }

    void CalcuateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = _moveSpeed * direction;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move( Time.deltaTime * velocity );
    }
}
