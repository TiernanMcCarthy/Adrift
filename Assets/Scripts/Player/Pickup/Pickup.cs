using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : NetworkBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] public Transform holdPosition;
    [SerializeField] Transform raycastPoint;
    private GameObject heldObj { get; set; }
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    Vector3 objLastPos;

    public override void Spawned()
    {
        if(HasInputAuthority)
        {
            raycastPoint = FindObjectOfType<CameraManager>().raycastPoint;
        }
    }
    private void Update()
    {
       

        
    }
    public override void FixedUpdateNetwork()
    {
        //Manipulate Object

        if (GetInput(out PlayerInputData data))
        {
            if (data.buttons.IsSet(PlayerInputData.ACTION))
            {
                if (heldObj == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(raycastPoint.transform.position, raycastPoint.forward, out hit, pickupRange))
                    {
                        //pickupObject
                        PickupObject(hit.transform.gameObject);
                    }
                }
                else //Drop Object
                {
                    DropObject();
                }
            }
            if (heldObj != null)
            {
                objLastPos = heldObj.transform.position;
            }
        }
        if (heldObj != null && HasInputAuthority)
        {
            MoveObject();
        }

    }
    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdPosition.position) > pickupRange)
        {
            DropObject();
            return;
        }
        if(Vector3.Distance(heldObj.transform.position,holdPosition.position)>0.1f)
        {
            Vector3 moveDir = (holdPosition.position - heldObj.transform.position);

            heldObjRB.AddForce(moveDir*pickupForce);
        }
    }

    void PickupObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            heldObj = obj;
            heldObjRB = obj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = true;
            heldObjRB.drag = 10;
            heldObjRB.constraints= RigidbodyConstraints.FreezeRotation;
            //heldObj.transform.parent = holdPosition;
        }
    }

    public void DropObject()
    {
        if (heldObj==null)
            return;
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObj.transform.parent = null;

        Vector3 dir = heldObj.transform.position-objLastPos;
        if(dir.x==0)
        {
            dir.x += 0.01f;
        }
        if(dir.y==0)
        {
            dir.y += 0.01f;
        }
        if (dir.z == 0)
        {
            dir.z += 0.01f;
        }
        heldObjRB.velocity= (dir/Time.fixedDeltaTime)*2;

       
        heldObj = null;
        heldObjRB = null;



    }

    public GameObject GetHeldObject()
    {
        return heldObj; 
    }

}
