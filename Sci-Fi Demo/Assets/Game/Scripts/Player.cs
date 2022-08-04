using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3.5f;
    private float _gravity = 9.81f;
    private CharacterController _controller;
    [SerializeField]
    private  GameObject _muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRaycast();
        CalcuateMovement();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CalculateRaycast()
    {
        if (Input.GetMouseButton(0)) {
            _muzzleFlash.SetActive(true);
            Ray origin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(origin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
            }
        } else {
            _muzzleFlash.SetActive(false);
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
