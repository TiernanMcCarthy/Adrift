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
    [SerializeField] public Transform orientation;
   // public Transform 

    public Transform cosmeticBody;
    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    public PlayerMovement playerBehaviour;

    bool isReady = false;


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
            Vector2 rot = CollectInput();
            cam.localRotation = Quaternion.Euler(rot.x,rot.y,0);
            // CollectInput();
            if (cam != null)
            {


                //cam.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

                // playerBehaviour.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }

        }

    }

    public void UpdateRotation(Vector2 rotation)
    {
        if (HasInputAuthority)
        {
            //cam.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        orientation.localRotation = Quaternion.Euler(rotation.x,rotation.y,0);
        cosmeticBody.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetOrientation(float yRot)
    {
        if (!HasInputAuthority)
        {
            yRotation = yRot;
        }
        //orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public Vector2 CollectInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90);

        return new Vector2(xRotation, yRotation);
    }

    public Vector2 SampleInput()
    {
        return new Vector2(xRotation, yRotation);
    }

}
