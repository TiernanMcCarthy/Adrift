using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : NetworkBehaviour
{
    [SerializeField]
    private float sensX;
    [SerializeField]
    private float sensY;

    [SerializeField] public Transform cam;
    [SerializeField] Transform orientation;
    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    public PlayerMovement playerBehaviour;

    bool isReady=false;

  
    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (HasInputAuthority)
        {
            CollectInput();
            if (cam != null)
            {


                cam.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

                orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }

        }

    }

    void CollectInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90);

    }
}
