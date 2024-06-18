using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAuthority : MonoBehaviour
{

    public Component priorityComponent;


    public bool CanMove(Component t)
    {
        return t==priorityComponent;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
