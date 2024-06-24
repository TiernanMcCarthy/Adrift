using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : NetworkBehaviour
{
    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
