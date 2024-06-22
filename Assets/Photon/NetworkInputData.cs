using Fusion;
using UnityEngine;

public struct PlayerInputData : INetworkInput
{
    public Vector3 direction;

    public Vector2 rotationVect;

    public bool jumpState;

    public bool actionState;


    //Pickup/ activate something
    public const byte ACTION = 1;

    public const byte JUMP = 2;



    //container for const actions
    public NetworkButtons buttons;
}

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON0 = 1;
    public const byte MOUSEBUTTON1 = 2;

    public NetworkButtons buttons;
    public Vector3 direction;
}