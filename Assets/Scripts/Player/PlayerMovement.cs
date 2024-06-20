using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float playerHeight = 2;

    public float playerSpeedIgnoringY;

    public Pickup playerPickup;

    [SerializeField] Transform orientation;

    public Transform cameraTargetPos;

    public MoveCamera cameraObject;

    public PlayerLook playerLook;

    [Header("NetworkedBehaviours")]
    public PlayerRef playerRef;


    [Header("MovementVariables")]
    public float moveSpeed = 6.0f;
    public float moveMultiplier = 10.0f;
    [SerializeField] float airMultiplier = 0.1f;
    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    [Header("Drag")]
    [SerializeField]float groundDrag = 6;
    [SerializeField]float airDrag = 2f;



    [Header("Jumping")]
    public bool isGrounded = false;
    public float groundHugRadius = 0.4f;
    public float jumpForce = 5.0f;

    Vector3 slopeMoveDirection;

   

    public Rigidbody rb;

    [SerializeField] LayerMask jumpLayers;

    public float noSlipSlopeThreshold = 30;

    private float lastJumpTime;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    bool spawned = false;
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            jumpLayers = 1 << LayerMask.NameToLayer("Ground");
            jumpLayers = jumpLayers | 1 << LayerMask.NameToLayer("Object");
            jumpLayers = jumpLayers | 1 << LayerMask.NameToLayer("Default");

            cameraObject = FindObjectOfType<MoveCamera>();

            cameraObject.cameraPostion = cameraTargetPos;

            playerPickup.holdPosition = FindObjectOfType<CameraManager>().grabPosition;

            playerLook.cam = cameraObject.transform;

            cameraObject.transform.forward=transform.forward;

            spawned= true;

            FindObjectOfType<BasicSpawner>().localPlayer = this;

            //FindObjectOfType<MoveCamera>().cameraPostion=
        }
        else
        {
            jumpLayers = 1 << LayerMask.NameToLayer("Ground");
            jumpLayers = jumpLayers | 1 << LayerMask.NameToLayer("Object");
            jumpLayers = jumpLayers | 1 << LayerMask.NameToLayer("Default");

            // rb = GetComponent<Rigidbody>();
            //   rb.isKinematic = true;
        }
        base.Spawned();
    }
    public override void Render()
    {
        if (HasInputAuthority)
        {
            cameraObject.UpdateCamera();
        }
    }
    public Vector3 direction;
    public override void FixedUpdateNetwork()
    {
        isGrounded = Physics.SphereCast(transform.position, groundHugRadius, Vector3.down, out groundCastinfo, playerHeight / 2 + 0.1f, jumpLayers);
        if (GetInput(out PlayerInputData data))
        {
            MovePlayerWithData(data.direction);
            direction= data.direction;
            CalculateSpeed();
            ControlDrag();
            //Prevent player from picking up an object they're standing on
            if (isGrounded)
            {
                if (playerPickup.GetHeldObject() == groundCastinfo.collider.gameObject && playerPickup.GetHeldObject() != null)
                {
                    playerPickup.DropObject();
                }
            }
            slopeMoveDirection = Vector3.ProjectOnPlane(data.direction, slopeHit.normal);

            if (data.buttons.IsSet(PlayerInputData.JUMP))
            {
                Jump();
            }

        }
        
    }

    RaycastHit groundCastinfo;
    // Update is called once per frame
    void Update()
    {
        /*
        if (HasInputAuthority)
        {
            isGrounded = Physics.SphereCast(transform.position, groundHugRadius, Vector3.down, out groundCastinfo, playerHeight / 2 + 0.1f, jumpLayers);
            CollectInput();
            ControlDrag();

            if (isGrounded)
            {
                if (playerPickup.GetHeldObject() == groundCastinfo.collider.gameObject && playerPickup.GetHeldObject() != null)
                {
                    playerPickup.DropObject();
                }
            }
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time - lastJumpTime > 0.1f)
            {
                Jump();
            }
        }*/
    }

    private void Jump()
    {
        if (playerPickup != null)
        {
            if (playerPickup.GetHeldObject() != groundCastinfo.collider.gameObject)
            {
                lastJumpTime = Time.time;
                rb.drag = airDrag;
                transform.position += (transform.up + groundCastinfo.normal).normalized * 0.1f;
                rb.AddForce((transform.up + groundCastinfo.normal).normalized * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            lastJumpTime = Time.time;
            rb.drag = airDrag;
            transform.position += (transform.up + groundCastinfo.normal).normalized * 0.1f;
            rb.AddForce((transform.up + groundCastinfo.normal).normalized * jumpForce, ForceMode.Impulse);
        }
        
    }
    public Vector3 ReturnInput(float x,float y)
    {
       horizontalMovement = Input.GetAxisRaw("Horizontal");
       verticalMovement = Input.GetAxisRaw("Vertical");
       moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
       return moveDirection;
    }

    void CollectInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection=orientation.forward *verticalMovement+orientation.right *horizontalMovement;
    }
    void ControlDrag()
    {
        rb.useGravity = true;
        rb.drag = groundDrag;
        if (OnSlope() && Vector3.Angle(slopeHit.normal, Vector3.up) < noSlipSlopeThreshold && Mathf.Abs(verticalMovement)+Mathf.Abs(horizontalMovement)==0&& Time.time-lastJumpTime>0.1f && rb.velocity.magnitude<0.65f)
        {
            rb.drag = groundDrag * 10;
            rb.useGravity = false;
        }
        else if (isGrounded)
        {
            rb.drag = groundDrag;
        } //If the player is not moving and stood on a slope where they shouldn't slide, up their drag so they don't move
        else
        {
            rb.drag = airDrag;
        }
    }
    void MovePlayerWithData(Vector3 moveDir)
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else
        {
            //check playerspeed is more than a max speed, if this is the case, limit their speed but preserve y velocity
            if (playerSpeedIgnoringY > moveSpeed * 1.5f)
            {
                float yVelocity = rb.velocity.y;

                Vector3 velocity = rb.velocity;

                velocity.y = 0;

                velocity = velocity.normalized;

                velocity = moveSpeed * velocity * 1.5f;

                velocity += Vector3.up * yVelocity;
                rb.velocity = velocity;
            }
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded&&OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else
        {
            //check playerspeed is more than a max speed, if this is the case, limit their speed but preserve y velocity
            if(playerSpeedIgnoringY>moveSpeed*1.5f)
            {
                float yVelocity = rb.velocity.y;

                Vector3 velocity = rb.velocity;

                velocity.y = 0;

                velocity=velocity.normalized;

                velocity = moveSpeed * velocity * 1.5f;

                velocity += Vector3.up * yVelocity;
                rb.velocity = velocity;
            }
            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier*airMultiplier, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        
    }

    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            //Debug.Log(Vector3.Angle(slopeHit.normal, Vector3.up));
            if(slopeHit.normal!=Vector3.up)
            {
                return true;
            }
        }
        return false;
    }
    Vector3 lastPos;
    private void CalculateSpeed()
    {
        Vector3 newPos=transform.position;
        newPos.y = 0;
        if (Time.fixedDeltaTime != 0)
        {
            playerSpeedIgnoringY = (newPos - lastPos).magnitude / Time.fixedDeltaTime;
        }
        else
            Debug.LogWarning("Somehow the fixed delta time is 0? Fix that");

        lastPos = transform.position;
        lastPos.y= 0;
    }
}
