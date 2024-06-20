using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    [SerializeField] public Transform cameraPostion;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateCamera()
    {
        if (cameraPostion != null)
        {

            transform.position = cameraPostion.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
