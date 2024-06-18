using Fusion;
using Fusion.Addons.ConnectionManagerAddon;
using Fusion.XR.Shared.Rig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : NetworkBehaviour
{
    [Networked]
    public string playerName { get; set; }

    [Networked]
    public PlayerRef playerRef { get; set; }

    [Networked]
    public string joinTime { get; set; }

    public NetworkRig networkRig;

    public MoveAuthority t;

    public bool simulated = true;

    /// <summary>
    /// Local Actions that can be subscribed to and implemented once, before they are cleared and removed
    /// </summary>
    public List<Nonobehaviour.NonobehaviourAction> nonobehaviourActions = new List<Nonobehaviour.NonobehaviourAction>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteNonoBehaviours()
    {
        foreach(Nonobehaviour.NonobehaviourAction d in nonobehaviourActions)
        {
            d.Execute();
        }
        nonobehaviourActions.Clear();
    }

    public override void Spawned()
    {
        base.Spawned();
        networkRig = GetComponent<NetworkRig>();
        DontDestroyOnLoad(gameObject);
        if (HasInputAuthority)
        {
            //playerName = playerRef.PlayerId.ToString();
            joinTime=System.DateTime.Now.TimeOfDay.Hours+":" +System.DateTime.Now.TimeOfDay.Minutes.ToString();
        }

        PlayerManagement.instance.RecreatePlayerList();
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
        simulated = false;
    }

    public void MovePlayer()
    {
        networkRig.hardwareRig.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void MovePositionRPC()
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        // Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        //NetworkedHealth -= damage;
        MovePlayer();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void MoveAndLookAtRPC(Vector3 location, Vector3 forward)
    {
        networkRig.hardwareRig.transform.position = location;
        networkRig.hardwareRig.transform.forward = forward;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void LoadSceneRPC()
    {
       if(Runner.IsSceneAuthority)
        {
            Nonobehaviour.SpawnPlayers temp = new Nonobehaviour.SpawnPlayers();
            PlayerManagement.instance.localPlayer.nonobehaviourActions.Add(temp);
            ConnectionManager.instance.runner.LoadScene("CargoScene");
        }
    }

    //public override 
}
