using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpawnManagement;
using System;

namespace Nonobehaviour
{
    public class SpawnPlayers : NonobehaviourAction
    {
        public new bool executeOnLoad = true;
        public override void Execute()
        {
            SpawnManager.instance.SpawnPlayers();
            GC.Collect();
        }
    }
}
