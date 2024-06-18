using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nonobehaviour
{
    public class NonobehaviourAction
    {
        public bool executeOnLoad = false;
        public virtual void Execute()
        {
            Debug.LogError("Unimplemented Nonobehaviour Action");
        }
    }
}